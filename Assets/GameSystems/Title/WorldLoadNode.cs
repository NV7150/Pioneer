using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SelectView;
using System;

public class WorldLoadNode : MonoBehaviour,INode<int> {
    int id;

    public void setId(int id){
        this.id = id;
    }

    public int getElement() {
        return id;
    }
}
