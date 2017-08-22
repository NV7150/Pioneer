using System.Collections;using System.Collections.Generic;using UnityEngine;using Character;using Item;using MasterData;using Skill;using Parameter;public class TalkTest : MonoBehaviour {    public Container con;    public Container plCon;    public Container maCon;    public Container matsuco;    public HealItem hphItem;    public HealItem mphItem;    public Player hero;	// Use this for initialization	void Start () {        //List<Identity> identities = new List<Identity>(){
        //    IdentityMasterManager.getIdentityFromId(0),
        //    IdentityMasterManager.getIdentityFromId(0),
        //    IdentityMasterManager.getIdentityFromId(2)        //};        ////hero = new Player(JobMasterManager.getJobFromId(0),HumanityMasterManager.getHumanityFromId(0),identities, plCon);        //plCon.setCharacter(hero);        //hero.dammage(20,Skill.ActiveSkillParameters.AttackSkillAttribute.PHYSICAL);        //hero.minusMp(20);        //Debug.Log("beforeheal hp " + hero.getHp() + "/" + hero.getMaxHp() + " mp " + hero.getMp() + "/" + hero.getMaxMp());        //hero.addSkill(ReactionSkillMasterManager.getReactionSkillFromId(0));        //Citizen civ = CitizenMasterManager.getCitizenFromId(0);        //con.setCharacter(civ);	}		// Update is called once per frame	void Update () {  //      if (Input.GetKeyDown(KeyCode.H)) {
  //          hphItem.use(hero);
  //          mphItem.use(hero);
  //          Debug.Log("afterheal hp " + hero.getHp() + "/" + hero.getMaxHp() + " mp " + hero.getMp() + "/" + hero.getMaxMp());  //      }else if(Input.GetKeyDown(KeyCode.I)){
		//	hphItem = HealItemMasterManager.getHealItemFromId(0);
		//	mphItem = HealItemMasterManager.getHealItemFromId(1);
  //          Debug.Log("hph " + hphItem.getHeal());
		//	hero.addItem(hphItem);
		//	hero.addSkill(AttackSkillMasterManager.getAttackSkillFromId(0));  //      }else if(Input.GetKeyDown(KeyCode.L)){  //          //ClientMasterManager.getClientFromId(0);  //      }else if(Input.GetKeyDown(KeyCode.P)){
		//	hero.getFlagList().addEnemyKilled(EnemyMasterManager.getEnemyFromId(0));
		//	hero.getFlagList().addEnemyKilled(EnemyMasterManager.getEnemyFromId(1));  //      }  //      if(Input.GetKeyDown(KeyCode.O)){  //          Debug.Log("called");  //          progressData();  //      }

		//if (Input.GetKeyDown(KeyCode.M)) {
			//Debug.Log("called");        //    saveTest();        //}        //if(Input.GetKeyDown(KeyCode.Q)){        //    var enmey = EnemyMasterManager.getEnemyFromId(0);        //    enmey.getContainer().transform.position = hero.getContainer().transform.position + new Vector3(10, 0, 10);        //}	}

	private void progressData() {
        var progress = HealItemMasterManager.getHealItemFromId(0);        for (int i = 0; i < 100; i++)
            progress.use(hero);
	}    private void saveTest(){
		PioneerManager.getInstance().finished();
		HealItemMasterManager.updateProgress();

        Debug.Log("heal " + HealItemMasterManager.getHealItemFromId(0).getHeal());    }}