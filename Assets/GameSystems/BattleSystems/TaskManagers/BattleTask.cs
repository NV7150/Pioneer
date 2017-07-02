using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using Skill;
using Character;

namespace BattleSystem{
	public class BattleTask {
		//行うスキルです
		private readonly ActiveSkill SKILL;
		//行動系スキルの対象です
		private List<IBattleable> targets;
		//移動系スキルの移動量です
		private int move;
		//タスクを行うキャラクターのユニークIDです
		private readonly long OWNER_UNIQUEID;

		//ターゲットが存在する場合のコンストラクタです
		public BattleTask(long uniqueId,ActiveSkill skill,List<IBattleable> targets){
			this.OWNER_UNIQUEID = uniqueId;
			this.SKILL = skill;
			this.targets = targets;
		}

		//移動量が存在する場合のコンストラクタです
		public BattleTask(long uniqueId,ActiveSkill skill,int move){
			this.OWNER_UNIQUEID = uniqueId;
			this.SKILL = skill;
			this.move = move;
		}

		//スキルを取得します
		public ActiveSkill getSkill(){
			return SKILL;
		}

		//対象を取得します
		public List<IBattleable> getTargets(){
			if (targets.Count == 0)
				throw new InvalidOperationException ("this task isn't an action");
			return targets;
		}

		//移動量を取得します
		public int getMove(){
			return move;
		}

		//所有者のユニークIDを取得します
		public long getOwnerBattleId(){
			return OWNER_UNIQUEID;
		}
	}
}
