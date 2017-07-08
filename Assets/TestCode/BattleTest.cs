using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Character;
using BattleSystem;
using MasterData;
using Parameter;
using AI;


public class BattleTest : MonoBehaviour {
	public Hero hero;
	public Enemy en;
	public EnemyMasterManager manager;
	public Container con;
	public Container con2;
	public JobMasterManager jma;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.S) && Input.GetKey(KeyCode.A)) {
			startBattle ();
		}
	}

	private void startBattle(){
		en = EnemyMasterManager.getEnemyFromId (0);
		hero = new Hero (JobMasterManager.getJobFromId(0),con);
		Debug.Log ("cleared" + hero.getName() + en.getName());

		hero.addSkill (ReactionSkillMasterManager.getReactionSkillFromId(0));
		hero.addSkill (ReactionSkillMasterManager.getReactionSkillFromId(1));
		hero.addSkill (AttackSkillMasterManager.getAttackSkillFromId(0));
		hero.addSkill (AttackSkillMasterManager.getAttackSkillFromId(1));
		hero.addSkill (MoveSkillMasterManager.getMoveSkillFromId(0));

		con.setCharacter (hero);
		con2.setCharacter (en);

		hero.encount ();
		en.encount ();

//		ActiveSkillSet skillset = ActiveSkillSetMasterManager.getActiveSkillSetFromId (0);
//
//		Debug.Log (skillset.getSkillFromSkillCategory(ActiveSkillCategory.NORMAL).ToString() + "" + skillset.getSkillFromSkillCategory(ActiveSkillCategory.MOVE));
//
//		PassiveSkillSet pskillset = PassiveSkillSetMasterManager.getPassiveSkillSetFromId(0);
//		Debug.Log(pskillset.getSkillFromCategory(PassiveSkillCategory.DODGE).ToString() + "" + pskillset.getSkillFromCategory(PassiveSkillCategory.DODGE).ToString());
	}
}
