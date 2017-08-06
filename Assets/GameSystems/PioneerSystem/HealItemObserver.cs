using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MasterData;
using Item;

using Random = UnityEngine.Random;

public class HealItemObserver : ItemObserver {

    private static Dictionary<int, Dictionary<ProgressHealParameter, int>> progressTable = new Dictionary<int, Dictionary<ProgressHealParameter, int>>();
    
    public HealItemObserver(int itemId) : base(itemId) {}

    public override void report() {
        var itemProgressTable = new Dictionary<ProgressHealParameter, int>();

        var healItem = HealItemMasterManager.getHealItemFromId(OBSERVE_ITEM_ID);

        int progressHealValue = reportHealValue(healItem);
        itemProgressTable.Add(ProgressHealParameter.HEAL,progressHealValue);

        itemProgressTable.Add(ProgressHealParameter.LV,healItem.getId());

        progressTable.Add(OBSERVE_ITEM_ID,itemProgressTable);
    }

    private int reportItemLevel(int healProgress){
        return healProgress / 5;
    }

    private int reportHealValue(HealItem healItem) {
        float probalityHeal = (useFrequency + 1) / (healItem.getLevel() / 3 + 1) * 10;
		float rand = Random.Range(0, 100);
		if (probalityHeal >= rand) {
			int baseHeal = healItem.getHeal();
			int randAbs = useFrequency / 3;
			int healValueRand = Random.Range(-randAbs, randAbs);
			int growthPercentage = (healValueRand + useFrequency >= 0) ? healValueRand + useFrequency : 0;
			int healProgress = (int)((float)baseHeal * (float)growthPercentage / 100);
            return healProgress;
		}
        return 0;
    }

    public static void saveToFile() {
        int parameters = Enum.GetNames(typeof(ProgressHealParameter)).Length + 1;
        string[,] datas = new string[parameters,progressTable.Count];
        Debug.Log("called");

        var progressTableKeys = progressTable.Keys;
        foreach(int id in progressTableKeys){
            datas[0, id] = "" + id;
            Debug.Log("added" + datas[0,id]);
            var progressValueKeys = progressTable[id].Keys;
            int progressIndex = 1;
            foreach(int progressValue in progressValueKeys){
                Debug.Log("rooped");
                datas[progressIndex, id] = "" + progressValue;
                progressIndex++;
            }
        }

        Debug.Log("x " + datas.GetLength(0) + " y " + datas.GetLength(1));

        CSVReader.writeCsvGrid(datas,"/Resources/Progresses/HealItemProgress");
    }


    public override void reset() {
        progressTable.Clear();
        useFrequency = 0;
    }

    private enum ProgressHealParameter{
        LV,HEAL
    }
}
