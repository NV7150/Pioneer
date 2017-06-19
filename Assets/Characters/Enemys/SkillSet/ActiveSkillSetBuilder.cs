using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using skill;
using AI;

namespace masterdata{
	public class ActiveSkillSetBuilder{
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

		public int getId(){
			return id;
		}
			
		public string getName(){
			return name;
		}

		public ActiveSkill getNormalSkill(){
			return manager.getActiveSkillFromId (normalSkillId);
		}

		public ActiveSkill getCautionSkill(){
			return manager.getActiveSkillFromId (cautionSkillId);
		}

		public ActiveSkill getDangerSkill(){
			return manager.getActiveSkillFromId (dangerSkillId);
		}

		public ActiveSkill getPowerSkill(){
			return manager.getActiveSkillFromId (powerSkillId);
		}

		public ActiveSkill getFullPowerSkill(){
			return manager.getActiveSkillFromId (fullPowerSkillId);
		}

		public ActiveSkill getSupportSkill(){
			return manager.getActiveSkillFromId (supportSkillId);
		}

		public ActiveSkill getHealSkill(){
			return manager.getActiveSkillFromId (healSkillId);
		}

		public ActiveSkill getMoveSkill(){
			return manager.getActiveSkillFromId (moveSkillId);
		}

		public ActiveSkillSet build(){
			return new ActiveSkillSet (this);
		}
	}
}
