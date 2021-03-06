﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using Skill;
using Character;

using Item;

using Extent = Skill.ActiveSkillParameters.Extent;

namespace BattleSystem{
	public class BattleTask {
		/// <summary> タスク名 </summary>
		private readonly string NAME;
		/// <summary> 行うスキル </summary>
        private IActiveSkill skill;
		/// <summary> スキルに対象があった場合の対象です </summary>
        private List<IBattleable> targets = new List<IBattleable>();
        /// <summary> 範囲攻撃の場合の対象範囲です </summary>
        private FieldPosition targetPos;
		/// <summary> スキルに移動量があった場合の移動量です </summary>
		private int move;
		/// <summary> タスクを実行するキャラクターのユニークID </summary>
		private readonly long OWNER_UNIQUEID;
        /// <summary>
        /// タスクの識別IDです
        /// TaskManagerごとに個別に設定するので一部はかぶる可能性があります
        /// </summary>
        private readonly long ID;

        private IItem item;

        private bool isSkill;

        private bool isProsessing;

		/// <summary>
        /// ターゲットが存在する場合のコンストラクタ
        /// </summary>
        /// <param name="uniqueId">タスクの実行者のユニークID</param>
        /// <param name="skill">タスクが実行するスキル</param>
        /// <param name="id">タスクID</param>
        public BattleTask(long uniqueId,IActiveSkill skill,List<IBattleable> targets,long id){
			this.OWNER_UNIQUEID = uniqueId;
			this.skill = skill;
			this.NAME = skill.getName ();
            this.ID = id;
            this.targets = targets;

            isSkill = true;
		}

        public BattleTask(long uniqueId, IActiveSkill skill, FieldPosition targetPos,long id){

			this.OWNER_UNIQUEID = uniqueId;
			this.skill = skill;
			this.NAME = skill.getName();
			this.ID = id;
            this.targetPos = targetPos;

            isSkill = true;
        }

        public BattleTask(long uniqueId, IItem item, IBattleable target, long id) {
			if (!(target is IPlayable))
				throw new ArgumentException("added character isn't a playable");
            
            this.OWNER_UNIQUEID = uniqueId;
            this.item = item;
            this.NAME = item.getName();
            this.ID = id;
            targets.Add(target);

            isSkill = false;
        }

		/// <summary>
		/// 移動が存在する場合のコンストラクタ
		/// </summary>
		/// <param name="uniqueId">タスクの実行者のユニークID</param>
		/// <param name="skill">タスクが実行するスキル</param>
		/// <param name="move">移動量</param>
		/// <param name="id">タスクID</param>
		public BattleTask(long uniqueId, IActiveSkill skill, int move, long id) {
            this.OWNER_UNIQUEID = uniqueId;
            this.skill = skill;
            this.NAME = skill.getName();
            this.move = move;
            this.ID = id;

            isSkill = true;
        }

		/// <summary>
        /// スキルを取得します
        /// </summary>
        /// <returns>タスクに設定されているスキル</returns>
		public IActiveSkill getSkill(){
            if (!isSkill)
                throw new InvalidOperationException("task has no skill");
			return skill;
		}

        public IItem getItem(){
            if (isSkill)
                throw new InvalidOperationException("task has no item");
            return item;
        }

		/// <summary>
        /// スキル対象のリストを取得します
        /// </summary>
        /// <returns>スキル対象のリスト</returns>
		public List<IBattleable> getTargets(){
            if (isSkill) {
                if (!ActiveSkillSupporter.isAffectSkill(skill))
                    throw new InvalidOperationException("this task isn't an action");
                if (targets.Count < 0)
                    targets = BattleManager.getInstance().getAreaCharacter(targetPos);
                return targets;
            }else{
                return targets;
            }
		}

		/// <summary>
        /// 移動量を取得します
        /// </summary>
        /// <returns>移動量</returns>
		public int getMove(){
			return move;
		}

		/// <summary>
        /// タスク実行者のユニークIDを取得します
        /// </summary>
        /// <returns>タスク実行者のユニークID</returns>
		public long getOwnerBattleId(){
			return OWNER_UNIQUEID;
		}

        /// <summary>
        /// タスクのIDを取得します
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

        public bool getIsSkill(){
            return isSkill;
        }

        public void activeteIsProssesing(){
            this.isProsessing = true;
        }
        public bool getIsProssesing(){
            return isProsessing;
        }

        public override bool Equals(object obj) {
            if (!(obj is BattleTask))
                return false;
            BattleTask task = (BattleTask)obj;
            return (task.getOwnerBattleId() == OWNER_UNIQUEID && task.getBattleTaskId() == ID);
        }
	}
}
