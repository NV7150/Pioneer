using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

using BattleAbility = Parameter.CharacterParameters.BattleAbility;
using FriendlyAbility = Parameter.CharacterParameters.FriendlyAbility;
using ActiveSkillType = Skill.ActiveSkillParameters.ActiveSkillType;

using Skill;
using MasterData;

namespace Parameter{
	[System.SerializableAttribute]
	public class Job{
		[SerializeField]
		private readonly int
			/// <summary> 職業のID </summary>
			ID,
            LEVEL,
			/// <summary> 基礎mft </summary>
			MFT,
			/// <summary> 基礎fft </summary>
			FFT,
			/// <summary> 基礎mgp </summary>
			MGP,
			/// <summary> 基礎phy </summary>
			PHY,
			/// <summary> 基礎dex </summary>
			DEX,
			/// <summary> 基礎agi </summary>
			AGI,
			/// <summary> 基礎spc  </summary>
			SPC;

		private readonly string
			/// <summary> 職業名 </summary>
            NAME,
            DESCRIPTION,
            FLAVOR_TEXT;

        private List<KeyValuePair<ActiveSkillType, int>> activeSkillIds = new List<KeyValuePair<ActiveSkillType, int>>();
        private List<int> reactionSkillIds = new List<int>();

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="parameters">csvによるstring配列データ</param>
		public Job(string[] parameters){
			ID = int.Parse (parameters[0]);
			NAME = parameters [1];
            LEVEL = int.Parse(parameters[2]);
			MFT = int.Parse (parameters[3]);
			FFT = int.Parse (parameters[4]);
			MGP = int.Parse (parameters[5]);
			PHY = int.Parse (parameters[6]);
			AGI = int.Parse (parameters[7]);
			DEX = int.Parse (parameters[8]);
			SPC = int.Parse (parameters[9]);

            DESCRIPTION = parameters[10];
            FLAVOR_TEXT = parameters[11];

            int index = 12;
            for (;parameters[index] != "end";index += 2){
                ActiveSkillType type = (ActiveSkillType)Enum.Parse(typeof(ActiveSkillType), parameters[index]);
                var skillId = int.Parse(parameters[index + 1]);
                activeSkillIds.Add(new KeyValuePair<ActiveSkillType, int>(type,skillId));
            }
            index++;

            for (; parameters[index] != "end";index++){
                var id = int.Parse(parameters[index]);
                reactionSkillIds.Add(id);
            }
		}

		/// <summary>
        /// 職業名を取得します
        /// </summary>
        /// <returns>ジョブ名</returns>
		public string getName (){
			return NAME;
		}

        public int getLevel(){
            return LEVEL;
        }

		/// <summary>
        /// BattleAbilityの初期化する値が入ったDictionaryを取得します
        /// </summary>
        /// <returns>初期化データのDictionary</returns>
		public Dictionary<BattleAbility,int> defaultSettingBattleAbility(){
			Dictionary<BattleAbility,int> parameters = new Dictionary<BattleAbility, int> ();
			parameters [BattleAbility.MFT] = MFT;
			parameters [BattleAbility.FFT] = FFT;
			parameters [BattleAbility.AGI] = AGI;
			parameters [BattleAbility.MGP] = MGP;
			parameters [BattleAbility.PHY] = PHY;

			return parameters;
		}

        /// <summary>
        /// FriendlyAbilityの初期化する値が入ったDictionaryを取得します
        /// </summary>
        /// <returns>初期化データのDictionary</returns>
		public Dictionary<FriendlyAbility,int> defaultSettingFriendlyAbility(){
			Dictionary<FriendlyAbility,int> parameters = new Dictionary<FriendlyAbility, int> ();
			parameters [FriendlyAbility.DEX] = DEX;
			parameters [FriendlyAbility.SPC] = SPC;

			return parameters;
		}

		/// <summary>
        /// 職業のIDを取得します
        /// </summary>
        /// <returns>ID</returns>
		public int getId(){
			return ID;
		}

        public List<IActiveSkill> getActiveSkills(){
            List<IActiveSkill> skills = new List<IActiveSkill>();
            foreach(var idSet in activeSkillIds)
                skills.Add(ActiveSkillSupporter.getActiveSkill(idSet.Key,idSet.Value));

            return skills;
        }

        public List<ReactionSkill> getReactionSkills(){
            List<ReactionSkill> skills = new List<ReactionSkill>();
            foreach (int id in reactionSkillIds)
                skills.Add(ReactionSkillMasterManager.getInstance().getReactionSkillFromId(id));

            return skills;
        }

        public string getDescription(){
            return DESCRIPTION;
        }

        public string getFlavorText(){
            return FLAVOR_TEXT;
        }

		public override string ToString () {
			return "Job " + NAME;
		}
	}
}
