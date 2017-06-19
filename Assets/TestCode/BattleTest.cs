using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using character;
using battleSystem;
using masterdata;
using parameter;

public class BattleTest : MonoBehaviour {
	public Hero hero;
	public Enemy en;
	public EnemyMasterManager manager;
	public Container con;
	public Container con2;

	// Use this for initialization
	void Start () {
		en = manager.getEnemyFromId (0);
		hero = new Hero (new Civil(),con);
		Debug.Log ("cleared" + hero.getName() + en.getName());

		con.setCharacter (hero);
		con2.setCharacter (en);

		hero.encount ();
		en.encount ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
