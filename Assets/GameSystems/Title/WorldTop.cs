using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CharaMake;
using MasterData;

public class WorldTop : MonoBehaviour {
    private int id;

    private TitleManager title;

    public void setId(int id){
        this.id = id;
    }

    public void setState(TitleManager title){
        this.title = title;
    }

	public void startCharaMake() {
		CharaMakeManager manager = Instantiate((GameObject)Resources.Load("Prefabs/CharaMakeManager")).GetComponent<CharaMakeManager>();
        if (WorldCreator.getInstance().getIsLoad()) {
            var data = MasterDataManagerBase.loadSaveData<WorldData>(id, "WorldData");
            manager.setDatas(data.WorldLevel);
        }else{
            Debug.Log("false");
            manager.setDatas(1);
        }
        Destroy(gameObject);
    }

    public void back(){
        title.comeBack();
        Destroy(gameObject);
    }
}
