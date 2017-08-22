using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Character;
using BattleSystem;
using MasterData;
using Parameter;
using AI;


public class BattleTest : MonoBehaviour {
	public Player hero;
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
        if(Input.GetKeyDown(KeyCode.N)){
            setBattle();
        }
	}

    private void setBattle(){
		en = EnemyMasterManager.getEnemyFromId(0);
        var list = new List<Identity>(){
            IdentityMasterManager.getIdentityFromId(0)
        };
        //hero = new Player(JobMasterManager.getJobFromId(0),HumanityMasterManager.getHumanityFromId(0), list,con);

		hero.addSkill(ReactionSkillMasterManager.getReactionSkillFromId(0));
		hero.addSkill(ReactionSkillMasterManager.getReactionSkillFromId(1));
		hero.addSkill(AttackSkillMasterManager.getAttackSkillFromId(0));
		hero.addSkill(AttackSkillMasterManager.getAttackSkillFromId(1));
		hero.addSkill(MoveSkillMasterManager.getMoveSkillFromId(0));
		hero.addSkill(BufSkillMasterManager.getBufSkillFromId(0));
		hero.addSkill(DebufSkillMasterManager.getDebufSkillFromId(0));
		hero.addSkill(HealSkillMasterManager.getHealSkillFromId(0));
        hero.addSkill(DebufSkillMasterManager.getDebufSkillFromId(2));
        hero.addSkill(AttackSkillMasterManager.getAttackSkillFromId(7));

		con.setCharacter(hero);
    }

	private void startBattle(){
        Debug.Log(hero);

		BattleManager.getInstance ().startNewBattle ();

		hero.encount ();
		en.encount ();
	}
}
