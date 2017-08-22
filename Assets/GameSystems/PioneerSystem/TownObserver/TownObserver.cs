using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using FieldMap;
using Character;

using Random = UnityEngine.Random;
using ItemAttribute = Item.ItemParameters.ItemAttribute;

public class TownObserver : IObserver{
    private readonly Town TOWN;

    private readonly int townId;
    private readonly Dictionary<IFriendly, int> charactersTraded = new Dictionary<IFriendly, int>();
    private readonly Dictionary<ItemAttribute, int> tradedAttributeTable = new Dictionary<ItemAttribute, int>();
    private int clearedQuest = 0;
    private int tradedValue = 0;
    private int tradedTimes = 0;

    private static int townNumber = 0;
    private static int totalTradedValue = 0;

    public TownObserver(Town town){
        this.TOWN = town;
        this.townId = town.getId();
        foreach(IFriendly character in town.getCharacters())
            charactersTraded.Add(character,0);

        var attributes = Enum.GetValues(typeof(ItemAttribute));
        foreach (ItemAttribute attribute in attributes)
            tradedAttributeTable.Add(attribute, 0);

		townNumber++;
		PioneerManager.getInstance().setObserver(this);
    }

    public void report() {

		if(judgeLevelUpped())
            TOWN.levelUped();

        if (judgeSizeUpped())
            TOWN.sizeUped();  

        var keys = charactersTraded.Keys;
        foreach(IFriendly character in keys){
            if(judgeLevelUpped()){
                if(character is Merchant){
                    var merchant = (Merchant)character;
                    merchant.levelup();
                }

                if(character is Client){
                    var client = (Client)character;
                    client.levelup();
                }
            }
        }

        ObserverHelper.saveToFile<TownBuilder>(TOWN.compressIntoBuilder(),"TownData",townId);
    }

    public void reset() {
        tradedAttributeTable.Clear();
        clearedQuest = 0;
        tradedValue = 0;
        tradedTimes = 0;
        totalTradedValue = 0;

        MonoBehaviour.Destroy(TOWN.gameObject);
    }

    private bool judgeLevelUpped(){
        return clearedQuest >= (TOWN.getLevel() / 5);
    }

    private bool judgeSizeUpped(){
        return (tradedValue / 100) >= TOWN.getSize() * 5;
    }

    private bool judgeCharaLevelUpped(IFriendly character){
        int number = charactersTraded[character];
        float probality = 0;
        if(character is Merchant){
            var merchant = (Merchant)character;
            probality = number / (merchant.getLevel() + 20);
        }else if(character is Client){
            var client = (Client)character;
            probality = number / (client.Level + 5);
        }

		probality = (probality <= 0.9) ? probality : 0.9f;
        int rand = Random.Range(0, 100);
        return probality >= rand;
    }

    private void levelupCharacter(IFriendly character){
        if (character is Merchant){
            var merchant = (Merchant)character;
            merchant.levelup();
        }else if (character is Client){
            var client = (Client)character;
            client.levelup();
        }
    }

    public void characterTraded(IFriendly character,int tradedValue,ItemAttribute tradedAttribute){
        if (tradedValue < 0)
            throw new InvalidOperationException("trade value won't be negative");

        if(charactersTraded.ContainsKey(character)){
            charactersTraded[character] += tradedValue;
        }else{
            charactersTraded.Add(character,tradedValue);
        }
        this.tradedValue = tradedValue;

        totalTradedValue += tradedValue;
        tradedTimes++;
        tradedAttributeTable[tradedAttribute]++;
    }

    public void characterQuestCleared(IFriendly character){
		if (charactersTraded.ContainsKey(character)) {
			charactersTraded[character] ++;
		} else {
			charactersTraded.Add(character, 1);
		}
        this.clearedQuest++;
    }

    public float changeTownPrise(){
        float difference = tradedValue - (totalTradedValue / townNumber);
        float randomAbs = tradedValue / 10;
        float growth = ((tradedValue * TOWN.getPriseMag()) + (difference + Random.Range(-randomAbs, randomAbs))) / tradedValue;
        return growth;
    }

    public Dictionary<ItemAttribute, float> changeTradeLate(){
        var keys = tradedAttributeTable.Keys;

        var tradeLates = new Dictionary<ItemAttribute, float>();

		foreach (ItemAttribute attribute in keys) {
			float baseLate = TOWN.getAttribute().getAttributeMag(attribute);

			if (tradedValue > 0) {
				int number = tradedAttributeTable[attribute];

                float probality = number / tradedTimes;
                float probalityRand = Random.Range(0, 100);

                if (probality >= probalityRand) {
                    float surplus = ((float)number * tradedValue * TOWN.getPriseMag() * baseLate) / 2 * tradedTimes - 20 * TOWN.getLevel() * TOWN.getSize();
                    float randAbs = (surplus > 0) ? surplus / 10 : -surplus / 10;
                    float result = (tradedValue * baseLate - (surplus / 2 + Random.Range(-randAbs, randAbs))) / tradedValue;

                    tradeLates.Add(attribute,result);
                    continue;
                }
			}

            tradeLates.Add(attribute,baseLate);
        }

        return tradeLates;
    }
}
