using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using Character;
using Parameter;
using AI;
using Item;

using BattleAbility = Parameter.CharacterParameters.BattleAbility;
using Faction = Parameter.CharacterParameters.Faction;
using AttackSkillAttribute = Skill.ActiveSkillParameters.AttackSkillAttribute;
using static Parameter.CharacterParameters.BattleAbility;

namespace MasterData{
	[System.SerializableAttribute]
	public class EnemyBuilder{
		//プロパティです

		[SerializeField]
		private int
		id,
		aiId,
		maxHp,
		maxMp,
		mft,
		fft,
		phy,
		mgp,
		agi,
		def,
		level,
		normalDropId,
		rareDropId,
		activeSkillSetId,
		reactionSkillSetId,
        weaponLevel;

		[SerializeField]
		private string 
		name,
		modelName,
		faction;

        private Dictionary<AttackSkillAttribute, float> attributeResistances= new Dictionary<AttackSkillAttribute, float>();

		//csvによるstring配列から初期化します
		public EnemyBuilder(string[] parameters){
			setParameterFromCSV (parameters);
		}

		//各能力値のgetterです

		public int getId() {
			return id;
		}

		public int getAiId() {
			return aiId;
		}  

        public int getWeaponLevel(){
            return weaponLevel;
        }

		public int getDef() {
			return def;
		}

		public int getLevel() {
			return level;
		}

		public int getNormalDropId() {
			return normalDropId;
		}

		public int getRareDropId() {
			return rareDropId;
		}

		public int getActiveSkillSetId() {
			return activeSkillSetId;
		}

		public int getReactionSkillSetId(){
			return reactionSkillSetId;
		}

		public string getName() {
			return name;
		}

		public string getModelName() {
			return modelName;
		}

		public Faction getFaction(){
			return (Faction) Enum.Parse (typeof(Faction),this.faction);
		}

		public int getMaxHp(){
			return maxHp;
		}

		public int getMaxMp(){
			return maxMp;
		}

        public Dictionary<AttackSkillAttribute, float> getAttributeRegists(){
            return new Dictionary<AttackSkillAttribute, float>(attributeResistances);
        }

		public Dictionary<BattleAbility,int> getAbilities(){
			return new Dictionary<BattleAbility,int> {
				{BattleAbility.MFT,mft},
				{BattleAbility.FFT,fft},
				{BattleAbility.PHY,phy},
				{BattleAbility.MGP,mgp},
				{BattleAbility.AGI,agi},			
			};
		}

		//Enemyを取得します
		public Enemy build(){
			Enemy returnEnemy = new Enemy (this);
			return returnEnemy;
		}

		//csvのstring配列から初期化します
		private void setParameterFromCSV(string[] parameters){
			id = int.Parse (parameters [0]);
			name = parameters [1];
			aiId = int.Parse (parameters [2]);
			maxHp = int.Parse (parameters [3]);
			maxMp = int.Parse (parameters [4]);
			mft = int.Parse (parameters[5]);
			fft = int.Parse (parameters [6]);
			phy = int.Parse (parameters [7]);
			mgp = int.Parse (parameters [8]);
			agi = int.Parse (parameters [9]);
			def = int.Parse (parameters [10]);
			level = int.Parse (parameters [11]);
			normalDropId = int.Parse (parameters [12]);
			rareDropId = int.Parse (parameters [13]);
			activeSkillSetId = int.Parse (parameters [14]);
			reactionSkillSetId = int.Parse (parameters[15]);
			faction = parameters [16];
			modelName = "Models/" + parameters [17];

            var attributes = attributeResistances.Keys;
            foreach(AttackSkillAttribute attribute in attributes){
                attributeResistances[attribute] = 1.0f;
            }
		}

        public void setProgress(EnemyProgress progress){
            mft += progress.Abilities[MFT];
            fft += progress.Abilities[FFT];
            mgp += progress.Abilities[MGP];
            agi += progress.Abilities[AGI];
            phy += progress.Abilities[PHY];

            weaponLevel += progress.WeponLevel;
            level += progress.Level;

            attributeResistances = progress.AttributeResistances;
        }

		public override string ToString () {
			return "EnemyBuilder " + name;
		}
	}
}