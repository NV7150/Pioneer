using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

using Character;

public class MetalView : MonoBehaviour {
    public Text numberText;
    private Player player;

    void Update(){
        numberText.text = player.getMetal() + "mt";
    }

    public void setNumber(Player player){
        this.player = player;
    }
}
