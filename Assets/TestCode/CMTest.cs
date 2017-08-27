using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Parameter;
using CharaMake;
using MasterData;

public class CMTest : MonoBehaviour {
    public CharaMakeManager manager;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKey(KeyCode.A)){
            startMaking();
        }
	}

    private void startMaking(){
        List<Job> jobs = new List<Job>() {
            JobMasterManager.getInstance().getJobFromId(0)
        };

        List<Identity> identities = new List<Identity>() {
	        //IdentityMasterManager.getIdentityFromId(0),
	        //IdentityMasterManager.getIdentityFromId(1),
	        //IdentityMasterManager.getIdentityFromId(2)
        };

        List<Humanity> humanities = new List<Humanity>() {
            HumanityMasterManager.getInstance().getHumanityFromId(0),
            HumanityMasterManager.getInstance().getHumanityFromId(1),
            HumanityMasterManager.getInstance().getHumanityFromId(2)
        };

        //manager.setDatas(jobs,humanities,identities);
    }
}
