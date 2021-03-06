﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using AI;
using Skill;
using Character;
using Parameter;
using MasterData;

using ActiveSkillType = Skill.ActiveSkillParameters.ActiveSkillType;
using Extent = Skill.ActiveSkillParameters.Extent;

namespace BattleSystem {
	public class AIBattleTaskManager : MonoBehaviour,IBattleTaskManager{
		/// <summary> userが持つAI </summary>
		IEnemyAI ai;
		/// <summary> 担当するIBattleableキャラクター </summary>
		IBattleable user;
		/// <summary> 現在のステート </summary>
		BattleState state = BattleState.IDLE;

		/// <summary> ディレイ終了までの秒数 </summary>
		float delay = 0;
		/// <summary> リアクションの制限時間終了までの秒数 </summary>
		float reactionLimit = 0;
		/// <summary> リアクションが必要かどうか </summary>
		bool needToReaction;
        /// <summary> リアクションを行う可能性 </summary>
        float reactionProbality;

		/// <summary> 初期設定が完了しているかどうか </summary>
		bool isReady = false;

		/// <summary> リアクションしているスキルとそれを使用したキャラクター </summary>
		private KeyValuePair<IBattleable,AttackSkill> prosessingPair;
		/// <summary> リアクション待ちのスキルとそれを使用したキャラクター </summary>
		private List<KeyValuePair<IBattleable,AttackSkill>> waitingReactionActiveSkills = new List<KeyValuePair<IBattleable, AttackSkill>>();

        /// <summary> BattleTaskを判別するためのIDのカウント </summary>
        private long battletaskIdCount = 0;

        private ReactionSkill choseReaction;

        private bool moving = true;

		void Update(){
            if (isReady && moving) {
				if (needToReaction) {
					reactionState ();
				} else if (state == BattleState.ACTION) {
					actionState ();
				} else if (state == BattleState.IDLE) {
					idleState ();
				}

				if (user.getHp() <= 0) {
					BattleManager.getInstance().deadCharacter(user);
				}
			}
		}

        /// <summary>
        /// リアクションが必要な時に毎フレーム行う処理
        /// </summary>
        private void reactionState() {
            reactionLimit -= Time.deltaTime;
            if (isGoingToDoReaction())
                reaction();

            if(reactionLimit <= 0){
                dammage();
            }
        }

        /// <summary>
        /// そのフレームにリアクションを行うかを取得します
        /// これは、攻撃者と自身(user)のレベル差に依存します
        /// </summary>
        /// <returns><c>true</c>, リアクションを行う, <c>false</c> otherwise.</returns>
        private bool isGoingToDoReaction(){
			float rand = UnityEngine.Random.Range(0f, 1f);
			reactionProbality += 0.01f;
            if(reactionProbality >= rand){
                return true;
            }else{
                return false;
            }
        }

		private void reaction() {
			IBattleable attacker = prosessingPair.Key;
			AttackSkill useSkill = prosessingPair.Value;

            choseReaction = ai.decideReaction(attacker, useSkill);
        }

        /// <summary>
        /// ダメージを与えられます
        /// </summary>
        private void dammage(){
			IBattleable attacker = prosessingPair.Key;
			AttackSkill useSkill = prosessingPair.Value;

			int atk = useSkill.getAtk(attacker);
			int hit = useSkill.getHit(attacker);
            choseReaction.reaction(user, atk, hit, useSkill.getAttackSkillAttribute());
			waitingReactionActiveSkills.Remove(prosessingPair);
			updateProsessingPair();

            choseReaction = ReactionSkillMasterManager.getInstance().getReactionSkillFromId(2);
        }

		/// <summary>
        /// ステートがACTIONの時に毎フレーム行う処理
        /// </summary>
		private void actionState(){
			BattleTask task = creatTask ();
			task.getSkill ().action (user,task);

			delay = task.getSkill ().getDelay (user);
			state = BattleState.IDLE;
		}

		/// <summary>
        /// ステートがIDLEの時に毎フレーム行う処理
        /// </summary>
		private void idleState(){
			delay -= Time.deltaTime;
			if(delay <= 0){
				state = BattleState.ACTION;
			}
		}

		/// <summary>
        /// BattleTaskを生成します
        /// </summary>
        /// <returns>生成したタスク</returns>
        private BattleTask creatTask(){
            if (!isReady)
                throw new InvalidOperationException("manager hasn't readied yet");

			IActiveSkill skill = ai.decideSkill ();
            if (ActiveSkillSupporter.isAffectSkill(skill)) {
                Extent extent = ActiveSkillSupporter.searchExtent(skill);
                BattleTask returnTask;
                //効果範囲に応じてタスクを生成
                switch (extent) {
                    case Extent.SINGLE:
						IBattleable target = ai.decideSingleTarget(skill);
						List<IBattleable> singleTargetList = new List<IBattleable>() { target };
						returnTask = new BattleTask(user.getUniqueId(), skill, singleTargetList, battletaskIdCount);
                        break;

                    case Extent.AREA:
						FieldPosition pos = ai.decideAreaTarget(skill);
						returnTask = new BattleTask(user.getUniqueId(), skill, pos, battletaskIdCount);
                        break;

                    case Extent.ALL:
						List<IBattleable> allTargetList = BattleManager.getInstance().getJoinedBattleCharacter();
						returnTask = new BattleTask(user.getUniqueId(), skill, allTargetList, battletaskIdCount);
                        break;

                    default: throw new NotSupportedException("unkonwn extent");
                }
                battletaskIdCount++;
                return returnTask;
			} else if(skill.getActiveSkillType() == ActiveSkillType.MOVE){
				MoveSkill moveSkill = (MoveSkill)skill;
				int move = ai.decideMove (moveSkill);
                BattleTask returnTask = new BattleTask(user.getUniqueId(),skill,move,battletaskIdCount);
                battletaskIdCount++;
                return returnTask;
			}
			throw new InvalidOperationException ("unknown skillType");
		}

		/// <summary>
        /// キャラクターを設定します
        /// </summary>
        /// <param name="user">設定するキャラクター</param>
        /// <param name="ai">userが持つAI</param>
        public void setCharacter(IBattleable user,IEnemyAI ai){
			this.ai = ai;
			this.user = user;
			this.isReady = true;
		}

		/// <summary>
        /// リアクションしているスキルを更新します
        /// </summary>
		private void updateProsessingPair(){
			if (waitingReactionActiveSkills.Count > 0) {
				prosessingPair = waitingReactionActiveSkills [0];
				needToReaction = true;

                IBattleable attacker = prosessingPair.Key;
                float probalityToReaction = user.getLevel() / (attacker.getLevel() * 10);
			} else {
				needToReaction = false;
			}
		}

		#region IBattleTaskManager implementation

		public void deleteTaskFromTarget (IBattleable target) {}

		public void offerReaction (IBattleable attacker, AttackSkill skill) {
			waitingReactionActiveSkills.Add (new KeyValuePair<IBattleable, AttackSkill>(attacker,skill));
			reactionLimit = skill.getDelay (user);
			updateProsessingPair ();
		}

		public bool isHavingTask () {
			return true;
		}

		public void win () {
			//(実装)まだです
		}

		public void finished () {
			MonoBehaviour.Destroy (gameObject);
		}

        public void stop() {
            moving = false;
        }

        public void move() {
            moving = true;
        }
        #endregion
    }
}

