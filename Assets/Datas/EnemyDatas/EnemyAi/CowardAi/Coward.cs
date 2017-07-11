using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Character;
using Skill;
using BattleSystem;

using Extent = Skill.ActiveSkillParameters.Extent;
using ReactionSkillCategory = Skill.ReactionSkillParameters.ReactionSkillCategory;

namespace AI {
	/*臆病なAIで、弱い敵を積極的に攻撃します*/
	public class Coward :IEnemyAI{
		//ベースの行動可能性値です
		private Dictionary<ActiveSkillCategory,int> probalityTable = new Dictionary<ActiveSkillCategory, int>(){
			{ ActiveSkillCategory.NORMAL, 10 },
			{ ActiveSkillCategory.CAUTION, 1 },
			{ ActiveSkillCategory.DANGER, 0 },
			{ ActiveSkillCategory.POWER, 8 },
			{ ActiveSkillCategory.FULL_POWER,3 },
			{ ActiveSkillCategory.SUPPORT, 10 },
			{ ActiveSkillCategory.HEAL, 10 },
			{ ActiveSkillCategory.MOVE,0}
		};

		//アクティブスキルのスキルセットです
		private ActiveSkillSet activeSkills;
		//リアクションスキルのスキルセットです
		private ReactionSkillSet reactionSkills;

		//このAIのユーザーです
		private readonly IBattleable user;

		//このAIのIDです
		public static readonly int ID = 0;

		public Coward(IBattleable battleable,ActiveSkillSet acitiveSkills,ReactionSkillSet passiveSkills){
			this.user = battleable;
			this.activeSkills = acitiveSkills;
			this.reactionSkills = passiveSkills;
		}

		#region EnemyAI implementation
			
		public IActiveSkill decideSkill () {
//			Debug.Log ("into decideSkill");

			//ボーナス値のテーブルです。最終的に足されます。
			Dictionary<ActiveSkillCategory,int> probalityBonus = new Dictionary<ActiveSkillCategory, int> (){
				{ ActiveSkillCategory.NORMAL, 0 },
				{ ActiveSkillCategory.CAUTION, 0 },
				{ ActiveSkillCategory.DANGER, 0 },
				{ ActiveSkillCategory.POWER, 0 },
				{ ActiveSkillCategory.FULL_POWER, 0 },
				{ ActiveSkillCategory.SUPPORT, 0 },
				{ ActiveSkillCategory.HEAL, 0 },
				{ ActiveSkillCategory.MOVE,0}
			};

			int maxMp = (user.getMaxMp () > 0) ? user.getMaxMp () : 1;

			//HPが50%以下の場合、caution可能性値を+20します
			if (this.user.getHp() / maxMp <= 0.5f)
				probalityBonus [ActiveSkillCategory.CAUTION] += 20;

			//HPが20%以下の場合、danger可能性値を+30します
			if (this.user.getHp() / maxMp <= 0.2f)
				probalityBonus [ActiveSkillCategory.DANGER] += 30;

			//HPが70%以下の場合、攻撃する可能性値を-5、移動する可能性を+10します
			if (this.user.getHp() / maxMp <= 0.7f) {
				probalityBonus [ActiveSkillCategory.NORMAL] -= 5;
				probalityBonus [ActiveSkillCategory.POWER] -= 5;
				probalityBonus [ActiveSkillCategory.FULL_POWER] -= 5;
				probalityBonus [ActiveSkillCategory.MOVE] += 10;
			}

			List<ActiveSkillCategory> categories = new List<ActiveSkillCategory>(probalityBonus.Keys);

			//スキルの射程内に何もいない時、ボーナス値を使って可能性値を0にします。
			foreach(ActiveSkillCategory category in categories){
				IActiveSkill categorySkill = activeSkills.getSkillFromSkillCategory (category);

				if (!ActiveSkillSupporter.needsTarget (categorySkill))
					continue;

				int range = ActiveSkillSupporter.searchRange (categorySkill,user);

				if(BattleManager.getInstance().sumFromAreaTo(user,range) <= 0){
					probalityBonus [category] = -1 * probalityTable [category] ;
				}
			}

			//基礎値 + ボーナス値が負の値の場合、可能性値が0になるように設定し直します
			foreach (ActiveSkillCategory category in categories) {
				if (probalityTable [category] + probalityBonus [category] < 0) {
					probalityBonus [category] = -1 * probalityTable [category] ;;
				}
			}

			//可能性値を合計します
			int sum = 0;
			foreach (ActiveSkillCategory category in categories) {
				sum += probalityTable [category] + probalityBonus [category];
			}

			//合計が0の場合、攻撃不可と判断して移動します
			if(sum <= 0){
				return activeSkills.getSkillFromSkillCategory (ActiveSkillCategory.MOVE);
			}

			//乱数でスキルを選択します
			int choose = UnityEngine.Random.Range (0, sum);
			foreach (ActiveSkillCategory category in categories) {
				int probality = probalityTable [category] + probalityBonus [category];
				if (choose <  probality|| choose == 0) {
//					Debug.Log (activeSkills.getSkillFromSkillCategory(category).getName());
					return activeSkills.getSkillFromSkillCategory (category);
				}
				choose -= probalityTable [category] + probalityBonus [category];
			}

			throw new InvalidOperationException("exception state");
		}


		public List<IBattleable> decideTarget (List<IBattleable> targets, IActiveSkill useSkill) {
			if (!ActiveSkillSupporter.needsTarget (useSkill))
				throw new ArgumentException ("the skill " + useSkill + " dosen't has to decide target.");

			switch (useSkill.isFriendly ()) {
				case true:
					return this.decideFriendlyTarget (targets, useSkill);
				case false:
					return this.decideHostileTarget (targets, useSkill);
			}
			throw new InvalidOperationException ("Wrong isFriendly. (not supported 実装してないです)");
		}

		private List<IBattleable> decideFriendlyTarget(List<IBattleable> targets,IActiveSkill useSkill){
			// とりあえずreturnがなかったので
			return new List<IBattleable>();
		}

		private List<IBattleable> decideHostileTarget(List<IBattleable> targets,IActiveSkill useSkill){
			Extent extent = ActiveSkillSupporter.searchExtent (useSkill);
			int range = ActiveSkillSupporter.searchRange (useSkill,user);

			if (extent == Extent.SINGLE) {
				List<IBattleable> returnList = new List<IBattleable> ();
				returnList.Add (decideHostileSingleTarget(targets));
				return returnList;

			} else if (extent == Extent.AREA) {
				
				//エリア攻撃の場合、最もレベルが低いエリアを殴ります。
				FieldPosition targetPos = BattleManager.getInstance ().whereIsMostSafePositionInRange (user, range);
				return BattleManager.getInstance ().getAreaCharacter (targetPos);

			} else if (extent == Extent.ALL) {
				//全体の場合は無条件で全部焼き払います
				List<IBattleable> returnList = new List<IBattleable> ();
				foreach(List<IBattleable> list in targets){
					returnList.AddRange (list);
				}
				return returnList;
			}
			throw new Exception ("invlit state (未実装)");
		}
			
		//単体の対象を決定します
		private IBattleable decideHostileSingleTarget(List<IBattleable> targets){
			//レベルを合計する
			int sumLevel = 0;
			List<IBattleable> hostalityTargets = new List<IBattleable> ();
			foreach (IBattleable target in targets) {
				if (target.isHostility (user.getFaction())) {
					hostalityTargets.Add (target);
					sumLevel += target.getLevel ();
				}
			}

			//乱数を出す
			int choose = UnityEngine.Random.Range (0, sumLevel) + 1;
			//最終判定
			//弱い敵を積極的に殴るので、レベル合計-レベルが可能性値です
			foreach (IBattleable target in hostalityTargets) {
				int probality = sumLevel - target.getLevel ();
				if (probality >= choose || probality <= 0)
					return target;
				choose -= probality;
			}
			throw new InvalidOperationException ("Cannot decideHostileSingleTarget.");
		}

		//移動距離を決めます
		public int decideMove (MoveSkill useSkill) {
			//HPが最大HPの50%以下なら非戦的行動、以上なら好戦的行動
			if ((user.getHp () / user.getMaxHp ()) * 100 <= 50) {
				return recession (useSkill);
			} else {
				return advance (useSkill);
			}
		}

		//好戦的な移動を行います
		private int advance(MoveSkill useSkill){
			//レベルが高い所に行く
			FieldPosition targetPos =  BattleManager.getInstance ().whereIsMostDengerPositionInRange (user, useSkill.getMove(user));
			FieldPosition nowPos =  BattleManager.getInstance ().searchCharacter (user);
			return ((int)targetPos) - ((int)nowPos);
		}

		//非戦的な移動を行います
		private int recession(MoveSkill useSkill){
			//レベルが低い所に行く
			FieldPosition targetPos =  BattleManager.getInstance ().whereIsMostSafePositionInRange (user, useSkill.getMove(user));
			FieldPosition nowPos =  BattleManager.getInstance ().searchCharacter (user);
			return((int)targetPos) - ((int)nowPos);
		}

		//リアクションを決定します
		public ReactionSkill decideReaction (IBattleable attacker, AttackSkill skill) {
			Dictionary<ReactionSkillCategory,float> riskTable = new Dictionary<ReactionSkillCategory,float> ();

			//ダメージからのリスク:攻撃側の攻撃力/現在HP
			int atk = skill.getAtk (attacker);
			atk = (atk > 0) ? atk : 1;
			int hp = user.getHp ();
			hp = (hp > 0) ? hp : 1;
			float dodgeDammageRisk = atk / hp;
			//命中からのリスク:命中値/回避値 - 1
			float dodgeHitRisk = (skill.getHit (attacker)) / (user.getDodge() + reactionSkills.getReactionSkillFromCategory(ReactionSkillCategory.DODGE).getDodge()) - 1;
			//回避合計リスク
			float dodgeRisk = dodgeHitRisk + dodgeDammageRisk;
			if (dodgeRisk != 0)
				dodgeRisk /= 2;
			riskTable.Add (ReactionSkillCategory.DODGE, dodgeRisk);

			//攻撃を受けるリスク
			int sumHpGuard = user.getHp() + user.getDef();
			sumHpGuard = (sumHpGuard > 0) ? sumHpGuard : 1;
			float guardRisk = atk  / sumHpGuard;
			riskTable.Add (ReactionSkillCategory.GUARD,guardRisk);

			//乱数判定
			float random = UnityEngine.Random.Range(0,dodgeRisk + guardRisk);
			var keys = riskTable.Keys;
			foreach(ReactionSkillCategory category in keys){
				if (riskTable [category] >= random) {
					return reactionSkills.getReactionSkillFromCategory(category);
				} else {
					random -= riskTable [category];
				}
			}
			throw new InvalidOperationException ("invalid state");
		}
		#endregion

		public override string ToString () {
			return "CowardAI attached with " + user.ToString();
		}
	}
}

