using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

using Skill;
using Character;
using MasterData;
using Parameter;

using ActiveSkillType = Skill.ActiveSkillParameters.ActiveSkillType;
using Extent = Skill.ActiveSkillParameters.Extent;

namespace BattleSystem{
	public class PlayerBattleTaskManager : MonoBehaviour ,IBattleTaskManager{
		/// <summary> 登録済みタスクのリスト </summary>
		private List<BattleTask> tasks = new List<BattleTask>();

		/// <summary> スクロールビューのcontent </summary>
		public GameObject contents;
		/// <summary> スクロールビューのreactionContent </summary>
		public GameObject reactoinContents;
		/// <summary> アタッチされているオブジェクト </summary>
		public GameObject view;
        /// <summary> 戻るボタン </summary>
        public Button backButton;
        /// <summary> ヘッダのTextオブジェクト </summary>
        public Text headerText;

		/// <summary> 管理対象のキャラクター </summary>
		private IPlayable player;
		/// <summary> 現在選択されているIActiveSkillSkill </summary>
		private IActiveSkill chosenActiveSkill;
		/// <summary> 現在リアクション処理中のスキルと、その攻撃者 </summary>
		private KeyValuePair<IBattleable,AttackSkill> prosessingPair;
		/// <summary> リアクション待ちのスキルのリスト </summary>
		private List<KeyValuePair<IBattleable,AttackSkill>> waitingReactionActiveSkills = new List<KeyValuePair<IBattleable, AttackSkill>>();

		/// <summary> 現在のステート </summary>
		private BattleState battleState = BattleState.ACTION;

		/// <summary> 残りディレイ秒数 </summary>
		private float delay = 0;
        private float maxDelay;
		/// <summary> 残りリアクソン制限時間 </summary>
		private float reactionLimit = 0;

        /// <summary> リアクションが必要かどうか </summary>
		private bool needToReaction = false;

        /// <summary> BattleTaskを識別するIDのカウント </summary>
        private long battletaskIdCount = 0;

        /// <summary> 関連づけられているBattleTaskListView </summary>
        private BattleTaskListView listView;

        /// <summary> 関連づけられているBattleStateNode </summary>
        private BattleStateNode state;

        /// <summary> 戻るボタンを表示する必要があるかどうか </summary>
        private bool isInputingBackButton = false;

        private FieldPosition goingPos;

		// Use this for initialization
		void Start () {
            GameObject battleView = GameObject.Find ("Canvas/BattleView");
			view.transform.SetParent (battleView.transform);
			reactoinContents.SetActive (false);

            GameObject taskView = GameObject.Find("Canvas/TaskView");
            GameObject battleListNode = Instantiate((GameObject)Resources.Load("Prefabs/BattleTaskListView"));
            battleListNode.transform.SetParent(taskView.transform);
            listView = battleListNode.GetComponent<BattleTaskListView>();
            listView.setManager(this);
		}
		
		// Update is called once per frame
		void Update () {
            Debug.Log("update "  + goingPos);

			if (player.getHp () <= 0) {
				BattleManager.getInstance ().deadCharacter (player);
			}

			if(needToReaction){
				reactionState ();
			}else if (battleState == BattleState.ACTION && tasks.Count > 0) {
				actionState ();
			}
            if (battleState == BattleState.IDLE) {
				idleState ();
			}
		}
			
		/// <summary>
        /// リアクションが必要な時に毎フレーム行う処理
        /// </summary>
		private void reactionState(){
			reactionLimit -= Time.deltaTime;
			if(reactionLimit <= 0){
				reaction (ReactionSkillMasterManager.getReactionSkillFromId(2));

				reactoinContents.SetActive (false);
				contents.SetActive (true);
				detachReactionContents ();

				backButton.gameObject.SetActive(isInputingBackButton);
			}
		}

		/// <summary>
        /// ステートがACTIONの時に毎フレーム行う処理
        /// </summary>
		private void actionState(){
			BattleTask runTask = tasks [0];
			runTask.getSkill ().action (player, runTask);
			delay = runTask.getSkill ().getDelay (player) * 2;
            maxDelay = delay;
			tasks.Remove (runTask);
            listView.deleteTask(runTask);
			battleState = BattleState.IDLE;
            state.resetProgress();
		}

		/// <summary>
        /// ステートがIDLEの時に毎フレーム行う処理
        /// </summary>
		private void idleState(){
			delay -= Time.deltaTime;
            state.advanceProgress(Time.deltaTime / maxDelay);
			if (delay <= 0) {
				battleState = BattleState.ACTION;
			}
		}

		/// <summary>
        /// プレイヤーをマネージャに設定します
        /// </summary>
        /// <param name="player">Player.</param>
		public void setPlayer(IPlayable player){
			this.player = player;
			GameObject stateView = GameObject.Find("Canvas/StateView");
			GameObject stateNode = Instantiate((GameObject)Resources.Load("Prefabs/BattleStateNode"));
			stateNode.transform.SetParent(stateView.transform);
			state = stateNode.GetComponent<BattleStateNode>();
            state.setUser(player);
            goingPos = BattleManager.getInstance().searchCharacter(player);
			inputActiveSkillList ();
		}

		/// <summary>
        /// タスクを追加します
        /// </summary>
        /// <param name="addTask">Add task.</param>
        private void addTask(BattleTask addTask){
            tasks.Add(addTask);
            listView.setTask(addTask);

            battletaskIdCount++;

			chosenActiveSkill = null;
			inputActiveSkillList();
		}

		/// <summary>
        /// skillNodeが選択された時の処理
        /// </summary>
        /// <param name="chosenSkill">選択されたIActiveSkill</param>
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

		/// <summary>
        /// taretNodeが選択された時の処理
        /// </summary>
        /// <param name="targets">選択されたターゲットのリスト</param>
		public void targetChose(List<IBattleable> targets){
			BattleTask addingTask = new BattleTask(player.getUniqueId(), chosenActiveSkill, targets, battletaskIdCount);
            addTask(addingTask);
		}

		/// <summary>
		/// taretNodeが選択された時の処理
		/// </summary>
        /// <param name="pos">選択された場所</param>
        public void targetChose(FieldPosition pos) {
            BattleTask addingTask = new BattleTask(player.getUniqueId(), chosenActiveSkill, pos, battletaskIdCount);
			addTask(addingTask);
		}

		/// <summary>
        /// moveAreaNodeが選択された時の処理
        /// </summary>
        /// <param name="pos">選択されたFieldPostion</param>
		public void moveAreaChose(int move){
            int moveAmount = move;
			BattleTask addingTask = new BattleTask(player.getUniqueId(), chosenActiveSkill, move, battletaskIdCount);
            Debug.Log("before " + goingPos + "move " + move);
            goingPos += move;
            Debug.Log(goingPos + " , " + move);
            addTask(addingTask);
		}

		/// <summary>
        /// reactionSkillNodeが選択された時の処理
        /// </summary>
        /// <param name="chosenSkill">選択されたReactionSkill</param>
		public void reactionChose(ReactionSkill chosenSkill){
			reaction(chosenSkill);

			reactoinContents.SetActive (false);
			contents.SetActive (true);
			detachReactionContents ();

            backButton.gameObject.SetActive(isInputingBackButton);
            needToReaction = false;
		}

		/// <summary>
        /// ReactionSkillを使用します
        /// </summary>
        /// <param name="reactionSkill"> 使用するReactionSkill </param>
        private void reaction(ReactionSkill reactionSkill){
			IBattleable attacker = prosessingPair.Key;
			AttackSkill skill = prosessingPair.Value;
			int atk = skill.getAtk (attacker);
			int hit = skill.getHit(attacker);
			reactionSkill.reaction (player,atk,hit,skill.getAttackSkillAttribute());
			waitingReactionActiveSkills.Remove (prosessingPair);
			updateProsessingPair ();
		}

		/// <summary>
        /// スクロールビューにスキルのリストを表示します
        /// </summary>
		private void inputActiveSkillList(){
			detachContents();

            headerText.text = "スキル選択";

            backButton.gameObject.SetActive(false);
            isInputingBackButton = false;

			foreach(IActiveSkill skill in player.getActiveSkills()){
				GameObject node =  Instantiate ((GameObject)Resources.Load("Prefabs/ActiveSKillNode"));
				node.GetComponent<ActiveSkillNode> ().setState (this,skill);
				node.transform.SetParent (contents.transform);
			}

			GameObject escapeNode = Instantiate((GameObject)Resources.Load("Prefabs/EscapeNode"));
			escapeNode.GetComponent<EscapeNode>().setCharacter(player);
			escapeNode.transform.SetParent(contents.transform);
		}

		/// <summary>
        /// スクロールビューにターゲット選択岐を表示します
        /// </summary>
        /// <param name="extent">スキルの効果範囲</param>
        /// <param name="range">スキルの射程</param>
		private void inputTargetList(Extent extent,int range){
			detachContents ();
            headerText.text = chosenActiveSkill.getName() + "の対象を決定";
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
			}
		}

		/// <summary>
        /// スキルの効果範囲が単体の場合のターゲットをスクロールビューに表示します
        /// </summary>
        /// <param name="range">スキルの射程</param>
		private void inputSingleTargetList(int range){
			List<IBattleable> targets = BattleManager.getInstance().getCharacterInRange(player, range);
			backButton.gameObject.SetActive(true);
			isInputingBackButton = true;

			foreach (IBattleable target in targets) {
				if (target.Equals (player))
					continue;
				
				GameObject node = Instantiate ((GameObject)Resources.Load ("Prefabs/TargetNode"));
				node.GetComponent<TargetNode> ().setState (target, this);
				node.transform.SetParent (contents.transform);
			}
			
		}

		/// <summary>
        /// スキルの効果範囲が範囲の場合のターゲットをスクロールビューに表示します
        /// </summary>
        /// <param name="range">スキルの射程</param>
		private void inputAreaTargetList(int range){
			FieldPosition nowPos = BattleManager.getInstance().searchCharacter(player);
			backButton.gameObject.SetActive(true);
			isInputingBackButton = true;

            int index = BattleManager.getInstance().restructionPositionValue(nowPos, -range);
            int maxRange = BattleManager.getInstance().restructionPositionValue(nowPos, range);

			for (; index <= maxRange; index++) {
				if (index == (int)nowPos)
					continue;
				GameObject node = Instantiate ((GameObject)Resources.Load ("Prefabs/TargetNode"));
				node.GetComponent<TargetNode> ().setState ((FieldPosition)index, this);
				node.transform.SetParent (contents.transform);
			}
		}

		/// <summary>
        /// 移動スキルの場合の移動位置の選択岐を表示します
        /// </summary>
        /// <param name="move">移動力</param>
		private void inputMoveAreaList(int move){
			detachContents ();
            backButton.gameObject.SetActive(true);
            isInputingBackButton = true;

            headerText.text = chosenActiveSkill.getName() + "の移動先を決定";

            int index = BattleManager.getInstance().restructionPositionValue(goingPos,-move);
            int maxpos = BattleManager.getInstance().restructionPositionValue(goingPos, move);

            Debug.Log("index : " + index + " max : " + maxpos);

			for (; index <= maxpos; index++) {
                if (index == (int)goingPos)
                    continue;
				GameObject node = Instantiate ((GameObject)Resources.Load ("Prefabs/MoveAreaNode"));
                node.GetComponent<MoveAreaNode> ().setState (index - (int)goingPos, this);
				node.transform.SetParent (contents.transform);
			}
		}

		/// <summary>
        /// スクロールビューにリアクションスキルのリストを表示します
        /// </summary>
		private void inputReactionSkillList(){
			detachReactionContents();

            IBattleable attacker = prosessingPair.Key;
            AttackSkill attackedSkill = prosessingPair.Value;

            headerText.text = attacker.getName() + "の" + attackedSkill.getName() + "へのリアクションを決定";

            backButton.gameObject.SetActive(false);
			contents.SetActive (false);
			foreach(ReactionSkill skill in player.getReactionSKills()){
				GameObject node = Instantiate((GameObject)Resources.Load ("Prefabs/ReactionSkillNode"));
				node.GetComponent<ReactionSkillNode> ().setState (skill,this);
				node.transform.SetParent (reactoinContents.transform);
			}
			reactoinContents.SetActive (true);
		}

		/// <summary>
        /// contentsオブジェクトの子ノードを削除します
        /// </summary>
		private void detachContents(){
			Transform children = contents.GetComponentInChildren<Transform> ();
			foreach(Transform child in children){
				MonoBehaviour.Destroy (child.gameObject);
			}
			contents.transform.DetachChildren ();
		}


		/// <summary>
		///passiveContentsオブジェクトの子ノードを削除します
		/// </summary>
		private void detachReactionContents(){
			Transform children = reactoinContents.GetComponentInChildren<Transform> ();
			foreach(Transform child in children){
				MonoBehaviour.Destroy (child.gameObject);
			}
			reactoinContents.transform.DetachChildren ();
		}

		/// <summary>
        /// リアクション処理中のスキルを更新します
        /// </summary>
		private void updateProsessingPair(){
			if (waitingReactionActiveSkills.Count > 0) {
				prosessingPair = waitingReactionActiveSkills[0];
				inputReactionSkillList ();
                reactionLimit = prosessingPair.Value.getDelay(prosessingPair.Key);
				needToReaction = true;
			} else {
				needToReaction = false;
			}
		}

        /// <summary>
        /// タスクをキャンセルします
        /// </summary>
        /// <param name="task"> キャンセルしたいタスク </param>
		public void finishedTask(BattleTask task) {
            tasks.Remove(task);

		}

        public void canseledTask(BattleTask task){
			if (task.getSkill().getActiveSkillType() == ActiveSkillType.MOVE) {
				goingPos -= task.getMove();
			}
        }

        /// <summary>
        /// 戻るボタンが選ばれた時の処理です
        /// </summary>
        public void backChose(){
            inputActiveSkillList();
            isInputingBackButton = true;
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
            Destroy (view.gameObject);
            Destroy (listView.gameObject);
            Destroy (state.gameObject);
		}
		#endregion


	}
}