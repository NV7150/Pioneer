using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Item;

namespace BattleSystem{
    public class BattleItemNode : MonoBehaviour {
        IItem item;
        PlayerBattleTaskManager manager;

        public Text nameText;

        public void setItem(IItem item,PlayerBattleTaskManager manager){
            this.item = item;
            this.manager = manager;

            nameText.text = item.getName();        
        }

        public void chosen(){
            manager.itemChose(item);
        }
    }
}
