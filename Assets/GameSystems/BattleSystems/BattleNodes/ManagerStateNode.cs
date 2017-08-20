using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BattleSystem {
    public class ManagerStateNode : MonoBehaviour {
        private PlayerBattleTaskManager manager;

        public Text stateText;

        public void setState(string name, PlayerBattleTaskManager manager){
            stateText.text = name;
            this.manager = manager;
        }

        public void chosen(){
            manager.stateChanged();
        }
    }
}