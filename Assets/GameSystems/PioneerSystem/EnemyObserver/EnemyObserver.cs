﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using AI;
using Character;
using MasterData;

using AttackSkillAttribute = Skill.ActiveSkillParameters.AttackSkillAttribute;
using Random = UnityEngine.Random;
using BattleAbility = Parameter.CharacterParameters.BattleAbility;

using static Parameter.CharacterParameters.BattleAbility;

public class EnemyObserver : IObserver {
    private readonly int OBSERVE_ENEMY_ID;
    EnemyBuilder observeEnemyBuilder;

    private int killedNumber = 0;
    private int madeDammage = 0;
    private int attackedTimes = 0;
    //private Dictionary<ActiveSkillCategory, int> categoryUsedTimes = new Dictionary<ActiveSkillCategory, int>();
    private Dictionary<AttackSkillAttribute, int> dammagedAttributeTimes = new Dictionary<AttackSkillAttribute, int>();
    private int killedPlayer = 0;

    public EnemyObserver(int observeEnemyId){
        this.OBSERVE_ENEMY_ID = observeEnemyId;
        PioneerManager.getInstance().setObserver(this);

        var keys = Enum.GetValues(typeof(AttackSkillAttribute));
        foreach(var key in keys){
            dammagedAttributeTimes.Add((AttackSkillAttribute)key,0);
        }
    }

    public void report(int worldId) {
        observeEnemyBuilder = EnemyMasterManager.getInstance().getEnemyBuilderFromId(OBSERVE_ENEMY_ID);
        var abilities = new Dictionary<BattleAbility, int>();
        var keys = Enum.GetValues(typeof(BattleAbility));
        foreach(BattleAbility ability in keys){
            if(ability == MFT || ability == FFT || ability == MGP){
                if (judgeAttackAbilityProgressed()) {
                    abilities.Add(ability, progressAttackAbility());
                }else{
                    abilities.Add(ability,0);
                }
            }else{
                if(judgeExserciseAbilityProgressed()){
                    abilities.Add(ability,progressExserciseAbility());
				} else {
                    abilities.Add(ability,0);
                    
                }
            }
        }

        var attributeTable = new Dictionary<AttackSkillAttribute, float>();
        var attributeKeys = Enum.GetValues(typeof(AttackSkillAttribute));
        foreach(AttackSkillAttribute attribute in attributeKeys){
            if(judgeResistanceProsessed(attribute)){
                attributeTable.Add(attribute,progressResistance(attribute));
			} else {
                attributeTable.Add(attribute, 0);
            }
        }

        int weponLevel;
        if(judgeWeponLevelUped()){
            weponLevel = weponLevelUp();
        }else{
            weponLevel = 0;
        }

        var progress = EnemyMasterManager.getInstance().getProgressFromId(OBSERVE_ENEMY_ID);

        var abilityKeys = abilities.Keys;
        foreach(var key in abilityKeys){
            progress.Abilities[key] += abilities[key];
        }

        foreach(AttackSkillAttribute key in attributeKeys){
            progress.AttributeResistances[key] += attributeTable[key];
        }

        progress.WeponLevel += weponLevel;
        ObserverHelper.saveToFile<EnemyProgress>(progress,"EnemyProgress",OBSERVE_ENEMY_ID,worldId);
    }

    public void reset(){
        killedNumber = 0;
		madeDammage = 0;
		attackedTimes = 0;
		killedPlayer = 0;
		//categoryUsedTimes.Clear();
		var keys = Enum.GetValues(typeof(AttackSkillAttribute));
        foreach (AttackSkillAttribute key in keys) {
            dammagedAttributeTimes[key] = 0;
		}
    }

    private bool judgeAttackAbilityProgressed() {
        float probality = madeDammage / (((float)observeEnemyBuilder.getLevel() / 3 + 1) * 300);
        float probalityRand = Random.Range(0, 100);
        return probality >= probalityRand;
    }

    private int progressAttackAbility() {
        float randAbs = madeDammage / 500;
        float rand = Random.Range(-randAbs, randAbs);
        float progress = (madeDammage / 200 + rand) / (50 * ((float)observeEnemyBuilder.getLevel() / 3 + 1)) + killedPlayer;
        return (int)progress;
    }

    private bool judgeExserciseAbilityProgressed() {
        float probality = killedNumber / (((float)observeEnemyBuilder.getLevel() / 3 + 1) * 50);
        float probalityRand = Random.Range(0, 100);
        return probality >= probalityRand;
    }

    private int progressExserciseAbility(){
        float randAbs = killedNumber / 3;
        float rand = Random.Range(-randAbs, randAbs);
        float progress = (killedNumber + rand) / (50 * ((float)observeEnemyBuilder.getLevel() / 3 + 1)) + killedPlayer;
        return (int)progress;
    }

    private bool judgeResistanceProsessed(AttackSkillAttribute attribute){
        float probalityValue = (dammagedAttributeTimes[attribute] + killedNumber) / 2;
        float probality = probalityValue / (50 * (observeEnemyBuilder.getLevel() / 3 + 1));
        float probalityRand = Random.Range(0, 100);
        return probality >= probalityRand;
    }

    private float progressResistance(AttackSkillAttribute attribute){
        float randAbs = dammagedAttributeTimes[attribute] / 3;
        float rand = Random.Range(-randAbs, randAbs);
        float progress = dammagedAttributeTimes[attribute] + rand / (20 * (observeEnemyBuilder.getLevel() / 3));
        progress = (progress < 0.05f) ? progress : 0.05f;
        return progress;
    }

    private bool judgeWeponLevelUped(){
        float probality = attackedTimes / (((float)observeEnemyBuilder.getLevel() / 3 + 1) * 50);
		float probalityRand = Random.Range(0, 100);
		return probality >= probalityRand;
    }

	private int weponLevelUp() {
        return 1 + killedPlayer;
	}

    public void killed(){
        killedNumber++;
    }

    public void dammaged(AttackSkillAttribute attribute){
        dammagedAttributeTimes[attribute]++;
    }

    public void attacked(int dammage){
        madeDammage += dammage;
        attackedTimes++;
    }

    public void playerKilled(){
        killedPlayer++;
    }
}
