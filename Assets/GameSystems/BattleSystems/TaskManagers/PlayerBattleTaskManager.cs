using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

using Skill;
using Character;
using MasterData;

using ActiveSkillType = Skill.ActiveSkillParameters.ActiveSkillType;
using Extent = Skill.ActiveSkillParameters.Extent;

namespace BattleSystem{
	public class PlayerBattleTaskManager : MonoBehaviour ,IBattleTaskManager{
		//記録されているタスクのリストです
		private List<BattleTask> tasks = new List<BattleTask>();

		//スクロールビューのcontentです
		public GameObject contents;
		//スクロールビューのreactoinContentsです
		public GameObject reactoinContents;
		//アタッチされているスクロールビューです
		public GameObject view;

		//元のプレイヤーです
		private IPlayable player;
		//現在選ばれているActiveSkillです
		private IActiveSkill chosenActiveSkill;
		//現在選ばれているのがmoveか、それ以外かを表します
		private ActiveSkillType activeSkillType;
		//現在リアクション中のKeyValuePairです
		private KeyValuePair<IBattleable,AttackSkill> prosessingPair;
		//現在リアクションを待っているKeyValuePairたちです
		private List<KeyValuePair<IBattleable,AttackSkill>> waitingReactionActiveSkills = new List<KeyValuePair<IBattleable, AttackSkill>>();

		//現在のステートです
		private BattleState battleState = BattleState.ACTION;

		//残り待機フレーム数です
		private float delay = 0;
		//残りリアクションフレーム数です
		private float reactionLimit = 0;

		private bool needToReaction = false;

		// Use this for initialization
		void Start () {
			GameObject canvas = GameObject.Find ("Canvas/BattleView");
			view.transform.SetParent (canvas.transform);
			reactoinContents.SetActive (false);
		}
		
		// Update is called once per frame
		void Update () {
			if (player.getHp () <= 0) {
				BattleManager.getInstance ().deadCharacter (player);
			}

			if(needToReaction){
				reactionState ();
			}else if (battleState == BattleState.ACTION && tasks.Count > 0) {
				actionState ();
			} else if (battleState == BattleState.IDLE) {
				idleState ();
			}


		}
			
		//update時リアクションが必要な時に呼ばれます
		private void reactionState(){
			reactionLimit -= Time.deltaTime;
			if(reactionLimit <= 0){
				reaction (ReactionSkillMasterManager.getReactionSkillFromId(2));

				reactoinContents.SetActive (false);
				contents.SetActive (true);
				detachReactionContents ();
			}
		}

		//update時actionステートの時に呼ばれます
		private void actionState(){
			BattleTask runTask = tasks [0];
			runTask.getSkill ().action (player, runTask);
			delay = runTask.getSkill ().getDelay (player);
			tasks.Remove (runTask);
			battleState = BattleState.IDLE;
		}

		//update時idleステート時に呼ばれます
		private void idleState(){
			delay -= Time.deltaTime;
			if (delay <= 0) {
				battleState = BattleState.ACTION;
			}
		}

		//プレイヤーをManagerにセットします
		public void setPlayer(IPlayable player){
			this.player = player;
			inputActiveSkillList ();
		}

		//タスクを追加します
		private void setTask(IActiveSkill skill,List<IBattleable> targets){
			tasks.Add(new BattleTask(player.getUniqueId(),skill,targets));
		}

		//skillnodeが選ばれた時の処理です
		public void skillChose(IActiveSkill chosenSkill){
			this.chosenActiveSkill = chosenSkill;

			if (chosenSkill.getActiveSkillType () != ActiveSkillType.MOVE) {
				Extent extent = ActiveSkillSupporter.searchExtent (chosenSkill);
				int range = ActiveSkillSupporter.searchRange (chosenSkill, player);
				inputTargetList (extent, range);
			} else if (chosenSkill.getActiveSkillType () == ActiveSkillType.MOVE) {
				int move = ActiveSkillSupporter.searchMove (chosenSkill, player);
				inputMoveAreaList (move);
			} else {
				throw new NotSupportedException ("Unkonwn skillType");
			}
		}

		//targtNodeが選ばれた時の処理です
		public void targetChose(List<IBattleable> targets){
			tasks.Add (new BattleTask(player.getUniqueId(),chosenActiveSkill,targets));
			chosenActiveSkill = null;
			inputActiveSkillList ();
		}

		//moveAreaNodeが選ばれた時の処理です
		public void moveAreaChose(FieldPosition pos){
			int move = (int)(pos - BattleManager.getInstance ().searchCharacter(player));
			tasks.Add (new BattleTask(player.getUniqueId(),chosenActiveSkill,move));
			chosenActiveSkill = null;
			inputActiveSkillList ();
		}

		//passiveNodeが選ばれた時の処理です
		public void reactionChose(ReactionSkill chosenSkill){
			reaction (chosenSkill);

			reactoinContents.SetActive (false);
			contents.SetActive (true);
			detachReactionContents ();
		}

		//passiveスキルを使用します
		private void reaction(ReactionSkill passiveSkill){
			IBattleable attacker = prosessingPair.Key;
			AttackSkill skill = prosessingPair.Value;
			int atk = skill.getAtk (attacker);
			int hit = skill.getHit(attacker);
			Debug.Log ("Dammage is " + atk + "hit is " + hit);
			passiveSkill.reaction (player,atk,hit,skill.getAttackSkillAttribute());
			waitingReactionActiveSkills.Remove (prosessingPair);
			updateProsessingPair ();
		}

		//スクロールビューにActiveSkillのリストを表示します
		private void inputActiveSkillList(){
			detachContents ();

			foreach(IActiveSkill skill in player.getActiveSkills()){
				GameObject node =  Instantiate ((GameObject)Resources.Load("Prefabs/ActiveSKillNode"));
				node.GetComponent<ActiveSkillNode> ().setState (this,skill);
				node.transform.SetParent (contents.transform);
			}
		}

		//スクロールビューにTargetのリストを表示します
		private void inputTargetList(Extent extent,int range){
			detachContents ();
			switch(extent){
				case Extent.SINGLE:
					inputSingleTargetList (range);
					break;
				case Extent.AREA:
					inputAreaTargetList (range);
					break;
				case Extent.ALL:
					targetChose (BattleManager.getInstance().getJoinedBattleCharacter());
					break;
				case Extent.NONE:
					throw new ArgumentException ("invalid skill");
			}
		}

		//スキルの効果範囲が単体の時のtargetをビューにインプットします
		private void inputSingleTargetList(int range){
			List<IBattleable> targets = BattleManager.getInstance ().getCharacterInRange (player, range);

			foreach (IBattleable target in targets) {
				if (target.Equals (player))
					continue;
				
				GameObject node = Instantiate ((GameObject)Resources.Load ("Prefabs/TargetNode"));
				node.GetComponent<TargetNode> ().setState (target, this);
				node.transform.SetParent (contents.transform);
			}
		}

		//スキルの効果範囲が範囲の時のtargetをビューにインプットします
		private void inputAreaTargetList(int range){
			FieldPosition nowPos = BattleManager.getInstance ().searchCharacter (player);

			int index = (int)nowPos - range;
			index = (index < 0) ? 0 : index;

			int maxRange = (int)nowPos + range;
			maxRange = (maxRange > 7) ? 7 : maxRange;
			maxRange = (maxRange < 0) ? 0 : maxRange;

			for (; index < maxRange; index++) {
				if ((nowPos + index) >= 0) {
					GameObject node = Instantiate ((GameObject)Resources.Load ("Prefabs/TargetNode"));
					node.GetComponent<TargetNode> ().setState ((FieldPosition)index, this);
					node.transform.SetParent (contents.transform);
				}
			}
		}

		//スキルの効果範囲が全体の時のtargetをビューにインプットします
		private void inputMoveAreaList(int move){
			detachContents ();

			FieldPosition nowPos = BattleManager.getInstance ().searchCharacter (player);

			int index = (int)nowPos - move;
			index = (index < 0) ? 0 : index;

			int maxpos = (int)nowPos + move + 1;
			maxpos = (maxpos > 7) ? 7 : maxpos;
			maxpos = (maxpos < 0) ? 0 : maxpos;

			for (; index < maxpos; index++) {
				if ((nowPos + index) >= 0) { 
					GameObject node = Instantiate ((GameObject)Resources.Load ("Prefabs/MoveAreaNode"));
					node.GetComponent<MoveAreaNode> ().setState ((FieldPosition)index, this);
					node.transform.SetParent (contents.transform);
				}
			}
		}

		//スクロールビューにPassiveSkillのリストを表示します
		private void inputReactionSkillList(){
			detachReactionContents ();
			contents.SetActive (false);
			foreach(ReactionSkill skill in player.getReactionSKills()){
				GameObject node = Instantiate((GameObject)Resources.Load ("Prefabs/ReactionSkillNode"));
				node.GetComponent<ReactionSkillNode> ().setState (skill,this);
				node.transform.SetParent (reactoinContents.transform);
			}
			reactoinContents.SetActive (true);
		}

		//contentsオブジェクトの子ノードを削除します
		private void detachContents(){
			Transform children = contents.GetComponentInChildren<Transform> ();
			foreach(Transform child in children){
				MonoBehaviour.Destroy (child.gameObject);
			}
			contents.transform.DetachChildren ();
		}

		//passiveContentsオブジェクトの子ノードを削除します
		private void detachReactionContents(){
			Transform children = reactoinContents.GetComponentInChildren<Transform> ();
			foreach(Transform child in children){
				MonoBehaviour.Destroy (child.gameObject);
			}
			reactoinContents.transform.DetachChildren ();
		}

		//実行中のアクティブスキルを更新します
		private void updateProsessingPair(){
			if (waitingReactionActiveSkills.Count > 0) {
				prosessingPair = waitingReactionActiveSkills[0];
				inputReactionSkillList ();
//				reactionLimit = pair.Value.getDelay() ;
				reactionLimit = 1;
				needToReaction = true;
			} else {
				needToReaction = false;
			}
		}

		#region IBattleTaskManager implementation

		public void deleteTaskFromTarget (IBattleable target) {
			foreach(BattleTask task in tasks){
				foreach(IBattleable bal in task.getTargets()){
					if (bal.Equals (target))
						tasks.Remove (task);
				}
			}
		}

		public void offerReaction (IBattleable attacker, AttackSkill skill) {
			KeyValuePair<IBattleable,AttackSkill> pair = new KeyValuePair<IBattleable, AttackSkill> (attacker,skill);
			waitingReactionActiveSkills.Add (pair);

			updateProsessingPair ();
		}

		public bool isHavingTask () {
			return tasks.Count > 0;
		}

		public void win () {
			//(実装)まだです
		}
			
		public void finished(){
			MonoBehaviour.Destroy (view);
		}
		#endregion


	}
}