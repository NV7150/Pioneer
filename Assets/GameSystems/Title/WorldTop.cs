using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CharaMake;
using MasterData;

public class WorldTop : MonoBehaviour {
    private int id;

    private TitleManager title;

    public void setState(TitleManager title){
        this.title = title;
    }

    public void setState(int id ,TitleManager title){
        this.id = id;
        this.title = title;
    }

	public void startCharaMake() {
		CharaMakeManager manager = Instantiate((GameObject)Resources.Load("Prefabs/CharaMakeManager")).GetComponent<CharaMakeManager>();
        if (WorldCreator.getInstance().getIsLoad()) {
            var data = MasterDataManagerBase.loadSaveData<WorldData>(id, "WorldData");
            manager.setDatas(data.WorldLevel);
        }else{
            manager.setDatas(1);
        }
        Destroy(gameObject);
    }

    public void back(){
        title.loadTitle();
        Destroy(gameObject);
    }
}
