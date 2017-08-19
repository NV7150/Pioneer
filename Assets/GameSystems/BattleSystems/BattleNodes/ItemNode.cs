using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Item;
using BattleSystem;

public class ItemNode : MonoBehaviour {
    public Text itemText;

    IItem item;
    PlayerBattleTaskManager manager;

    public void setItem(IItem item, PlayerBattleTaskManager manager){
        this.item = item;
        this.manager = manager;
    }

    public void chosen(){
        
    }
}
