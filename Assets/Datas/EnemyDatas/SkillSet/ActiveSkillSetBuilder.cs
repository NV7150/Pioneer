using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Skill;
using AI;

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

		private ActiveSkillMasterManager manager;

		//csvによるstring配列からプロパティを初期化します
		public ActiveSkillSetBuilder(string[] datas){
			id = int.Parse (datas[0]);
			name = datas [1];
			normalSkillId = int.Parse (datas [2]);
			cautionSkillId = int.Parse (datas[3]);
			dangerSkillId = int.Parse (datas[4]);
			powerSkillId = int.Parse (datas [5]);
			fullPowerSkillId = int.Parse (datas[6]);
			supportSkillId = int.Parse (datas[7]);
			healSkillId = int.Parse (datas[8]);
			moveSkillId = int.Parse (datas[9]);
		}

		//getterです

		public int getId(){
			return id;
		}
			
		public string getName(){
			return name;
		}

		public ActiveSkill getNormalSkill(){
			return ActiveSkillMasterManager.getActiveSkillFromId (normalSkillId);
		}

		public ActiveSkill getCautionSkill(){
			return ActiveSkillMasterManager.getActiveSkillFromId (cautionSkillId);
		}

		public ActiveSkill getDangerSkill(){
			return ActiveSkillMasterManager.getActiveSkillFromId (dangerSkillId);
		}

		public ActiveSkill getPowerSkill(){
			return ActiveSkillMasterManager.getActiveSkillFromId (powerSkillId);
		}

		public ActiveSkill getFullPowerSkill(){
			return ActiveSkillMasterManager.getActiveSkillFromId (fullPowerSkillId);
		}

		public ActiveSkill getSupportSkill(){
			return ActiveSkillMasterManager.getActiveSkillFromId (supportSkillId);
		}

		public ActiveSkill getHealSkill(){
			return ActiveSkillMasterManager.getActiveSkillFromId (healSkillId);
		}

		public ActiveSkill getMoveSkill(){
			return ActiveSkillMasterManager.getActiveSkillFromId (moveSkillId);
		}

		//ActiveSKillSetを取得します
		public ActiveSkillSet build(){
			return new ActiveSkillSet (this);
		}
	}
}
