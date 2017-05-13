using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using character;
using parameter;
using battleSystem;

public class ContainerTest : MonoBehaviour {
	public Container heroCon;
	public Container enemyCOn;

	// Use this for initialization
	void Start () {
		BattleManager.getInstance ().StartNewBattle ((heroCon.transform.position + enemyCOn.transform.position) / 2);
		Hero hero = new Hero (new Civil(),new Tester(),new Test(),heroCon);
		Enemy enemy = new Goblin(enemyCOn);
		heroCon.setCharacter (hero);
		enemyCOn.setCharacter (enemy);
		hero.encount ();
		enemy.encount ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
