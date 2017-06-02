using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using masterData;
using character;

public class MasDTest : MonoBehaviour {
	public EnemyMasterManager manager;
	public Container con;

	// Use this for initialization
	void Start () {
		manager.getEnemyFromId (0);
		manager.getEnemyFromId (1);
	}
	
	// Update is called once per frame
	void Update () {
	}
}
