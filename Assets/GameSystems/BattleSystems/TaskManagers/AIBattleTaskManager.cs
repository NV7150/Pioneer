using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using AI;
using Skill;
using Character;

using ActiveSkillType = Skill.ActiveSkillParameters.ActiveSkillType;

namespace BattleSystem {
	public class AIBattleTaskManager : MonoBehaviour,IBattleTaskManager{
		//行動の指針となるAIです
		IEnemyAI ai;
		//元のキャラクターを表します
		IBattleable user;
		//現在のステートを表します
		BattleState state = BattleState.IDLE;

		//ディレイ終了までの残り秒数
		float delay = 0;
		//リアクション終了までの残り秒数
		float reactionLimit = 0;
		//リアクションが必要かを表します
		bool needToReaction;

		//ユーザーがセットされているかを表します
		bool isReady = false;

		//現在リアクションしているKeyValuePairを表します
		private KeyValuePair<IBattleable,AttackSkill> prosessingPair;
		//リアクション待ちのKeyValuePairを表します
		private List<KeyValuePair<IBattleable,AttackSkill>> waitingReactionActiveSkills = new List<KeyValuePair<IBattleable, AttackSkill>>();

		//かり
		public GameObject gameObject;

		void Update(){
			if (user.getHp () <= 0) {
				BattleManager.getInstance ().deadCharacter (user);
			}

			if (isReady) {
				if (needToReaction) {
					reactionState ();
				} else if (state == BattleState.ACTION) {
					actionState ();
				} else if (state == BattleState.IDLE) {
					idleState ();
				}
			}
		}

		//ステートがリアクション時に毎フレーム行う処理です
		private void reactionState(){

			IBattleable attacker = prosessingPair.Key;
			AttackSkill useSkill = prosessingPair.Value;

			ReactionSkill reactoin = ai.decideReaction (attacker,useSkill);

			int atk = useSkill.getAtk (attacker);
			int hit = useSkill.getHit (attacker);
			reactoin.reaction (user,atk,hit,useSkill.getAttackSkillAttribute());
			waitingReactionActiveSkills.Remove (prosessingPair);
			updateProsessingPair ();
		}

		//ステートがアクション時に毎フレーム行う処理です
		private void actionState(){
			BattleTask task = getTask ();
			task.getSkill ().action (user,task);
			delay = task.getSkill ().getDelay (user);
			//テスト用
			delay = 3f;
			state = BattleState.IDLE;
		}

		//ステートがidle時に毎フレーム行う処理です
		private void idleState(){
			delay -= Time.deltaTime;
			if(delay <= 0){
				state = BattleState.ACTION;
			}
		}

		//タスクを生成して返します
		private BattleTask getTask(){
			searchIsReady ();
			IActiveSkill skill = ai.decideSkill ();

			if (ActiveSkillSupporter.needsTarget (skill)) {
				List<IBattleable> targets = ai.decideTarget (BattleManager.getInstance ().getCharacterInRange (user, ActiveSkillSupporter.searchRange (skill, user)), skill);
				return new BattleTask (user.getUniqueId (), skill, targets);
			} else if(skill.getActiveSkillType() == ActiveSkillType.MOVE){
				MoveSkill moveSkill = (MoveSkill)skill;
				int move = ai.decideMove (moveSkill);
				return new BattleTask(user.getUniqueId(),skill,move);
			}
			throw new InvalidOperationException ("unknown skillType");
		}

		//キャラクターをセットします
		public void setCharacter(IBattleable bal,IEnemyAI ai){
			this.ai = ai;
			this.user = bal;
			this.isReady = true;
		}

		//キャラクターがセットされていない場合に例外を投げます
		private void searchIsReady(){
			if (ai == null || user == null)
				throw new InvalidOperationException ("Manager hasn't readied yet");
		}

		//実行中のKeyValuePairを更新します
		private void updateProsessingPair(){
			if (waitingReactionActiveSkills.Count > 0) {
				prosessingPair = waitingReactionActiveSkills [0];
				needToReaction = true;
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
		#endregion
	}
}

