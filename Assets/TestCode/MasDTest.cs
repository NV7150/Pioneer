using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using masterData;
using character;
using item;

public class MasDTest : MonoBehaviour {
	public EnemyMasterManager eManager;
	public Container con;
	public WeponMasterManager wManager;
	public Wepon woodenSowrd;
	public Wepon ironSowrd;
	public ArmorMasterManager aManager;
	public Armor cloce;
	public Armor leatherArmor;

	// Use this for initialization
	void Start () {
		eManager.getEnemyFromId (0);
		eManager.getEnemyFromId (1);

		woodenSowrd = wManager.getWeponFromId(0);
		ironSowrd = wManager.getWeponFromId (1);
		Debug.Log (woodenSowrd.getName() + " and " + ironSowrd.getName());
		cloce = aManager.getArmorFromId (0);
		leatherArmor = aManager.getArmorFromId (1);
		Debug.Log (cloce.getName() + " and " + leatherArmor.getName());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
