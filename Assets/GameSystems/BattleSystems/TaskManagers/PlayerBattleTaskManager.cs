﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

using Skill;
using Character;
using MasterData;
using SelectView;
using Item;

using ActiveSkillType = Skill.ActiveSkillParameters.ActiveSkillType;
using Extent = Skill.ActiveSkillParameters.Extent;
using BattleAbility = Parameter.CharacterParameters.BattleAbility;

namespace BattleSystem{
    public class PlayerBattleTaskManager : MonoBehaviour, IBattleTaskManager {
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
        /// <summary> リアクション待ちのスキルのリスト </summary>
        private List<KeyValuePair<IBattleable, AttackSkill>> waitingDecideReactionSkills = new List<KeyValuePair<IBattleable, AttackSkill>>();

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

        /// <summary> ディレイ抜きにして行く予定のポジション </summary>
        private FieldPosition goingPos;

        /// <summary> スキルをkeyとする現在表示中のターゲットラインのdictionary </summary>
        private Dictionary<IBattleable, List<GameObject>> targetLines = new Dictionary<IBattleable, List<GameObject>>();

        private List<KeyValuePair<ReactionSkill, KeyValuePair<IBattleable, AttackSkill>>> waitingProgressSkills = new List<KeyValuePair<ReactionSkill, KeyValuePair<IBattleable, AttackSkill>>>();

        private bool choosingReaction = false;

        private IItem choseItem;

        private ManagerState currentManagerState = ManagerState.SKILL;

        private GameObject battleItemNodePrefab;

        private GameObject managerStateNodePrefab;

        private void Awake() {
            battleItemNodePrefab = (GameObject)Resources.Load("Prefabs/BattleItemNode");
            managerStateNodePrefab = (GameObject)Resources.Load("Prefabs/ManagerStateNode");
        }

        // Use this for initialization
        void Start() {
            GameObject battleView = GameObject.Find("Canvas/BattleView");
            view.transform.SetParent(battleView.transform);
            reactoinContents.SetActive(false);

            GameObject taskView = GameObject.Find("Canvas/TaskView");
            GameObject battleListNode = Instantiate((GameObject)Resources.Load("Prefabs/BattleTaskListView"));
            battleListNode.transform.SetParent(taskView.transform);
            listView = battleListNode.GetComponent<BattleTaskListView>();
            listView.setManager(this);
        }

        // Update is called once per frame
        void Update() {
            if (player.getHp() <= 0) {
                BattleManager.getInstance().deadCharacter(player);
            }

            if (needToReaction) {
                reactionState();
            } else if (battleState == BattleState.ACTION && tasks.Count > 0) {
                actionState();
            }
            if (battleState == BattleState.IDLE) {
                idleState();
            }
        }

        /// <summary>
        /// リアクションが必要な時に毎フレーム行う処理
        /// </summary>
        private void reactionState() {
            reactionLimit -= Time.deltaTime;
            if (reactionLimit <= 0) {
                var reactionedSkill = waitingProgressSkills[0].Key;
                reaction();
                var missReaction = ReactionSkillMasterManager.getReactionSkillFromId(2);

                if (reactionedSkill.Equals(missReaction)) {
                    reactoinContents.SetActive(false);
                    contents.SetActive(true);
                    detachReactionContents();
                    backButton.gameObject.SetActive(isInputingBackButton);
                    choosingReaction = false;
                    updateProsessingPair();
                }
            }
        }

        /// <summary>
        /// ステートがACTIONの時に毎フレーム行う処理
        /// </summary>
        private void actionState() {
            BattleTask runTask = tasks[0];
            if (runTask.getIsSkill()) {
                IActiveSkill runSkill = runTask.getSkill();
                runSkill.action(player, runTask);

                delay = runTask.getSkill().getDelay(player) * 2;
                maxDelay = delay;
            }else{
                IItem runItem = runTask.getItem();
                player.getInventory().useItem(runItem,player);

                float delayBonus = (float)player.getAbilityContainsBonus(BattleAbility.AGI) / 20;
				delayBonus = (delayBonus < 1.0f) ? delayBonus : 1.0f;
				delay = 2.0f - delayBonus;
                maxDelay = delay;
            }

            tasks.Remove(runTask);
            listView.deleteTask(runTask);

            battleState = BattleState.IDLE;
            state.resetProgress();

            updateTargetLine();
        }

        /// <summary>
        /// 自分から出すターゲットラインを更新します
        /// </summary>
        private void updateTargetLine() {
            if (tasks.Count <= 0)
                return;

            BattleTask task = tasks[0];

            if (task.getIsSkill()) {
                IActiveSkill useSkill = task.getSkill();
                if (ActiveSkillSupporter.isAffectSkill(useSkill)) {
                    List<IBattleable> targets = task.getTargets();
                    drawTargetingLine(player, targets, useSkill.getName(),useSkill.isFriendly());
                }
            }else{
                List<IBattleable> targets = task.getTargets();
                var item = task.getItem();
                drawTargetingLine(player,targets,item.getName(),true);
            }
        }

        /// <summary>
        /// ステートがIDLEの時に毎フレーム行う処理
        /// </summary>
        private void idleState() {
            delay -= Time.deltaTime;
            state.advanceProgress(Time.deltaTime / maxDelay);
            if (delay <= 0) {
                battleState = BattleState.ACTION;
                deleteTargetingLine(player);
            }
        }

        /// <summary>
        /// プレイヤーをマネージャに設定します
        /// </summary>
        /// <param name="player">Player.</param>
        public void setPlayer(IPlayable player) {
            this.player = player;
            GameObject stateView = GameObject.Find("Canvas/StateView");
            GameObject stateNode = Instantiate((GameObject)Resources.Load("Prefabs/BattleStateNode"));
            Debug.Log(stateNode + " and " + stateView);
            stateNode.transform.SetParent(stateView.transform);
            state = stateNode.GetComponent<BattleStateNode>();
            state.setUser(player);
            goingPos = BattleManager.getInstance().searchCharacter(player);
            inputActiveSkillList();
        }

        /// <summary>
        /// タスクを追加します
        /// </summary>
        /// <param name="addTask">Add task.</param>
        private void addTask(BattleTask addTask) {
            tasks.Add(addTask);
            listView.setTask(addTask);

            battletaskIdCount++;

            chosenActiveSkill = null;
            inputActiveSkillList();

            updateTargetLine();
        }

        /// <summary>
        /// skillNodeが選択された時の処理
        /// </summary>
        /// <param name="chosenSkill">選択されたIActiveSkill</param>
        public void skillChose(IActiveSkill chosenSkill) {
            this.chosenActiveSkill = chosenSkill;

            if (ActiveSkillSupporter.isAffectSkill(chosenSkill)) {
                Extent extent = ActiveSkillSupporter.searchExtent(chosenSkill);
                int range = ActiveSkillSupporter.searchRange(chosenSkill, player);
                inputTargetList(extent, range);
            } else if (chosenSkill.getActiveSkillType() == ActiveSkillType.MOVE) {
                int move = ActiveSkillSupporter.searchMove(chosenSkill, player);
                inputMoveAreaList(move);
            } else {
                throw new NotSupportedException("Unkonwn skillType");
            }
        }

        public void itemChose(IItem item) {
            this.choseItem = item;
            inputTargetList(Extent.SINGLE,0);
        }

        public void stateChanged(){
            if(currentManagerState == ManagerState.ITEM){
                this.currentManagerState = ManagerState.SKILL;
                inputActiveSkillList();
            }else if(currentManagerState == ManagerState.SKILL){
                this.currentManagerState = ManagerState.ITEM;
                inputItemList();
            }
        }

        /// <summary>
        /// taretNodeが選択された時の処理
        /// </summary>
        /// <param name="targets">選択されたターゲットのリスト</param>
        public void targetChose(List<IBattleable> targets) {
            BattleTask addingTask;
            if (currentManagerState == ManagerState.SKILL) {
                addingTask = new BattleTask(player.getUniqueId(), chosenActiveSkill, targets, battletaskIdCount);
            }else{
                addingTask = new BattleTask(player.getUniqueId(),choseItem,targets[0],battletaskIdCount);
            }
            addTask(addingTask);
        }

        /// <summary>
        /// taretNodeが選択された時の処理
        /// </summary>
        /// <param name="pos">選択された場所</param>
        public void targetChose(FieldPosition pos) {
            if (currentManagerState == ManagerState.ITEM)
                throw new NotSupportedException("area extent item hasn't impremented yet");

            BattleTask addingTask = new BattleTask(player.getUniqueId(), chosenActiveSkill, pos, battletaskIdCount);
            addTask(addingTask);
        }

        /// <summary>
        /// moveAreaNodeが選択された時の処理
        /// </summary>
        public void moveAreaChose(int move) {
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
        public void reactionChose(ReactionSkill chosenSkill) {
            Debug.Log("<color=greeen>into reactionChose</color>");
            waitingProgressSkills[0] = new KeyValuePair<ReactionSkill, KeyValuePair<IBattleable, AttackSkill>>(chosenSkill, waitingProgressSkills[0].Value);

            reactoinContents.SetActive(false);
            contents.SetActive(true);
            detachReactionContents();

            backButton.gameObject.SetActive(isInputingBackButton);
            choosingReaction = false;
            updateProsessingPair();
            Debug.Log("<color=blue>into chosen</color>" + waitingProgressSkills[0].Key.getName() + "" + waitingProgressSkills[0].Value.Key.getName());
        }

        /// <summary>
        /// ReactionSkillを使用します
        /// </summary>
        private void reaction() {
            Debug.Log("<color=red>intoreaction</color>");
            ReactionSkill reactionSkill = waitingProgressSkills[0].Key;
            IBattleable attacker = waitingProgressSkills[0].Value.Key;
            AttackSkill skill = waitingProgressSkills[0].Value.Value;
            deleteTargetingLine(attacker);
            int atk = skill.getAtk(attacker);
            int hit = skill.getHit(attacker);
            reactionSkill.reaction(player, atk, hit, skill.getAttackSkillAttribute());
            waitingProgressSkills.Remove(waitingProgressSkills[0]);
            choosingReaction = false;
            updateProsessingPair();
        }

        /// <summary>
        /// スクロールビューにスキルのリストを表示します
        /// </summary>
        private void inputActiveSkillList() {
            detachContents();

            headerText.text = "スキル選択";

            backButton.gameObject.SetActive(false);
            isInputingBackButton = false;

            var managerStateNode = Instantiate(managerStateNodePrefab).GetComponent<ManagerStateNode>();
            managerStateNode.setState("アイテムへ",this);
            managerStateNode.transform.SetParent(contents.transform);

            foreach (IActiveSkill skill in player.getActiveSkills()) {
                GameObject node = Instantiate((GameObject)Resources.Load("Prefabs/ActiveSKillNode"));
                node.GetComponent<ActiveSkillNode>().setState(this, skill);
                node.transform.SetParent(contents.transform);
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
        private void inputTargetList(Extent extent, int range) {
            detachContents();
            if (this.currentManagerState == ManagerState.SKILL) {
                headerText.text = chosenActiveSkill.getName();
            }else{
                headerText.text = choseItem.getName();
            }
            switch (extent) {
                case Extent.SINGLE:
                    inputSingleTargetList(range);
                    break;
                case Extent.AREA:
                    inputAreaTargetList(range);
                    break;
                case Extent.ALL:
                    targetChose(BattleManager.getInstance().getJoinedBattleCharacter());
                    break;
            }
        }

        /// <summary>
        /// スキルの効果範囲が単体の場合のターゲットをスクロールビューに表示します
        /// </summary>
        /// <param name="range">スキルの射程</param>
        private void inputSingleTargetList(int range) {
            List<IBattleable> targets = BattleManager.getInstance().getCharacterInRange(player, range);
            backButton.gameObject.SetActive(true);
            isInputingBackButton = true;

			foreach (IBattleable target in targets) {
				bool inputOnlyPlayable = currentManagerState == ManagerState.ITEM;
				if (!(target is IPlayable) && inputOnlyPlayable)
					continue;
                
                bool inputSelf = inputOnlyPlayable || !chosenActiveSkill.isFriendly();
                if (target.Equals(player) && !inputSelf)
                    continue;

                GameObject node = Instantiate((GameObject)Resources.Load("Prefabs/TargetNode"));
                node.GetComponent<TargetNode>().setState(target, this);
                node.transform.SetParent(contents.transform);
            }

        }

        /// <summary>
        /// スキルの効果範囲が範囲の場合のターゲットをスクロールビューに表示します
        /// </summary>
        /// <param name="range">スキルの射程</param>
        private void inputAreaTargetList(int range) {
            FieldPosition nowPos = BattleManager.getInstance().searchCharacter(player);
            backButton.gameObject.SetActive(true);
            isInputingBackButton = true;

            int index = BattleManager.getInstance().restructionPositionValue(nowPos, -range);
            int maxRange = BattleManager.getInstance().restructionPositionValue(nowPos, range);

            for (; index <= maxRange; index++) {
                if (index == (int)nowPos)
                    continue;
                GameObject node = Instantiate((GameObject)Resources.Load("Prefabs/TargetNode"));
                node.GetComponent<TargetNode>().setState((FieldPosition)index, this);
                node.transform.SetParent(contents.transform);
            }
        }

        /// <summary>
        /// 移動スキルの場合の移動位置の選択岐を表示します
        /// </summary>
        /// <param name="move">移動力</param>
        private void inputMoveAreaList(int move) {
            detachContents();
            backButton.gameObject.SetActive(true);
            isInputingBackButton = true;

            headerText.text = chosenActiveSkill.getName() + "の移動先を決定";

            int index = BattleManager.getInstance().restructionPositionValue(goingPos, -move);
            int maxpos = BattleManager.getInstance().restructionPositionValue(goingPos, move);

            for (; index <= maxpos; index++) {
                if (index == (int)goingPos)
                    continue;
                GameObject node = Instantiate((GameObject)Resources.Load("Prefabs/MoveAreaNode"));
                node.GetComponent<MoveAreaNode>().setState(index - (int)goingPos, this);
                node.transform.SetParent(contents.transform);
            }
        }

        /// <summary>
        /// スクロールビューにリアクションスキルのリストを表示します
        /// </summary>
        private void inputReactionSkillList() {
            detachReactionContents();

            IBattleable attacker = waitingProgressSkills[0].Value.Key;
            AttackSkill attackedSkill = waitingProgressSkills[0].Value.Value;

            headerText.text = attacker.getName() + "の" + attackedSkill.getName() + "へのリアクションを決定";

            backButton.gameObject.SetActive(false);
            contents.SetActive(false);
            foreach (ReactionSkill skill in player.getReactionSkills()) {
                GameObject node = Instantiate((GameObject)Resources.Load("Prefabs/ReactionSkillNode"));
                node.GetComponent<ReactionSkillNode>().setState(skill, this);
                node.transform.SetParent(reactoinContents.transform);
            }
            reactoinContents.SetActive(true);
        }

        private void inputItemList() {
            detachContents();

			backButton.gameObject.SetActive(false);
			isInputingBackButton = false;

			var managerStateNode = Instantiate(managerStateNodePrefab).GetComponent<ManagerStateNode>();
			managerStateNode.setState("スキルへ", this);
			managerStateNode.transform.SetParent(contents.transform);

            var inventoryItems = new List<IItem>(player.getInventory().getItems());
            foreach(IItem item in inventoryItems){
                var node = Instantiate(battleItemNodePrefab).GetComponent<BattleItemNode>();
                node.setItem(item,this);
                node.transform.SetParent(contents.transform);
            }
        }

        /// <summary>
        /// ターゲットラインを対象の間に引きます
        /// </summary>
        /// <param name="user">スキル使用者</param>
        /// <param name="targets">スキル対象者のリスト</param>
        /// <param name="skill">使用するスキル</param>
        private void drawTargetingLine(IBattleable user, List<IBattleable> targets, string lineName,bool isFriendly) {
            GameObject targetingLinePrefab = (GameObject)Resources.Load("Prefabs/TargetLine");
            GameObject attackerModel = user.getContainer().getModel();

            List<GameObject> addedLines = new List<GameObject>();
            foreach (IBattleable target in targets) {
                GameObject targetingLine = Instantiate(targetingLinePrefab);

                TargetLine line = targetingLine.GetComponent<TargetLine>();
                GameObject targetModel = target.getContainer().getModel();
                line.setState(attackerModel, targetModel, lineName, isFriendly);

                addedLines.Add(targetingLine);
            }

            if (targetLines.ContainsKey(user)) {
                targetLines[user].AddRange(addedLines);
            } else {
                targetLines.Add(user, addedLines);
            }
        }

        /// <summary>
        /// スキルのターゲットラインを削除します
        /// </summary>
        private void deleteTargetingLine(IBattleable user) {
            if (targetLines.ContainsKey(user)) {
                Destroy(targetLines[user][0]);
                targetLines[user].Remove(targetLines[user][0]);
            }
        }

        //private void unactivateLines(){
        //    var keys = targetLines.Keys;
        //    foreach(IBattleable bal in keys){
        //        foreach(GameObject line in targetLines[bal]){
        //            line.SetActive(false);
        //        }
        //    }
        //}

        //private void activateLines(){
        //    var keys = targetLines.Keys;
        //    foreach(IBattleable bal in keys){
        //        foreach(GameObject line in targetLines[bal]){
        //            line.SetActive(true);
        //        }
        //    }
        //}

        /// <summary>
        /// contentsオブジェクトの子ノードを削除します
        /// </summary>
        private void detachContents() {
            Transform children = contents.GetComponentInChildren<Transform>();
            foreach (Transform child in children) {
                Destroy(child.gameObject);
            }
            contents.transform.DetachChildren();
        }


        /// <summary>
        ///passiveContentsオブジェクトの子ノードを削除します
        /// </summary>
        private void detachReactionContents() {
            Transform children = reactoinContents.GetComponentInChildren<Transform>();
            foreach (Transform child in children) {
                Destroy(child.gameObject);
            }
            reactoinContents.transform.DetachChildren();
        }

        /// <summary>
        /// リアクション処理中のスキルを更新します
        /// </summary>
        private void updateProsessingPair() {
            if (!choosingReaction) {
                if (waitingDecideReactionSkills.Count > 0) {
                    var missReaction = ReactionSkillMasterManager.getReactionSkillFromId(2);
                    var prosessingPair = waitingDecideReactionSkills[0];
                    waitingProgressSkills.Add(new KeyValuePair<ReactionSkill, KeyValuePair<IBattleable, AttackSkill>>(missReaction, prosessingPair));
                    waitingDecideReactionSkills.Remove(prosessingPair);

                    inputReactionSkillList();

                    reactionLimit = prosessingPair.Value.getDelay(prosessingPair.Key);
                    needToReaction = true;
                    choosingReaction = true;
                    Debug.Log("1" + waitingProgressSkills[0].Key.getName());
                } else if (waitingProgressSkills.Count <= 0) {
                    needToReaction = false;
                }
            }
        }

        /// <summary>
        /// タスクを削除します
        /// </summary>
        /// <param name="task"> 削除したいタスク </param>
		public void finishedTask(BattleTask task) {
            tasks.Remove(task);
        }

        public void canseledTask(BattleTask task) {
            if (task.getSkill().getActiveSkillType() == ActiveSkillType.MOVE) {
                goingPos -= task.getMove();
            }
            if (ActiveSkillSupporter.isAffectSkill(task.getSkill())) {
                deleteTargetingLine(player);
            }
        }

        /// <summary>
        /// 戻るボタンが選ばれた時の処理です
        /// </summary>
        public void backChose() {
            inputActiveSkillList();
            isInputingBackButton = true;
        }
        #region IBattleTaskManager implementation

        public void deleteTaskFromTarget(IBattleable target) {
            foreach (BattleTask task in tasks) {
                foreach (IBattleable bal in task.getTargets()) {
                    if (bal.Equals(target))
                        tasks.Remove(task);
                }
            }
        }

        public void offerReaction(IBattleable attacker, AttackSkill skill) {
            KeyValuePair<IBattleable, AttackSkill> pair = new KeyValuePair<IBattleable, AttackSkill>(attacker, skill);
            waitingDecideReactionSkills.Add(pair);

            List<IBattleable> drawLineTarget = new List<IBattleable>() { player };
            drawTargetingLine(attacker, drawLineTarget, skill.getName(),skill.isFriendly());

            updateProsessingPair();
        }

        public bool isHavingTask() {
            return tasks.Count > 0;
        }

        public void win() {
            //(実装)まだです
        }

        public void finished() {
            Destroy(view.gameObject);
            Destroy(listView.gameObject);
            Destroy(state.gameObject);
        }
        #endregion

        private enum ManagerState {
            SKILL, ITEM
        }
    }
}