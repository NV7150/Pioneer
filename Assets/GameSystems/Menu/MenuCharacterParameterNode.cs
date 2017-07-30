using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuCharacterParameterNode : MonoBehaviour {
    public Text numberText;
    private int number;

    public void setNumber(int number){
        this.number = number;
        this.numberText.text = "" + number;
    }

}
