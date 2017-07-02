using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Skill;
using Character;

namespace BattleSystem{
	public class PlayerBattleTaskManager : MonoBehaviour ,IBattleTaskManager{
		private List<BattleTask> tasks = new List<BattleTask>();
		public GameObject contents;
		public GameObject passiveContents;
		private IPlayable player;
		public GameObject view;

		private ActiveSkill chosenActiveSkill;
		private PassiveSkill chosenPassiveSKill;

		// Use this for initialization
		void Start () {
			GameObject canvas = GameObject.Find ("Canvas/BattleView");
			view.transform.SetParent (canvas.transform);
			passiveContents.SetActive (false);
		}
		
		// Update is called once per frame
		void Update () {
			
		}

		public void setPlayer(IPlayable player){
			this.player = player;
			inputActiveSkillList ();
		}

		private void setTask(ActiveSkill skill,List<IBattleable> targets){
			tasks.Add(new BattleTask(player.getUniqueId(),skill,targets));
		}

		public void skillChose(ActiveSkill chosenSkill){
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

		public void targetChose(List<IBattleable> targets){
			tasks.Add (new BattleTask(player.getUniqueId(),chosenActiveSkill,targets));
			chosenActiveSkill = null;
			inputActiveSkillList ();
		}

		public void moveAreaChose(FieldPosition pos){
			int move = (int) (BattleManager.getInstance ().searchCharacter(player) - pos);
			tasks.Add (new BattleTask(player.getUniqueId(),chosenActiveSkill,move));
			chosenActiveSkill = null;
			inputActiveSkillList ();
		}

		public void passiveChose(PassiveSkill chosenSkill){
			this.chosenPassiveSKill = chosenSkill;

			passiveContents.SetActive (false);
			contents.SetActive (true);
			detachContents ();
		}

		private void inputActiveSkillList(){
			detachContents ();

			foreach(ActiveSkill skill in player.getActiveSkills()){
				GameObject node =  Instantiate ((GameObject)Resources.Load("Prefabs/ActiveSKillNode"));
				node.GetComponent<ActiveSkillNode> ().setState (this,skill);
				node.transform.SetParent (contents.transform);
			}
		}

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

		private void inputSingleTargetList(){
			List<IBattleable> targets = BattleManager.getInstance ().getCharacterInRange (player, chosenActiveSkill.getRange ());
			foreach (IBattleable target in targets) {
				GameObject node = Instantiate ((GameObject)Resources.Load ("Prefabs/TargetNode"));
				node.GetComponent<TargetNode> ().setState (target, this);
				node.transform.SetParent (contents.transform);
			}
		}

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

		private void detachContents(){
			Transform children = contents.GetComponentInChildren<Transform> ();
			foreach(Transform child in children){
				MonoBehaviour.Destroy (child.gameObject);
			}
			contents.transform.DetachChildren ();
		}

		private void detachPassiveContents(){
			Transform children = passiveContents.GetComponentInChildren<Transform> ();
			foreach(Transform child in children){
				MonoBehaviour.Destroy (child.gameObject);
			}
			passiveContents.transform.DetachChildren ();
		}

		private void deleteTask (BattleTask task) {
			tasks.Remove (task);
		}

		#region IBattleTaskManager implementation

		public BattleTask getTask () {
			if (tasks.Count != 0) {
				return tasks [0];
			} else {
				//とりあえず
				return null;
			}
		}

		public void deleteTaskFromTarget (IBattleable target) {
			foreach(BattleTask task in tasks){
				foreach(IBattleable bal in task.getTargets()){
					if (bal.Equals (target))
						tasks.Remove (task);
				}
			}
		}

		public void offerPassive () {
			detachPassiveContents ();
			contents.SetActive (false);
			foreach(PassiveSkill skill in player.getPassiveSKills()){
				GameObject node = Instantiate((GameObject)Resources.Load ("Prefabs/PassiveSkillNode"));
				node.GetComponent<PassiveSkillNode> ().setState (skill,this);
				node.transform.SetParent (passiveContents.transform);
			}
			passiveContents.SetActive (true);
		}

		public bool isHavingTask () {
			return tasks.Count > 0;
		}

		public PassiveSkill getPassive (IBattleable attacker, ActiveSkill skill) {
			passiveContents.SetActive (false);
			contents.SetActive (true);
			detachContents ();

			return chosenPassiveSKill;
		}
		#endregion


	}
}