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
        text.text = "ワールド" + id;
    }

    public int getElement() {
        return id;
    }
}
