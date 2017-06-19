using System;
using System.Collections;
using System.Collections.Generic;

using character;
using skill;
using battleSystem;

namespace AI {
	/*臆病なAIで、弱い敵を積極的に攻撃します*/
	public class Coward :IEnemyAI{
		private Dictionary<SkillCategory,int> probalityTable = new Dictionary<SkillCategory, int>(){
			{ SkillCategory.NORMAL, 10 },
			{ SkillCategory.CAUTION, 1 },
			{ SkillCategory.DANGER, 0 },
			{ SkillCategory.POWER, 8 },
			{ SkillCategory.FULL_POWER,3 },
			{ SkillCategory.SUPPORT, 10 },
			{ SkillCategory.HEAL, 10 },
			{ SkillCategory.MOVE,0}
		};

		private ActiveSkillSet skills;

		private readonly IBattleable battleable;

		private Random rand = new Random ();

		public Coward(IBattleable battleable){
			this.battleable = battleable;
		}

		#region EnemyAI implementation
			
		public ActiveSkill decideSkill () {
			//ボーナス値のテーブルです。最終的に足されます。
			Dictionary<SkillCategory,int> probalityBonus = new Dictionary<SkillCategory, int> ();

			//HPが50%以下の場合、caution可能性値を+20します
			if (this.battleable.getHp() / this.battleable.getMaxHp() <= 0.5f)
				probalityBonus [SkillCategory.CAUTION] += 20;

			//HPが20%以下の場合、danger可能性値を+30します
			if (this.battleable.getHp() / this.battleable.getMaxHp() <= 0.2f)
				probalityBonus [SkillCategory.DANGER] += 30;

			//HPが70%以下の場合、攻撃する可能性値を-5、移動する可能性を+10します
			if (this.battleable.getHp() / this.battleable.getMaxHp() <= 0.7f) {
				probalityBonus [SkillCategory.NORMAL] -= 5;
				probalityBonus [SkillCategory.POWER] -= 5;
				probalityBonus [SkillCategory.FULL_POWER] -= 5;
				probalityBonus [SkillCategory.MOVE] += 10;
			}

			//スキルの射程内に何もいない時、ボーナス値を使って可能性値を0にします。
			foreach(SkillCategory category in probalityBonus.Keys){
				if(BattleManager.getInstance().sumFromAreaTo(battleable,skills.getSkillFromSkillCategory(category).getRange()) <= 0){
					probalityBonus [category] = -1 * probalityTable [category] ;
				}
			}

			//基礎値 + ボーナス値が負の値の場合、可能性値が0になるように設定し直します
			foreach (SkillCategory category in probalityBonus.Keys) {
				if (probalityTable [category] + probalityBonus [category] < 0) {
					probalityBonus [category] = -1 * probalityTable [category] ;;
				}
			}

			//可能性値を合計します
			int sum = 0;
			foreach (SkillCategory category in probalityTable.Keys) {
				sum += probalityTable [category] + probalityBonus [category];;
			}

			//合計が0の場合、攻撃不可と判断して移動します
			if(sum <= 0){
				return skills.getSkillFromSkillCategory (SkillCategory.MOVE);
			}

			//乱数でスキルを選択します
			int choose = rand.Next (0, sum);
			foreach (SkillCategory category in probalityTable.Keys) {
				if (choose < probalityTable [category] + probalityBonus [category] || choose == 0) {
					return skills.getSkillFromSkillCategory (category);
				}
				choose -= probalityTable [category];
			}
			throw new Exception ("exception state");
		}


		public List<IBattleable> decideTarget (List<IBattleable> targets, ActiveSkill useSkill) {
			switch (useSkill.getIsFriendly ()) {
				case true:
					return this.decideFriendlyTarget (targets, useSkill);
				case false:
					return this.decideHostileTarget (targets, useSkill);
				default:
					throw new Exception ("Wrong isFriendly.");
			}
		}

		private List<IBattleable> decideFriendlyTarget(List<IBattleable> targets,ActiveSkill useSkill){
			// とりあえずreturnがなかったので
			return new List<IBattleable>();
		}

		private List<IBattleable> decideHostileTarget(List<IBattleable> targets,ActiveSkill useSkill){
			if (useSkill.getExtent () == Extent.SINGLE) {
				//単体攻撃の場合、最もレベルが低いやつを殴ります
				IBattleable returnObject = targets [0];
				foreach (List<IBattleable> list in targets) {
					foreach (IBattleable target in list) {
						if (battleable.isHostility (target.getFaction ())) {
							if (target.getLevel () > returnObject.getLevel ()) {
								returnObject = target;
							}
						}
					}
				}
				List<IBattleable> returnList = new List<IBattleable> ();
				returnList.Add (returnObject);
				return returnList;
			} else if (useSkill.getExtent () == Extent.AREA) {
				//エリア攻撃の場合、最もレベルが低いエリアを殴ります。
				FieldPosition targetPos = BattleManager.getInstance ().whereIsMostSafePositionInRange (battleable, useSkill.getRange ());
				return BattleManager.getInstance ().getAreaCharacter (targetPos);
			} else if (useSkill.getExtent () == Extent.ALL) {
				//全体の場合は無条件で全部焼き払います
				List<IBattleable> returnList = new List<IBattleable> ();
				foreach(List<IBattleable> list in targets){
					returnList.AddRange (list);
				}
				return returnList;
			}
			throw new Exception ("invlit state");
		}

		private IBattleable decideHostileSkingleTarget(List<IBattleable> targets){
			int sumLevel = 0;
			foreach (IBattleable target in targets) {
				sumLevel += target.getLevel ();
			}
			int choose = rand.Next (0, sumLevel);
			foreach (IBattleable target in targets) {
				if (( sumLevel - target.getLevel () )>= choose)
					return target;
				choose -= ( sumLevel - target.getLevel () );
			}
			throw new Exception ("Cannot decideHOstileSingleTarget.");
		}

		//移動距離を決めます
		public int decideMove () {
			if ((battleable.getHp () / battleable.getMaxHp ()) * 100 >= 50) {
				return recession ();
			} else {
				return advance ();
			}
		}

		//好戦的な移動を行います
		private int advance(){
			FieldPosition targetPos =  BattleManager.getInstance ().whereIsMostDengerPositionInRange (battleable, skills.getSkillFromSkillCategory (SkillCategory.MOVE).getRange ());
			FieldPosition nowPos =  BattleManager.getInstance ().searchCharacter (battleable);
			return ((int)targetPos) - ((int)nowPos);
		}

		//非戦的な移動を行います
		private int recession(){
			FieldPosition targetPos =  BattleManager.getInstance ().whereIsMostSafePositionInRange (battleable, skills.getSkillFromSkillCategory (SkillCategory.MOVE).getRange ());
			FieldPosition nowPos =  BattleManager.getInstance ().searchCharacter (battleable);
			return((int)targetPos) - ((int)nowPos);
		}
		#endregion
	}
}

