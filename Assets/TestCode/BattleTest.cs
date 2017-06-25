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
	public ActiveSkillMasterManager sManager;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.S)) {
			startBattle ();
		}
	}

	private void startBattle(){
		en = manager.getEnemyFromId (0);
		hero = new Hero (jma.getJobFromId(0),con);
		Debug.Log ("cleared" + hero.getName() + en.getName());

//		hero.addSkill (sManager.getActiveSkillFromId(0));
//		hero.addSkill (sManager.getActiveSkillFromId(1));
//
		con.setCharacter (hero);
		con2.setCharacter (en);

		IEnemyAI ai = EnemyAISummarizingManager.getInstance ().getAiFromId (0, en);
		Debug.Log (ai.ToString());

//		hero.encount ();
//		en.encount ();
	}
}
