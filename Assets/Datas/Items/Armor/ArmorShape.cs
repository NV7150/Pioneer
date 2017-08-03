using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorShape{
    private readonly int 
    ID,
    DEF,
    VALUE,
    MASS,
    CREAT_DIFFICULTY;

    private readonly float
    DODGE_DISTURB_MAG,
    DELAY_DISTURB_MAG,
    MAGIC_DISTURB_MAG;

    private readonly string
    NAME,
    DESCRIPTION,
	FLAVOR_TEXT,
    ADDITIONAL_DESCRIPTION,
	ADDITIONAL_FLAVOR;

    public ArmorShape(string[] datas){
        ID = int.Parse(datas[0]);
        NAME = datas[1];
        DEF = int.Parse(datas[2]);
        VALUE = int.Parse(datas[3]);
		MASS = int.Parse(datas[4]);
		CREAT_DIFFICULTY = int.Parse(datas[5]);
        DODGE_DISTURB_MAG = float.Parse(datas[6]);
        DELAY_DISTURB_MAG = float.Parse(datas[7]);
        MAGIC_DISTURB_MAG = float.Parse(datas[8]);
        DESCRIPTION = datas[9];
        FLAVOR_TEXT = datas[10];
		ADDITIONAL_DESCRIPTION = datas[11];
		ADDITIONAL_FLAVOR = datas[12];
    }

    public int getId(){
        return ID;
    }

    public int getDef(){
        return DEF;
    }

    public int getMass(){
        return MASS;
    }

    public int getItemValue(){
        return VALUE;
    }

    public int getCreatDifficulty(){
        return CREAT_DIFFICULTY;
    }

    public float getDodgeDisturbMag(){
        return DODGE_DISTURB_MAG;
    }

    public float getDelayDisturbMag(){
        return DELAY_DISTURB_MAG;
    }

    public float getMagicDisturbMag(){
        return MAGIC_DISTURB_MAG;
    }

    public string getName(){
        return NAME;
    }

    public string getDescription(){
        return DESCRIPTION;
    }

    public string getFlavorText(){
        return FLAVOR_TEXT;
    }

	public string getAdditionalDescription() {
		return ADDITIONAL_DESCRIPTION;
	}

	public string getAdditionalFlavor() {
		return ADDITIONAL_FLAVOR;
	}
}
