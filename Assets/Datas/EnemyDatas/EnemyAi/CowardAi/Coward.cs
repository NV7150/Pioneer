﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Character;
using Skill;
using BattleSystem;

using Extent = Skill.ActiveSkillParameters.Extent;
using ReactionSkillType = Skill.ReactionSkillParameters.ReactionSkillType;
using ActiveSkillType = Skill.ActiveSkillParameters.ActiveSkillType;

namespace AI {
    /// <summary>
    /// 臆病なAIです
    /// 基本的に高LVな味方キャラクターを優先して支援し、低LVな敵キャラクターを優先して攻撃します
    /// </summary>

    public class Coward : IEnemyAI {
        /// <summary> ベースの行動の可能性値です </summary>
        private Dictionary<ActiveSkillCategory, int> probalityTable = new Dictionary<ActiveSkillCategory, int>(){
            { ActiveSkillCategory.NORMAL, 10 },
            { ActiveSkillCategory.CAUTION, 1 },
            { ActiveSkillCategory.DANGER, 0 },
            { ActiveSkillCategory.POWER, 8 },
            { ActiveSkillCategory.FULL_POWER,3 },
            { ActiveSkillCategory.SUPPORT, 10},
            { ActiveSkillCategory.HEAL, 10 },
            { ActiveSkillCategory.MOVE,0}
        };

        /// <summary> userが持つActiveSkillセット </summary>
        private ActiveSkillSet activeSkills;
        /// <summary> userが持つReactionSkillセット </summary>
        private ReactionSkillSet reactionSkills;

        /// <summary> このAIに基づいて行動するキャラクター </summary>
        private readonly IBattleable user;

        /// <summary> このAIのID </summary>
        public static readonly int ID = 0;

        /// <summary>
        /// AIのコンストラクタ
        /// </summary>
        /// <param name="user">AIを使用するIBattleableキャラクター</param>
        /// <param name="acitiveSkills">userが使用するActiveSkillSet</param>
        /// <param name="reactionSkills">userが使用するReactionSkillSet</param>
        public Coward(IBattleable user, ActiveSkillSet acitiveSkills, ReactionSkillSet reactionSkills) {
            this.user = user;
            this.activeSkills = acitiveSkills;
            this.reactionSkills = reactionSkills;
        }

        #region EnemyAI implementation

        public IActiveSkill decideSkill() {
            //ボーナス値のテーブルです。最終的に足されます。
            Dictionary<ActiveSkillCategory, int> probalityBonus = new Dictionary<ActiveSkillCategory, int>(){
                { ActiveSkillCategory.NORMAL, 0 },
                { ActiveSkillCategory.CAUTION, 0 },
                { ActiveSkillCategory.DANGER, 0 },
                { ActiveSkillCategory.POWER, 0 },
                { ActiveSkillCategory.FULL_POWER, 0 },
                { ActiveSkillCategory.SUPPORT, 0 },
                { ActiveSkillCategory.HEAL, 0 },
                { ActiveSkillCategory.MOVE,0}
            };

            //div0を防ぐための処理
            int maxMp = (user.getMaxMp() > 0) ? user.getMaxMp() : 1;

            //HPが50%以下の場合、caution可能性値を+20します
            if (this.user.getHp() / maxMp <= 0.5f)
                probalityBonus[ActiveSkillCategory.CAUTION] += 20;

            //HPが20%以下の場合、danger可能性値を+30します
            if (this.user.getHp() / maxMp <= 0.2f)
                probalityBonus[ActiveSkillCategory.DANGER] += 30;

            //HPが70%以下の場合、攻撃する可能性値を-5、移動する可能性を+10します
            if (this.user.getHp() / maxMp <= 0.7f) {
                probalityBonus[ActiveSkillCategory.NORMAL] -= 5;
                probalityBonus[ActiveSkillCategory.POWER] -= 5;
                probalityBonus[ActiveSkillCategory.FULL_POWER] -= 5;
                probalityBonus[ActiveSkillCategory.MOVE] += 10;
            }

            List<ActiveSkillCategory> categories = new List<ActiveSkillCategory>(probalityBonus.Keys);

            //スキルの射程内に何もいない、またはコスト外の時、ボーナス値を使って可能性値を0にします。
            foreach (ActiveSkillCategory category in categories) {
                IActiveSkill categorySkill = activeSkills.getSkillFromSkillCategory(category);

                if (!ActiveSkillSupporter.isAffectSkill(categorySkill))
                    continue;

                int range = ActiveSkillSupporter.searchRange(categorySkill, user);
                bool isInRange = BattleManager.getInstance().sumFromAreaTo(user, range) > 0;

                bool canUse = categorySkill.getCost() <= user.getMp();

                if (!isInRange || !canUse) {
                    probalityBonus[category] = -1 * probalityTable[category];
                }
            }

            //基礎値 + ボーナス値が負の値の場合、可能性値が0になるように設定し直します
            foreach (ActiveSkillCategory category in categories) {
                if (probalityTable[category] + probalityBonus[category] < 0) {
                    probalityBonus[category] = -1 * probalityTable[category]; ;
                }
            }

            //可能性値を合計します
            int sum = 0;
            foreach (ActiveSkillCategory category in categories) {
                sum += probalityTable[category] + probalityBonus[category];
            }

            //合計が0の場合、攻撃不可と判断して移動します
            if (sum <= 0) {
                return activeSkills.getSkillFromSkillCategory(ActiveSkillCategory.MOVE);
            }

            //乱数でスキルを選択します
            int choose = UnityEngine.Random.Range(0, sum);
            foreach (ActiveSkillCategory category in categories) {
                int probality = probalityTable[category] + probalityBonus[category];
                if (choose < probality) {
                    return activeSkills.getSkillFromSkillCategory(category);
                }
                choose -= probalityTable[category] + probalityBonus[category];
            }

            throw new InvalidOperationException("exception state");
        }

        /// <summary>
        /// スキルの対象を決定します
        /// </summary>
        /// <returns>対象のリスト</returns>
        /// <param name="useSkill">使用するスキル</param>
        public IBattleable decideSingleTarget(IActiveSkill useSkill) {
            if (!ActiveSkillSupporter.isAffectSkill(useSkill))
                throw new ArgumentException("the skill " + useSkill + " dosen't has to decide target.");
            if (ActiveSkillSupporter.searchExtent(useSkill) != Extent.SINGLE)
                throw new ArgumentException("the skill " + useSkill + " isn't a skill for single");

            if (useSkill.isFriendly()) {
                return this.decideFriendlySingleTarget(useSkill);
            }else{
                return this.decideHostileSingleTarget(useSkill);
            }
        }

        /// <summary>
        /// 友好的なスキルの対象を決定します
        /// </summary>
        /// <returns>スキルの対象のリスト</returns>
        /// <param name="useSkill">使用するスキル</param>
        private IBattleable decideFriendlySingleTarget(IActiveSkill useSkill) {
            switch (useSkill.getActiveSkillType()) {
                case ActiveSkillType.HEAL:
                    return decideHealSingleTarget((HealSkill)useSkill);
                case ActiveSkillType.BUF:
                    return decideBufSingleTarget((BufSkill)useSkill);
            }
            throw new ArgumentException("invalid type of skill");
        }

        /// <summary>
        /// スキル効果範囲(Extent)がSINGLEの場合の対象を決定します
        /// </summary>
        /// <returns>対象のリスト</returns>
        /// <param name="useSkill">使用するスキル</param>
        private IBattleable decideHealSingleTarget(HealSkill useSkill) {
            //keyに可能性値、要素に対象をもつdictionary
            Dictionary<float, IBattleable> characterAndProbalities = new Dictionary<float, IBattleable>();
            float sumProbality = 0;

            List<IBattleable> targets =  BattleManager.getInstance().getCharacterInRange(user, useSkill.getRange());

            foreach (IBattleable target in targets) {
                //敵対勢力→論外
                if (!target.isHostility(user.getFaction())) {
                    //HPの減った割合が可能性値
                    float probality = 1 - ((float)target.getHp() / (float)target.getMaxHp());
                    probality = (probality <= 0) ? 0.001f : probality;
                    characterAndProbalities.Add(probality,target);
                    sumProbality += probality;
                }
            }

            //乱数判定
            float rand = UnityEngine.Random.Range(0, sumProbality);
            var probalities = characterAndProbalities.Keys;

            foreach (float probality in probalities){
                if(probality >= rand){
                    return characterAndProbalities[probality];
                }else{
                    rand -= probality;
                }
            }
            throw new InvalidOperationException("cannot decideHealTarget");
        }

		/// <summary>
		/// スキル効果範囲(Extent)がSINGLEの時の対象を決定します
		/// </summary>
		/// <returns>対象のリスト</returns>
		/// <param name="useSkill">使用するスキル</param>
		private IBattleable decideBufSingleTarget(BufSkill useSkill) {
			//keyに選択可能性、要素にIBattleable本体を持つリスト
			Dictionary<int, IBattleable> targetsAndProbality = new Dictionary<int, IBattleable>();
			int sumFriendlyLevel = 0;

			var targets = BattleManager.getInstance().getCharacterInRange(user, useSkill.getRange());

			foreach (IBattleable target in targets) {
				if (!target.isHostility(user.getFaction())) {
                    Debug.Log("into hos");
					//レベルが高いほど選択可能性が高い
					targetsAndProbality.Add(target.getLevel(), target);
					sumFriendlyLevel += target.getLevel();
                    Debug.Log("sum " + sumFriendlyLevel + "lv " + target.getLevel());
				}
			}

			//乱数判定
			int random = UnityEngine.Random.Range(0, sumFriendlyLevel);
            Debug.Log("rand " + random);
			var probalities = targetsAndProbality.Keys;
			foreach (int probality in probalities) {
				if (probality >= random) {
					return targetsAndProbality[probality];
				} else {
					random -= probality;
				}
			}

			throw new InvalidOperationException("cannot decide singleBufTarget");
		}

		/// <summary>
		/// 効果範囲が単体のスキルの対象を決定します
		/// </summary>
		/// <returns>対象となったキャラクター</returns>
		/// <param name="useSkill">使用するスキル</param>
		private IBattleable decideHostileSingleTarget(IActiveSkill useSkill) {
			//レベルを合計する
			int sumLevel = 0;
			List<IBattleable> hostalityTargets = new List<IBattleable>();

			var targets = BattleManager.getInstance().getCharacterInRange(user, ActiveSkillSupporter.searchRange(useSkill, user));

			foreach (IBattleable target in targets) {
				if (target.isHostility(user.getFaction())) {
					hostalityTargets.Add(target);
					sumLevel += target.getLevel();
				}
			}

            //最終判定：計算可能な形式の変数にする
            Dictionary<int, int> levelTable = new Dictionary<int, int>();
            for (int i = 0; i < hostalityTargets.Count;i++){
                levelTable.Add(i,hostalityTargets[i].getLevel());
            }

            return hostalityTargets[MathHelper.getRandomKeyLowerOrderProbality(levelTable)];

			throw new InvalidOperationException("Cannot decideHostileSingleTarget.");
		}

		public FieldPosition decideAreaTarget(IActiveSkill useSkill) {
			if (!ActiveSkillSupporter.isAffectSkill(useSkill))
				throw new ArgumentException("the skill " + useSkill + " dosen't has to decide target.");
            if (ActiveSkillSupporter.searchExtent(useSkill) != Extent.AREA)
				throw new ArgumentException("the skill " + useSkill + " isn't a skill for single");

			if (useSkill.isFriendly()) {
                return this.decideAreaFriendlyTarget(useSkill);
			} else {
                return this.decideHostileAreaTarget(useSkill);
			}
		}

        private FieldPosition decideAreaFriendlyTarget(IActiveSkill useSkill){
			switch (useSkill.getActiveSkillType()) {
				case ActiveSkillType.HEAL:
                    return decideAreaHealTarget((HealSkill)useSkill);
				case ActiveSkillType.BUF:
                    return decideAreaBufTarget((BufSkill)useSkill);
			}
			throw new ArgumentException("invalid type of skill");
        }

        private FieldPosition decideAreaHealTarget(HealSkill useSkill) {
            //keyのposにいる友好的なキャラクターのリスト
            Dictionary<FieldPosition, List<IBattleable>> posCharacters = new Dictionary<FieldPosition, List<IBattleable>>();
            //keyのposの可能性値
            Dictionary<FieldPosition, float> posProbalities = new Dictionary<FieldPosition, float>();
            //可能性値の合計
            int fieldPosMax = Enum.GetNames(typeof(FieldPosition)).Length;
            //現在のpos
            FieldPosition nowPos = BattleManager.getInstance().searchCharacter(user);

			//ループ変数の設定
			int index = BattleManager.getInstance().restructionPositionValue(nowPos, -1 * useSkill.getRange());

			int maxIndex = BattleManager.getInstance().restructionPositionValue(nowPos, useSkill.getRange());

            //２つのdictionaryの設定
            float probalitySum = 0;
            for (;index < maxIndex;index++){
                //範囲にいるキャラクターの取得
                var targets = BattleManager.getInstance().getAreaCharacter((FieldPosition)index);
                float areaProbalitySum = 0;

                //リストの初期化
                posCharacters.Add((FieldPosition)index,new List<IBattleable>());
				int hpSum = 0;
				int maxHpSum = 0;
                //友好的なキャラクターを検索してdictionaryに追加
                foreach(IBattleable target in targets){
                    if (!target.isHostility(user.getFaction())){
                        hpSum += target.getHp();
                        maxHpSum += target.getMaxHp();
                        posCharacters[(FieldPosition)index].Add(target);
                    }
                }
                //HPが減っている割合を計算→可能性値にする
                areaProbalitySum = 1 - ( (float)hpSum / (float)maxHpSum );
                posProbalities.Add((FieldPosition)index,areaProbalitySum);
                probalitySum += areaProbalitySum;
            }

            //乱数判定
            float rand = UnityEngine.Random.Range(0, probalitySum);
            var poses = posCharacters.Keys;

            foreach(FieldPosition pos in poses){
                if(posProbalities[pos] > rand){
                    return pos;
                }else{
                    rand += posProbalities[pos];
                }
            }

            throw new InvalidOperationException("cannot decide HealAreaCharacter");
        }

        /// <summary>
        /// BufSkillの効果範囲(Extent)がAREAの時の対象を決定します
        /// </summary>
        /// <returns>対象のリスト</returns>
        /// <param name="useSkill">使用するスキル</param>
        private FieldPosition decideAreaBufTarget(BufSkill useSkill){
            //エリアの友軍
            Dictionary<FieldPosition, List<IBattleable>> areaFriendlyCharacter = new Dictionary<FieldPosition, List<IBattleable>>();
            //エリアの選択可能性
            Dictionary<FieldPosition, int> areaProbality = new Dictionary<FieldPosition, int>();
            //レベルの合計
            int levelSum = 0;
            //現在のpos
            FieldPosition nowPos = BattleManager.getInstance().searchCharacter(user);

            //ループ変数の設定
			int fieldPosMax = Enum.GetNames(typeof(FieldPosition)).Length;

            int index = BattleManager.getInstance().restructionPositionValue(nowPos, -1 * useSkill.getRange());

            int maxIndex = BattleManager.getInstance().restructionPositionValue(nowPos, useSkill.getRange());

            //エリアの友軍の合計レベルが高いほど可能性が高くなる
            for (;index <= maxIndex;index++){
                int areaFriendlyLevelSum = 0;
                areaFriendlyCharacter.Add((FieldPosition)index,new List<IBattleable>());
                foreach(IBattleable target in BattleManager.getInstance().getAreaCharacter((FieldPosition)index)){
                    if(!target.isHostility(user.getFaction())){
                        areaFriendlyCharacter[(FieldPosition)index].Add(target);
                        areaFriendlyLevelSum += target.getLevel();
                    }
                }
                areaProbality.Add((FieldPosition)index,areaFriendlyLevelSum);
                levelSum += areaFriendlyLevelSum;
            }

            //乱数判定
            int rand = UnityEngine.Random.Range(0, levelSum);

            var probalistyKeys = areaProbality.Keys;
            foreach(FieldPosition pos in probalistyKeys){
                if(areaProbality[pos] > rand){
                    return pos;
                }else{
                    rand -= areaProbality[pos];
                }
            }
            throw new InvalidOperationException("cannot decide areaTarget");
        }

        /// <summary>
        /// 効果範囲が範囲のスキルの対象を決定します
        /// </summary>
        /// <returns>対象のキャラクターのリスト</returns>
        /// <param name="useSkill">使用するスキル</param>
        private FieldPosition decideHostileAreaTarget(IActiveSkill useSkill){
            int range = ActiveSkillSupporter.searchRange(useSkill,user);
            Dictionary<FieldPosition, int> areaDangerLevelTable = BattleManager.getInstance().getRawAreaDangerLevelTableInRange(user,range);
			var keys = areaDangerLevelTable.Keys;
			int sumDangerLevel = 0;
			foreach (FieldPosition pos in keys) {
				sumDangerLevel += areaDangerLevelTable[pos];
			}

			//最終判定：レベルが高いところへ
			int rand = UnityEngine.Random.Range(0, sumDangerLevel);

			foreach (FieldPosition pos in keys) {
				if (areaDangerLevelTable[pos] <= rand) {
                    return pos;
				} else {
					rand -= areaDangerLevelTable[pos];
				}
			}
            throw new InvalidOperationException("cannot decide areaTarget");
        }


        /// <summary>
        /// 移動系スキルの移動量を決定します
        /// </summary>
        /// <returns>移動量</returns>
        /// <param name="useSkill">使用するスキル</param>
        public int decideMove(MoveSkill useSkill) {
            //HPが最大HPの50%以下なら非戦的行動、以上なら好戦的行動
            if ((user.getHp() / user.getMaxHp()) * 100 <= 50) {
                return recession(useSkill);
            } else {
                return advance(useSkill);
            }
        }

        /// <summary>
        /// 好戦的な移動を行います
        /// </summary>
        /// <returns>移動量</returns>
        /// <param name="useSkill">使用するスキル</param>
        private int advance(MoveSkill useSkill) {
            //エリア危険性レベルを取得
            Dictionary<FieldPosition, int> areaDangerLevelTable = BattleManager.getInstance().getAreaDangerLevelTableInRange(user, useSkill.getMove(user));
            var keys = areaDangerLevelTable.Keys;
            int sumDangerLevel = 0;
            foreach(FieldPosition pos in keys){
                sumDangerLevel += areaDangerLevelTable[pos];
            }
            //最終判定：レベルが高いところへ
            int rand = UnityEngine.Random.Range(0,sumDangerLevel) + 1;

            foreach(FieldPosition pos in keys){
                if(areaDangerLevelTable[pos] >= rand){
                    FieldPosition nowPos = BattleManager.getInstance().searchCharacter(user);
                    int move = pos - nowPos;

                    return move;
                }else{
                    rand -= areaDangerLevelTable[pos];
                }
            }
            throw new InvalidOperationException("cannot dicide advance move");
        }

        /// <summary>
        /// 非好戦的な移動を行います
        /// </summary>
        /// <returns>移動量</returns>
        /// <param name="useSkill">使用するスキル</param>
        private int recession(MoveSkill useSkill) {
            Debug.Log("reacession");
			//エリア危険性レベルを取得
			Dictionary<FieldPosition, int> areaDangerLevelTable = BattleManager.getInstance().getAreaDangerLevelTableInRange(user, useSkill.getMove(user));
			Dictionary<int, int> dangerLevelIntegerTable = new Dictionary<int, int>();
			var keys = areaDangerLevelTable.Keys;
			int sumDangerLevel = 0;
			foreach (FieldPosition pos in keys) {
				sumDangerLevel += areaDangerLevelTable[pos];
                dangerLevelIntegerTable.Add((int)pos,areaDangerLevelTable[pos]);
			}

			//最終判定：レベルが低いところへ
			FieldPosition nowPos = BattleManager.getInstance().searchCharacter(user);
            int move = MathHelper.getRandomKeyLowerOrderProbality(dangerLevelIntegerTable) - (int)nowPos;
			//move = move = BattleManager.getInstance().restructionPositionValue(nowPos, move);
			return move;
        }

        /// <summary>
        /// リアクションを決定します
        /// </summary>
        /// <returns>決定したリアクションスキル</returns>
        /// <param name="attacker">攻撃者</param>
        /// <param name="skill">攻撃されるスキル</param>
        public ReactionSkill decideReaction(IBattleable attacker, AttackSkill skill) {
            Dictionary<ReactionSkillType, float> riskTable = new Dictionary<ReactionSkillType, float>();

            //ダメージからのリスク:攻撃側の攻撃力/現在HP
            int atk = skill.getAtkWithoutProgress(attacker); 
            atk = (atk > 0) ? atk : 1;
            int hp = user.getHp();
            hp = (hp > 0) ? hp : 1;
            float dodgeDammageRisk = atk / hp;
            //命中からのリスク:命中値/回避値 - 1
            float dodgeHitRisk = (skill.getHit(attacker)) / (user.getDodge() + reactionSkills.getReactionSkillFromCategory(ReactionSkillType.DODGE).getDodge()) - 1;
            //回避合計リスク
            float dodgeRisk = dodgeHitRisk + dodgeDammageRisk;
            if (dodgeRisk != 0)
                dodgeRisk /= 2;
            riskTable.Add(ReactionSkillType.DODGE, dodgeRisk);

            //攻撃を受けるリスク
            int sumHpGuard = user.getHp() + user.getDef();
            sumHpGuard = (sumHpGuard > 0) ? sumHpGuard : 1;
            float guardRisk = atk / sumHpGuard;
            riskTable.Add(ReactionSkillType.GUARD, guardRisk);

            //乱数判定
            float random = UnityEngine.Random.Range(0, dodgeRisk + guardRisk);
            var keys = riskTable.Keys;
            foreach (ReactionSkillType category in keys) {
                if (riskTable[category] >= random) {
                    return reactionSkills.getReactionSkillFromCategory(category);
                } else {
                    random -= riskTable[category];
                }
            }
            throw new InvalidOperationException("invalid state");
        }
        #endregion

        public override string ToString() {
            return "CowardAI attached with " + user.ToString();
        }




    }
}