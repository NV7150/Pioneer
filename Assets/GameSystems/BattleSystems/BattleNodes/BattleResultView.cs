using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BattleSystem {
    public class BattleResultView : MonoBehaviour {
        public Text expText;
        public void setExp(int exp){
            this.expText.text = "" + exp;
        }

        public void finishChose(){
            BattleManager.getInstance().backToField();
            Destroy(this.gameObject);
        }
    }
}