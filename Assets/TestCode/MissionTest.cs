using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Character;
using MasterData;

public class MissionTest : MonoBehaviour {
    Player player; 

	// Use this for initialization
	void Start () {
        player = WorldCreatFlugHelper.getInstance().getPlayer();
	}
	
	// Update is called once per frame
	void Update () {
        if(player != null){
            if(Input.GetKey(KeyCode.O)){
                Debug.Log("adding");
                player.getFlagList().addEnemyKilled(EnemyMasterManager.getInstance().getEnemyFromId(1));
            }
        }
	}
}
