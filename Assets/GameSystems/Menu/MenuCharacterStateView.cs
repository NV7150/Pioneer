using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Character;
using Parameter;

public class MenuCharacterStateView : MonoBehaviour {
    public Text nameText;

    public MenuCharacterParameterView parameterView;

    public Text weponText;
    public Text armorText;

    public Image hpBar;
    public Text hpValue;
    public Image mpBar;
    public Text mpValue;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setCharacter(IPlayable character){
        nameText.text = character.getName();

        parameterView.setAbilities(character.getLevel(),character.getBattleAbilities(),character.getFriendlyAbilities());

        //weponText.text += character.getWepon().getName();
        //armorText.text += character.getArmor().getName();

        hpBar.fillAmount = (float)character.getHp() / (float)character.getMaxHp();
        mpBar.fillAmount = (float)character.getMp() / (float)character.getMaxMp();
        hpValue.text = character.getHp() + "/" + character.getMaxHp();
        mpValue.text = character.getMp() + "/" + character.getMaxMp();
    }

    public void finishchose(){
        Destroy(gameObject);
    }
}
