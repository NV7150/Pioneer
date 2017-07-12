using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using Skill;
using Character;

namespace BattleSystem{
	public class BattleTask {
		//タスク名です
		private readonly string NAME;
		//行うスキルです
		private readonly IActiveSkill SKILL;
		//行動系スキルの対象です
		private List<IBattleable> targets;
		//移動系スキルの移動量です
		private int move;
		//タスクを行うキャラクターのユニークIDです
		private readonly long OWNER_UNIQUEID;
        //タスクのユニークIDです
        private readonly long ID;

		//ターゲットが存在する場合のコンストラクタです
        public BattleTask(long uniqueId,IActiveSkill skill,List<IBattleable> targets,long id){
			this.OWNER_UNIQUEID = uniqueId;
			this.SKILL = skill;
			this.targets = targets;
			this.NAME = SKILL.getName ();
            this.ID = id;
		}

        //移動量が存在する場合のコンストラクタです
        public BattleTask(long uniqueId, IActiveSkill skill, int move, long id) {
            this.OWNER_UNIQUEID = uniqueId;
            this.SKILL = skill;
            this.NAME = skill.getName();
            this.move = move;
            this.ID = id;
        }

		//スキルを取得します
		public IActiveSkill getSkill(){
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

        /// <summary>
        /// このタスクのIDを取得します
        /// </summary>
        /// <returns> タスクのID </returns>
        public long getBattleTaskId(){
            return this.ID;
        }

        /// <summary>
        /// このタスクの名前を取得します
        /// </summary>
        /// <returns> タスク名 </returns>
		public string getName(){
			return this.NAME;
		}

        public override bool Equals(object obj) {
            if (!(obj is BattleTask))
                return false;
            BattleTask task = (BattleTask)obj;
            return (task.getOwnerBattleId() == OWNER_UNIQUEID && task.getBattleTaskId() == ID);
        }
	}
}
