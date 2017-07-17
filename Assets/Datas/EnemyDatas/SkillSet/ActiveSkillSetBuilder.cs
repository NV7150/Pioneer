using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using Skill;
using AI;
using Character;

using ActiveSkillType = Skill.ActiveSkillParameters.ActiveSkillType;

namespace MasterData{
	public class ActiveSkillSetBuilder{
		//プロパティです。詳細はActveSkillSet参照

		private int
			id,
			normalSkillId,
			cautionSkillId,
			dangerSkillId,
			powerSkillId,
			fullPowerSkillId,
			supportSkillId,
			healSkillId,
			moveSkillId;

		private string name;

		private ActiveSkillType
			normalSkillType,
			cautionSkillType,
			dangerSkillType,
			powerSkillType,
			fullPowerSkillType,
			supportSkillType,
			healSkillType,
			moveSkillType;

		/// <summary>
        /// コンスラクタ
        /// </summary>
        /// <param name="datas">csvによるstring配列</param>
		public ActiveSkillSetBuilder(string[] datas){
			id = int.Parse (datas[0]);
			name = datas [1];

			normalSkillId = int.Parse (datas [2]);
			normalSkillType = (ActiveSkillType)Enum.Parse (typeof(ActiveSkillType),datas[3]);

			cautionSkillId = int.Parse (datas[4]);
			cautionSkillType = (ActiveSkillType)Enum.Parse (typeof(ActiveSkillType), datas [5]);

			dangerSkillId = int.Parse (datas[6]);
			dangerSkillType = (ActiveSkillType)Enum.Parse (typeof(ActiveSkillType),datas[7]);

			powerSkillId = int.Parse (datas [8]);
			powerSkillType = (ActiveSkillType)Enum.Parse (typeof(ActiveSkillType),datas[9]);

			fullPowerSkillId = int.Parse (datas[10]);
			fullPowerSkillType = (ActiveSkillType)Enum.Parse (typeof(ActiveSkillType),datas[11]);

			supportSkillId = int.Parse (datas[12]);
			supportSkillType = (ActiveSkillType)Enum.Parse (typeof(ActiveSkillType), datas [13]);

			healSkillId = int.Parse (datas[14]);
			healSkillType = (ActiveSkillType)Enum.Parse (typeof(ActiveSkillType), datas [15]);

			moveSkillId = int.Parse (datas[16]);
			moveSkillType = (ActiveSkillType)Enum.Parse (typeof(ActiveSkillType),datas[17]);
		}

		//getterです

		public int getId(){
			return id;
		}
			
		public string getName(){
			return name;
		}

		public IActiveSkill getNormalSkill(){
			return searchAndGetBuilder (normalSkillId,normalSkillType);
		}

		public IActiveSkill getCautionSkill(){
			return searchAndGetBuilder (cautionSkillId,cautionSkillType);
		}

		public IActiveSkill getDangerSkill(){
			return searchAndGetBuilder (dangerSkillId,dangerSkillType);
		}

		public IActiveSkill getPowerSkill(){
			return searchAndGetBuilder (powerSkillId,powerSkillType);
		}

		public IActiveSkill getFullPowerSkill(){
			return searchAndGetBuilder (fullPowerSkillId, fullPowerSkillType);
		}

		public IActiveSkill getSupportSkill(){
			return searchAndGetBuilder (supportSkillId,supportSkillType);
		}

		public IActiveSkill getHealSkill(){
			return searchAndGetBuilder (healSkillId,healSkillType);
		}

		public IActiveSkill getMoveSkill(){
			return searchAndGetBuilder (moveSkillId,moveSkillType);
		}

		/// <summary>
		/// 与えられたデータからIActiveSkillを探して返します
		/// </summary>
		/// <returns> 結果 </returns>
		/// <param name="id"> スキルのID </param>
		/// <param name="type"> スキルの種別 </param>
		private IActiveSkill searchAndGetBuilder(int id,ActiveSkillType type){
			switch(type){
				case ActiveSkillType.ATTACK:
					return AttackSkillMasterManager.getAttackSkillFromId (id);
				case ActiveSkillType.BUF :
					//return BufSkillMasterManager.getBufSkillFromId(id);
					break;
				case ActiveSkillType.DEBUF:
					//return DebufSkillMasterManager.getDebufSkillFromId(id);
					break;
				case ActiveSkillType.HEAL:
					//return HealSkillMasterManager.getHealSkillFromId(id);
					break;
				case ActiveSkillType.MOVE:
					return MoveSkillMasterManager.getMoveSkillFromId (id);
			}
			throw new NotSupportedException ("Unkonwn SkillType");
		}

		/// <summary>
        /// ActiveSkillSetを生成します
        /// </summary>
        /// <returns>生成したActiveSkillSet</returns>
        /// <param name="user">使用者</param>
		public ActiveSkillSet build(IBattleable user){
			return new ActiveSkillSet (this,user);
		}
	}
}
