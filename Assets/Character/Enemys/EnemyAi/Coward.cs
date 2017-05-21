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
			{ SkillCategory.HEAL, 10 }
		};

		private Dictionary<SkillCategory,IActiveSkill> skillTable = new Dictionary<SkillCategory, IActiveSkill>();

		private readonly int maxHp;
		private readonly int maxMp;

		private int nowHp;
		private int nowMp;

		private FiealdPosition position;

		private Random rand = new Random ();

		private int maxRange;


		public Coward(int maxHp,int maxMp){
			this.maxHp = maxHp;
			this.maxMp = maxMp;
		}

		#region EnemyAI implementation
		public battleSystem.BattleCommand decideCommand () {
			//HPが20%以下の場合、移動を選択します
			if (this.nowHp / this.maxHp <= 0.2f)
				return BattleCommand.MOVE;

			//有効射程内に１匹でも敵がいる場合、行動を選択します
			int scoapEnemy = 0;
			for(int i = 0;i < maxRange;i++)
				scoapEnemy += BattleManager.getInstance ().sumArea (position);
			if (scoapEnemy <= 0)
				return BattleCommand.MOVE;
			return BattleCommand.ACTION;
		}
			
		public IActiveSkill decideSkill () {
			Dictionary<SkillCategory,int> probalityBonus = new Dictionary<SkillCategory, int> ();

			//HPが50%以下の場合、caution可能性値を+20します
			if (nowHp / maxHp <= 0.5f)
				probalityBonus [SkillCategory.CAUTION] += 20;

			//HPが20%以下の場合、danger可能性値を+30します
			if (nowHp / maxHp <= 0.2f)
				probalityBonus [SkillCategory.DANGER] += 30;

			//HPが70%以下の場合、攻撃する可能性値を-5します
			if (nowHp / maxHp <= 0.7f) {
				probalityBonus [SkillCategory.NORMAL] -= 5;
				probalityBonus [SkillCategory.POWER] -= 5;
				probalityBonus [SkillCategory.FULL_POWER] -= 5;
			}

			//ボーナス値が負の値の場合、ボーナス値を0に設定し直します
			foreach (SkillCategory category in probalityBonus.Keys)
				if (probalityBonus [category] < 0)
					probalityBonus [category] = 0;

			//使用するスキルの最終決定をします
			int sum = 0;
			foreach (SkillCategory category in probalityTable.Keys) {
				sum += probalityTable [category] + probalityBonus [category];;
			}
			int choose = rand.Next (0, sum);
			foreach (SkillCategory category in probalityTable.Keys) {
				if (choose < probalityTable [category] + probalityBonus [category]|| choose == 0)
					return skillTable [category];
				choose -= probalityTable [category];
			}
			throw new Exception ("exception state in cowardAI");
		}

		public void setSkillTable (Dictionary<SkillCategory, IActiveSkill> skillTable) {
			foreach(SkillCategory category in skillTable.Keys){
				this.skillTable.Add (category,skillTable [category]);
			}
		}
		public List<IBattleable> decideTarget (List<IBattleable> targets, IActiveSkill useSkill) {
			switch (useSkill.isFriendly ()) {
				case true:
					return this.decideFriendlyTarget (targets, useSkill);
				case false:
					return this.decideHostileTarget (targets, useSkill);
				default:
					throw new Exception ("Wrong isFriendly.");
			}
		}

		private List<IBattleable> decideFriendlyTarget(List<IBattleable> targets,IActiveSkill useSkill){
			// とりあえずreturnがなかったので
			return new List<IBattleable>();
		}

		private List<IBattleable> decideHostileTarget(List<IBattleable> targets,IActiveSkill useSkill){
			// とりあえずreturnがなかったので
			return new List<IBattleable>();
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

		public int getMove () {
			throw new NotImplementedException ();
		}
		public void setNowHp (int hp) {
			nowHp = hp;
		}
		public void setNowMp (int mp) {
			nowMp = mp;
		}
		public void setNowPosition (FiealdPosition pos) {
			position = pos;
		}
		#endregion
	}
}

