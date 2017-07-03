using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
		private ActiveSkill chosenActiveSkill;
		//現在リアクション中のKeyValuePairです
		private KeyValuePair<IBattleable,ActiveSkill> prosessingPair;
		//現在リアクションを待っているKeyValuePairたちです
		private List<KeyValuePair<IBattleable,ActiveSkill>> waitingReactionActiveSkills = new List<KeyValuePair<IBattleable, ActiveSkill>>();

		//
		private BattleState battleState = BattleState.ACTION;

		private int delay = 0;
		private int reactionLimit = 0;

		private bool needToReaction = false;

		// Use this for initialization
		void Start () {
			GameObject canvas = GameObject.Find ("Canvas/BattleView");
			view.transform.SetParent (canvas.transform);
			reactoinContents.SetActive (false);
		}
		
		// Update is called once per frame
		void Update () {
//			Debug.Log ("updated pl");
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
			reactionLimit--;
			if(reactionLimit <= 0){
				reaction (ReactionSkillMasterManager.getReactionSkillFromId(2));

				reactoinContents.SetActive (false);
				contents.SetActive (true);
				detachReactionContents ();
			}
			Debug.Log ("" + player.getHp());
		}

		//update時actionステートの時に呼ばれます
		private void actionState(){
			BattleTask runTask = tasks [0];
			runTask.getSkill ().action (player, runTask);
			delay = runTask.getSkill ().getDelay ();
			tasks.Remove (runTask);
			battleState = BattleState.IDLE;
		}

		//update時idleステート時に呼ばれます
		private void idleState(){
			delay--;
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
		private void setTask(ActiveSkill skill,List<IBattleable> targets){
			tasks.Add(new BattleTask(player.getUniqueId(),skill,targets));
		}

		//skillnodeが選ばれた時の処理です
		public void skillChose(ActiveSkill chosenSkill){
			Debug.Log (chosenSkill.ToString());
			this.chosenActiveSkill = chosenSkill;

			switch(chosenSkill.getActiveSkillType()){
				case ActiveSkillType.ACTION:
					inputTargetList ();
					break;
				case ActiveSkillType.MOVE:
					inputMoveAreaList ();
					break;
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
			int move = (int) (BattleManager.getInstance ().searchCharacter(player) - pos);
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
			ActiveSkill skill = prosessingPair.Value;
			int atk = attacker.getAtk (skill.getAttribute (), skill.getUseAbility()) + skill.getAtk ();
			int hit = attacker.getHit (skill.getUseAbility()) + skill.getHit();
			passiveSkill.reaction (player,atk,hit,skill.getAttribute());
			waitingReactionActiveSkills.Remove (prosessingPair);
			updateProsessingPair ();
		}

		//スクロールビューにActiveSkillのリストを表示します
		private void inputActiveSkillList(){
			detachContents ();

			foreach(ActiveSkill skill in player.getActiveSkills()){
				GameObject node =  Instantiate ((GameObject)Resources.Load("Prefabs/ActiveSKillNode"));
				node.GetComponent<ActiveSkillNode> ().setState (this,skill);
				node.transform.SetParent (contents.transform);
			}
		}

		//スクロールビューにTargetのリストを表示します
		private void inputTargetList(){
			detachContents ();
			switch(chosenActiveSkill.getExtent()){
				case Extent.SINGLE:
					inputSingleTargetList ();
					break;
				case Extent.AREA:
					inputAreaTargetList ();
					break;
				case Extent.ALL:
					targetChose (BattleManager.getInstance().getJoinedBattleCharacter());
					break;
			}
		}

		//スキルの効果範囲が単体の時のtargetをビューにインプットします
		private void inputSingleTargetList(){
			List<IBattleable> targets = BattleManager.getInstance ().getCharacterInRange (player, chosenActiveSkill.getRange ());
			foreach (IBattleable target in targets) {
				GameObject node = Instantiate ((GameObject)Resources.Load ("Prefabs/TargetNode"));
				node.GetComponent<TargetNode> ().setState (target, this);
				node.transform.SetParent (contents.transform);
			}
		}

		//スキルの効果範囲が範囲の時のtargetをビューにインプットします
		private void inputAreaTargetList(){
			FieldPosition nowPos = BattleManager.getInstance ().searchCharacter (player);
			for (int i = -1 * (int)nowPos; i < chosenActiveSkill.getRange (); i++) {
				if ((nowPos + i) >= 0) {
					GameObject node = Instantiate ((GameObject)Resources.Load ("Prefabs/TargetNode"));
					node.GetComponent<TargetNode> ().setState ((FieldPosition)(nowPos + i), this);
					node.transform.SetParent (contents.transform);
				}
			}
		}

		//スキルの効果範囲が全体の時のtargetをビューにインプットします
		private void inputMoveAreaList(){
			FieldPosition nowPos = BattleManager.getInstance ().searchCharacter (player);
			for (int i = -1 * (int)nowPos; i < chosenActiveSkill.getRange (); i++) {
				if ((nowPos + i) >= 0) {
					GameObject node = Instantiate ((GameObject)Resources.Load ("Prefabs/MoveAreaNode"));
					node.GetComponent<MoveAreaNode> ().setState ((FieldPosition)(nowPos + i), this);
					node.transform.SetParent (contents.transform);
				}
			}
		}

		//スクロールビューにPassiveSkillのリストを表示します
		private void inputReactionSkillList(){
			detachReactionContents ();
			contents.SetActive (false);
			foreach(ReactionSkill skill in player.getReactionSKills()){
				GameObject node = Instantiate((GameObject)Resources.Load ("Prefabs/PassiveSkillNode"));
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
				reactionLimit = 1000;
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

		public void offerReaction (IBattleable attacker, ActiveSkill skill) {
			KeyValuePair<IBattleable,ActiveSkill> pair = new KeyValuePair<IBattleable, ActiveSkill> (attacker,skill);
			waitingReactionActiveSkills.Add (pair);

			updateProsessingPair ();

			Debug.Log ("offered");
		}

		public bool isHavingTask () {
			return tasks.Count > 0;
		}
		#endregion
	}
}