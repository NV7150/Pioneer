using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Item;
using MasterData;

using Random = UnityEngine.Random;

public class ItemMaterialObserver : ItemObserver {

    ItemMaterialBuilder material;

    public ItemMaterialObserver(int itemId) : base(itemId) {}

	public override void report() {
        material = ItemMaterialMasterManager.getInstance().getMaterialBuilderFromId(OBSERVE_ITEM_ID);
        ItemMaterialProgress progress = ItemMaterialMasterManager.getInstance().getProgress(OBSERVE_ITEM_ID);
        progress.Quality += progressQuality();
        progress.Level = progressLevel(progress.Quality);

        ObserverHelper.saveToFile<ItemMaterialProgress>(progress,"ItemMaterialProgress",OBSERVE_ITEM_ID);
    }

    private int progressLevel(float qualityPlus){
        return material.getLevel() + (int)(material.getRawQuality() + qualityPlus - 50) / 25;
    }

    private int progressQuality(){
        float probality = (float)(useFrequency + 1) / ( 2 * (float)material.getLevel() + 10);
        float probalityRand = Random.Range(0, 100);
        if(probality >= probalityRand){
            float qualityRandAbs = useFrequency / 3;
            float qualityRandom = Random.Range(-qualityRandAbs, qualityRandAbs);
            int growthPercentage = (useFrequency + qualityRandom >= 0) ? useFrequency + (int)qualityRandom : 0; 
            return (int)((float)material.getRawQuality() * (float)growthPercentage / 500);
        }

        return 0;
    }

    public override void reset() {
        useFrequency = 0;
    }

}

