using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathHelper {
    /// <summary>
    /// 与えられたDictionaryの値が小さいほど高い確率で選択するランダムでキーを一つ選びます。
    /// </summary>
    /// <returns>選ばれたKey</returns>
    /// <param name="dictionary">判定したいDictionary</param>
    public static int getRandomKeyLowerOrderProbality(Dictionary<int, float> dictionary){
        if (dictionary.Count <= 0)
            throw new System.ArgumentException("dictionary element count is 0");

        if (dictionary.Count == 1)
            return 0;

        float sum = 0;
        var keys = dictionary.Keys;
        foreach(var key in keys)
            sum += dictionary[key];

        float rand = Random.Range(0, sum);
        foreach(var key in keys){
            float probality = (sum - dictionary[key]) / (dictionary.Count - 1);
            if (probality >= rand) {
                return key;
            }else{
                rand -= probality;
            }
        }

        throw new System.InvalidProgramException("lowOrderRondom doesn't worked");
    }

    public static int getRandomKeyLowerOrderProbality(Dictionary<int, int> dictionary){
        Dictionary<int, float> floatDictionary = new Dictionary<int, float>();
        var keys = dictionary.Keys;
        foreach(int key in keys){
            floatDictionary.Add(key,(float)dictionary[key]);
        }
        return getRandomKeyLowerOrderProbality(floatDictionary);
    }
}
