using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MasterData;
using Item;

using Random = UnityEngine.Random;

public class HealItemObserver : ItemObserver {

    HealItemBuilder healItem;
    
    public HealItemObserver(int itemId) : base(itemId) {}

	public override void report() {
        this.healItem = HealItemMasterManager.getInstance().getHealItemBuilderFromId(OBSERVE_ITEM_ID);
        HealItemProgress progress = HealItemMasterManager.getInstance().getProgress(OBSERVE_ITEM_ID);
        progress.Heal += reportHealValue();
        progress.Level = reportItemLevel(progress.Heal);
        ObserverHelper.saveToFile<HealItemProgress>(progress,  "HealItemProgress", OBSERVE_ITEM_ID);
    }

    private int reportItemLevel(int healProgress){
        return (healItem.getRawHeal() + healProgress) / 5;
    }

    private int reportHealValue() {
        float probalityHeal = (useFrequency + 1) / ((healItem.getLevel() / 3 + 1) * 10);
		float rand = Random.Range(0, 100);
        Debug.Log("probality " + probalityHeal);
		if (probalityHeal >= rand) {
            Debug.Log("into report");
			int baseHeal = healItem.getRawHeal();
            float randAbs = useFrequency / 3;
            float healValueRand = Random.Range(-randAbs, randAbs);
            int growthPercentage = (healValueRand + useFrequency >= 0) ? (int)healValueRand + useFrequency : 0;
			int healProgress = (int)((float)baseHeal * (float)growthPercentage / 100);
            Debug.Log("progress" + healProgress);
            return healProgress;
		}
        return 0;
    }

    public override void reset() {
        useFrequency = 0;
    }

    private enum ProgressHealParameter{
        LV,HEAL
    }
}
