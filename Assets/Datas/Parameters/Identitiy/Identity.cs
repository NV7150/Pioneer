using System;
using System.Collections;
using System.Collections.Generic;

using Skill;
using Character;
using MasterData;

using BattleAbility = Parameter.CharacterParameters.BattleAbility;
using FriendlyAbility = Parameter.CharacterParameters.FriendlyAbility;
using ActiveSkillType = Skill.ActiveSkillParameters.ActiveSkillType;

namespace Parameter{﻿
    public class Identity {
        private readonly int
            ID,
            LEVEL,
	        MFT_BONUS,
	        FFT_BONUS,
	        MGP_BONUS,
	        PHY_BONUS,
	        AGI_BONUS,
	        SPC_BONUS,
	        DEX_BONUS,
            SKILL_ID;

        private readonly string
	        NAME,
	        DESCRIPTION,
	        FLAVOR_TEXT,
	        SKILL_TYPE;

        public Identity(string[] datas){
            this.ID = int.Parse(datas[0]);
            this.NAME = datas[1];
            this.LEVEL = int.Parse(datas[2]);
            this.MFT_BONUS = int.Parse(datas[3]);
            this.FFT_BONUS = int.Parse(datas[4]);
            this.MGP_BONUS = int.Parse(datas[5]);
            this.PHY_BONUS = int.Parse(datas[6]);
            this.AGI_BONUS = int.Parse(datas[7]);
            this.SPC_BONUS = int.Parse(datas[8]);
            this.DEX_BONUS = int.Parse(datas[9]);
            this.SKILL_TYPE = datas[10];
            this.SKILL_ID = int.Parse(datas[11]);
            this.DESCRIPTION = datas[12];
            this.FLAVOR_TEXT = datas[13];
        }

        public int getId(){
            return ID;
        }

        public int getLevel(){
            return LEVEL;
        }

        public string getName(){
            return NAME;
        }

		public string getDescription() {
			return DESCRIPTION;
		}

		public string getFlavorText() {
			return FLAVOR_TEXT;
		}

        public Dictionary<BattleAbility,int> getBattleAbilityBonuses(){
            return new Dictionary<BattleAbility, int>(){
                {BattleAbility.MFT,MFT_BONUS},
                {BattleAbility.FFT,FFT_BONUS},
                {BattleAbility.MGP,MGP_BONUS},
                {BattleAbility.PHY,PHY_BONUS},
                {BattleAbility.AGI,AGI_BONUS}
            };
        }

        public Dictionary<FriendlyAbility, int> getFriendlyAblityBonuses() {
            return new Dictionary<FriendlyAbility, int>(){
                {FriendlyAbility.DEX,DEX_BONUS},
                {FriendlyAbility.SPC,SPC_BONUS}
            };
		}

        public void activateSkill(IPlayable player){
            if (SKILL_TYPE == "NONE")
                return;

            var activeNames = Enum.GetNames(typeof(ActiveSkillType));
            foreach(string name in activeNames){
                if(name == SKILL_TYPE){
                    ActiveSkillType type = (ActiveSkillType)Enum.Parse(typeof(ActiveSkillType), SKILL_TYPE);
                    IActiveSkill skill = ActiveSkillSupporter.getActiveSkill(type,SKILL_ID);
                    player.addSkill(skill);
                    return;
                }
            }

            if(SKILL_TYPE == "REACTION"){
                player.addSkill(ReactionSkillMasterManager.getReactionSkillFromId(SKILL_ID));
                return;
            }

            throw new InvalidProgramException("SkillType " + SKILL_TYPE + "wan't found");
        }
	}
}
