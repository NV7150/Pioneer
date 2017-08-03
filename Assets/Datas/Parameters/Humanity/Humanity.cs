using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Character;

using BattleAbility = Parameter.CharacterParameters.BattleAbility;
using FriendlyAbility = Parameter.CharacterParameters.FriendlyAbility;
using Object = System.Object;

public class Humanity : MonoBehaviour {
    private readonly int
    ID,
    NUMBER_OF_OBJECT;

    private readonly string
    NAME,
    DESCRIPTION,
    FLAVOR_TEXT;

    private Dictionary<ApplyObject, int> applyList = new Dictionary<ApplyObject, int>();
    private Dictionary<BattleAbility, List<ApplyObject>> battleAbilityAplies = new Dictionary<BattleAbility, List<ApplyObject>>();
    private Dictionary<FriendlyAbility, List<ApplyObject>> friendlyAbilityAplies = new Dictionary<FriendlyAbility, List<ApplyObject>>();

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="datas">csvによるstring配列</param>
    public Humanity(string[] datas){
        ID = int.Parse(datas[0]);
        NAME = datas[1];
        DESCRIPTION = datas[2];
        FLAVOR_TEXT = datas[3];

        var keys = Enum.GetValues(typeof(ApplyObject));
        int count = 4;
        foreach(ApplyObject applyObject in keys){
            applyList.Add(applyObject, int.Parse(datas[count]));
            count++;
        }

        var baKeys = Enum.GetValues(typeof(BattleAbility));
        foreach(BattleAbility ability in baKeys){
            battleAbilityAplies.Add(ability,new List<ApplyObject>());
        }

        var friKeys = Enum.GetValues(typeof(FriendlyAbility));
        foreach (FriendlyAbility ability in friKeys){
            friendlyAbilityAplies.Add(ability,new List<ApplyObject>());
        }
    }

    /// <summary>
    /// ボーナスする能力値を決めます
    /// </summary>
    /// <param name="player">ボーナスを適用するキャラクター</param>
    public void activate(IPlayable player){
		//各パラメータを取得、初期化
		var battleAbilities = player.getBattleAbilities();
		var friendlyAbilities = player.getFriendlyAbilities();
		Dictionary<Object, int> abilities = new Dictionary<Object, int>();

		var bKeys = battleAbilities.Keys;
		foreach (BattleAbility ablity in bKeys) {
			abilities.Add(ablity, battleAbilities[ablity]);
		}

		var fKeys = friendlyAbilities.Keys;
		foreach (FriendlyAbility ability in fKeys) {
			abilities.Add(ability, friendlyAbilities[ability]);
		}

		//各パラメータをintのパラメータ値でソート
		int[] abilityValues = new int[7];
		abilities.Values.CopyTo(abilityValues, 0);

		Object[] abilityKeys = new Object[7];
		abilities.Keys.CopyTo(abilityKeys, 0);

        deployAbilities();
        orderAbilities(abilityKeys,abilityValues);
        shuffleAbilities(abilityKeys);
    }

    /// <summary>
    /// 各能力値に依存するボーナス適用能力値をセットします
    /// </summary>
    private void deployAbilities(){
		battleAbilityAplies[BattleAbility.MFT].Add(ApplyObject.MFT);
        battleAbilityAplies[BattleAbility.FFT].Add(ApplyObject.FFT);
        battleAbilityAplies[BattleAbility.MGP].Add(ApplyObject.MGP);
        battleAbilityAplies[BattleAbility.AGI].Add(ApplyObject.AGI);
        battleAbilityAplies[BattleAbility.PHY].Add(ApplyObject.PHY);
		friendlyAbilityAplies[FriendlyAbility.SPC].Add(ApplyObject.SPC);
        friendlyAbilityAplies[FriendlyAbility.DEX].Add(ApplyObject.DEX);
    }

    /// <summary>
    /// ボーナスの高さに依存するボーナス適用能力値をセットします
    /// </summary>
    /// <param name="abilities">能力値のEnum配列(Object配列として渡す)</param>
    /// <param name="abilityValues">能力値の数値配列</param>
    private void orderAbilities(Object[] abilities,int[] abilityValues){
        //ソート
        Array.Sort(abilityValues, abilities);

        //順番のリストを作成
		List<ApplyObject> order = new List<ApplyObject>(){
			ApplyObject.FIRST,
			ApplyObject.SECOND,
			ApplyObject.THIRD,
			ApplyObject.FOURTH,
			ApplyObject.FIFTH,
			ApplyObject.SIXTH,
			ApplyObject.SEVENTH
		};

        //順番にapliesに格納
		int i = 0;
        foreach (Object ability in abilities) {
			if (ability is BattleAbility) {
				battleAbilityAplies[(BattleAbility)ability].Add(order[i]);
			} else if (ability is FriendlyAbility) {
				friendlyAbilityAplies[(FriendlyAbility)ability].Add(order[i]);
			} else {
				throw new InvalidOperationException("can't cast");
			}
			i++;
		}
    }

    /// <summary>
    /// ランダムに決定する適用能力値を設定します
    /// </summary>
    /// <param name="abilities">能力値の配列(Object配列として)</param>
    private void shuffleAbilities(Object[] abilities){
        //シャッフル
        int index = abilities.Length;
        while(index > 1) {
            index--;
            Object indexObject = abilities[index];
            int rand = UnityEngine.Random.Range(0, index + 1);
            abilities[index] = abilities[rand];
            abilities[rand] = indexObject;
        }

		//順番のリストを作成
		List<ApplyObject> order = new List<ApplyObject>(){
            ApplyObject.RANDOM_ONE,
            ApplyObject.RANDOM_TWO,
            ApplyObject.RANDOM_THRIEE,
            ApplyObject.RANDOM_FOUR,
            ApplyObject.RANDOM_FIVE,
            ApplyObject.RANDOM_SIX,
            ApplyObject.RANDOM_SEVEN
		};

		//順番にapliesに格納
		int i = 0;
		foreach (Object ability in abilities) {
			if (ability is BattleAbility) {
				battleAbilityAplies[(BattleAbility)ability].Add(order[i]);
			} else if (ability is FriendlyAbility) {
				friendlyAbilityAplies[(FriendlyAbility)ability].Add(order[i]);
			} else {
				throw new InvalidOperationException("can't cast");
			}
			i++;
		}
    }

    /// <summary>
    /// 能力値のボーナス倍率を取得します
    /// </summary>
    /// <returns>ボーナス倍率</returns>
    /// <param name="ability">取得したい能力値</param>
    public int getAbilityBonus(BattleAbility ability){
        var applyObjects = battleAbilityAplies[ability];
        int sum = applyList[ApplyObject.ALL];
        foreach (ApplyObject applyObject in applyObjects){
            sum += applyList[applyObject];
        }
        return sum;
    }

	/// <summary>
	/// 能力値のボーナス倍率を取得します
	/// </summary>
	/// <returns>ボーナス倍率</returns>
	/// <param name="ability">取得したい能力値</param>
    public int getAbilityBonus(FriendlyAbility ability) {
		var applyObjects = friendlyAbilityAplies[ability];
        int sum = applyList[ApplyObject.ALL];
		foreach (ApplyObject applyObject in applyObjects) {
			sum += applyList[applyObject];
		}
		return sum;
	}

    /// <summary>
    /// この人間性のIDを取得します
    /// </summary>
    /// <returns>ID</returns>
    public int getId(){
        return ID;
    }

    /// <summary>
    /// この人間性の名称を取得します
    /// </summary>
    /// <returns>名称</returns>
    public string getName(){
        return NAME;
    }

    public string getDescription(){
        return DESCRIPTION;
    }

    public string getFlavorText(){
        return FLAVOR_TEXT;
    }


    /// <summary>
    /// ボーナス適用するもののリスト
    /// </summary>
    private enum ApplyObject{
		/// <summary> 全て </summary>
		ALL,
        /// <summary> １番高いもの </summary>
        FIRST,
		/// <summary> ２番目に高いもの </summary>
		SECOND,
		/// <summary> ３番目に高いもの </summary>
		THIRD,
		/// <summary> ４番目に高いもの </summary>
		FOURTH,
		/// <summary> ５番目に高いもの </summary>
		FIFTH,
        /// <summary> ６番目に高いもの </summary>
        SIXTH,
        /// <summary> ７番目に高い(１番低い)もの /summary>
		SEVENTH,
		/// <summary> 白兵戦闘力 </summary>
		MFT,
		/// <summary> 遠距離戦闘力 </summary>
		FFT,
		/// <summary> 魔力 </summary>
		MGP,
		/// <summary> 体力 </summary>
		PHY,
		/// <summary> 敏捷性 </summary>
		AGI,
		/// <summary> 話術 </summary>
		SPC,
		/// <summary> 器用 </summary>
		DEX,
		/// <summary> ランダムで一つ </summary>
		RANDOM_ONE,
		/// <summary> ランダムで一つ </summary>
		RANDOM_TWO,
		/// <summary> ランダムで一つ </summary>
		RANDOM_THRIEE,
		/// <summary> ランダムで一つ </summary>
		RANDOM_FOUR,
		/// <summary> ランダムで一つ </summary>
		RANDOM_FIVE,
		/// <summary> ランダムで一つ </summary>
		RANDOM_SIX,
		/// <summary> ランダムで一つ </summary>
		RANDOM_SEVEN
    }
}
