using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using SelectView;
using System;

public class WorldLoadNode : MonoBehaviour,INode<int> {
    int id;
    public Text text;

    public void setId(int id){
        this.id = id;
        var worldName = MasterData.MasterDataManagerBase.loadSaveData<WorldData>(id, id, "WorldData").WorldName;
        text.text = worldName;
    }

    public int getElement() {
        return id;
    }
}
