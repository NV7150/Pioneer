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

		//ディレイ終了までの残りフレーム数
		int delay = 0;
		//リアクション終了までの残りフレーム数
		int reactionLimit = 0;
		//リアクションが必要かを表します
		bool needToReaction;

		//ユーザーがセットされているかを表します
		bool isReady = false;

		//現在リアクションしているKeyValuePairを表します
		private KeyValuePair<IBattleable,ActiveSkill> prosessingPair;
		//リアクション待ちのKeyValuePairを表します
		private List<KeyValuePair<IBattleable,ActiveSkill>> waitingReactionActiveSkills = new List<KeyValuePair<IBattleable, ActiveSkill>>();

		void Update(){
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
			ActiveSkill useSkill = prosessingPair.Value;

			ReactionSkill reactoin = ai.decideReaction (attacker,useSkill);

			int atk = useSkill.getAtk () + attacker.getAtk (useSkill.getAttribute(),useSkill.getUseAbility());
			int hit = useSkill.getHit () + attacker.getHit (useSkill.getUseAbility());
			reactoin.reaction (user,atk,hit,useSkill.getAttribute());
			waitingReactionActiveSkills.Remove (prosessingPair);
			updateProsessingPair ();

			Debug.Log ("" + user.getHp());
		}

		//ステートがアクション時に毎フレーム行う処理です
		private void actionState(){
			BattleTask task = getTask ();
			task.getSkill ().action (user,task);
			delay = task.getSkill ().getDelay ();
			state = BattleState.IDLE;
		}

		//ステートがidle時に毎フレーム行う処理です
		private void idleState(){
			delay--;
			if(delay <= 0){
				state = BattleState.ACTION;
			}
		}

		//タスクを生成して返します
		private BattleTask getTask(){
			searchIsReady ();
			ActiveSkill skill = ai.decideSkill ();
			switch (skill.getActiveSkillType ()) {
				case ActiveSkillType.ACTION:
					List<IBattleable> targets = ai.decideTarget (BattleManager.getInstance ().getCharacterInRange (user, skill.getRange ()), skill);
					return new BattleTask (user.getUniqueId(),skill,targets);
				case ActiveSkillType.MOVE:
					int move = ai.decideMove (skill);
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

		public void deleteTaskFromTarget (IBattleable target) {
			
		}

		public void offerReaction (IBattleable attacker, ActiveSkill skill) {
			Debug.Log ("offered");
			waitingReactionActiveSkills.Add (new KeyValuePair<IBattleable, ActiveSkill>(attacker,skill));
			reactionLimit = skill.getDelay ();
			updateProsessingPair ();
		}

		public bool isHavingTask () {
			return true;
		}
		#endregion
	}
}

