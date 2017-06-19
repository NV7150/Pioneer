using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using skill;
using character;

namespace battleSystem{
	public class BattleNodeController : MonoBehaviour {
		private List<BattleTask> tasks = new List<BattleTask>();
		public GameObject contents;
		private IPlayable player;
		public GameObject view;

		private ActiveSkill chosenActiveSkill;
		private List<IBattleable> chosenTargets;

		// Use this for initialization
		void Start () {
			GameObject canvas = GameObject.Find ("Canvas");
			view.transform.SetParent (canvas.transform);
		}
		
		// Update is called once per frame
		void Update () {
			
		}

		private void setPlayer(IPlayable player){
			this.player = player;
			inputActiveSkillList ();
		}

		private void setTask(ActiveSkill skill,List<IBattleable> targets){
			tasks.Add(new BattleTask(player.getUniqueId(),skill,targets));
		}

		public List<BattleTask> getTasks(){
			return tasks;
		}

		public void skillChose(ActiveSkill chosedSkill){
			this.chosenActiveSkill = chosedSkill;
		}

		public void targetChose(List<IBattleable> targets){
			this.chosenTargets = targets;
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


		private void detachContents(){
			Transform children = contents.GetComponentInChildren<Transform> ();
			foreach(Transform child in children){
				MonoBehaviour.Destroy (child.gameObject);
			}
			contents.transform.DetachChildren ();
		}
	}
}