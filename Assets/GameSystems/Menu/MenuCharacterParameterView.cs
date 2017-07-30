using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using BattleAbility = Parameter.CharacterParameters.BattleAbility;
using FriendlyAbility = Parameter.CharacterParameters.FriendlyAbility;

[System.SerializableAttribute]
public class MenuCharacterParameterView : MonoBehaviour {
    public MenuCharacterParameterNode lvText;
    public MenuCharacterParameterNode mftText;
    public MenuCharacterParameterNode fftText;
    public MenuCharacterParameterNode mgpText;
    public MenuCharacterParameterNode phyText;
    public MenuCharacterParameterNode agiText;
    public MenuCharacterParameterNode spcText;
    public MenuCharacterParameterNode dexText;

    private Dictionary<BattleAbility, MenuCharacterParameterNode> battleAbilityTexts = new Dictionary<BattleAbility, MenuCharacterParameterNode>();
    private Dictionary<FriendlyAbility, MenuCharacterParameterNode> friendlyAbilityTexts = new Dictionary<FriendlyAbility, MenuCharacterParameterNode>();

    private void Awake(){
        battleAbilityTexts.Add(BattleAbility.MFT, mftText);
        battleAbilityTexts.Add(BattleAbility.FFT, fftText);
        battleAbilityTexts.Add(BattleAbility.MGP, mgpText);
        battleAbilityTexts.Add(BattleAbility.PHY, phyText);
        battleAbilityTexts.Add(BattleAbility.AGI, agiText);

        friendlyAbilityTexts.Add(FriendlyAbility.SPC, spcText);
        friendlyAbilityTexts.Add(FriendlyAbility.DEX, dexText);
    }

    public void setAbilities(int lv,Dictionary<BattleAbility, int> battleAbilities, Dictionary<FriendlyAbility, int> friendlyAbilities){
        lvText.setNumber(lv);

        var battleAbilityKeys = battleAbilities.Keys;
        foreach(BattleAbility ability in battleAbilityKeys){
            battleAbilityTexts[ability].setNumber(battleAbilities[ability]);
        }

        var friendlyKeys = friendlyAbilities.Keys;
        foreach (FriendlyAbility ability in friendlyKeys){
            friendlyAbilityTexts[ability].setNumber(friendlyAbilities[ability]);
        }
    }
}
