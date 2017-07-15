using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Character;
using Skill;
using BattleSystem;

using Extent = Skill.ActiveSkillParameters.Extent;
using ReactionSkillCategory = Skill.ReactionSkillParameters.ReactionSkillCategory;
using ActiveSkillType = Skill.ActiveSkillParameters.ActiveSkillType;

namespace AI {
    /*臆病なAIで、弱い敵を積極的に攻撃します*/
    public class Coward : IEnemyAI {
        //ベースの行動可能性値です
        private Dictionary<ActiveSkillCategory, int> probalityTable = new Dictionary<ActiveSkillCategory, int>(){
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

        public Coward(IBattleable battleable, ActiveSkillSet acitiveSkills, ReactionSkillSet passiveSkills) {
            this.user = battleable;
            this.activeSkills = acitiveSkills;
            this.reactionSkills = passiveSkills;
        }

        #region EnemyAI implementation

        public IActiveSkill decideSkill() {
            //			Debug.Log ("into decideSkill");

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

            //スキルの射程内に何もいない時、ボーナス値を使って可能性値を0にします。
            foreach (ActiveSkillCategory category in categories) {
                IActiveSkill categorySkill = activeSkills.getSkillFromSkillCategory(category);

                if (!ActiveSkillSupporter.needsTarget(categorySkill))
                    continue;

                int range = ActiveSkillSupporter.searchRange(categorySkill, user);

                if (BattleManager.getInstance().sumFromAreaTo(user, range) <= 0) {
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
                if (choose < probality || choose == 0) {
                    //					Debug.Log (activeSkills.getSkillFromSkillCategory(category).getName());
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
        /// <param name="targets">スキル効果範囲内のキャラクターのリスト</param>
        /// <param name="useSkill">使用するスキル</param>
        public List<IBattleable> decideTarget(List<IBattleable> targets, IActiveSkill useSkill) {
            if (!ActiveSkillSupporter.needsTarget(useSkill))
                throw new ArgumentException("the skill " + useSkill + " dosen't has to decide target.");

            switch (useSkill.isFriendly()) {
                case true:
                    return this.decideFriendlyTarget(targets, useSkill);
                case false:
                    return this.decideHostileTarget(targets, useSkill);
            }
            throw new InvalidOperationException("Wrong isFriendly. (not supported 実装してないです)");
        }

        /// <summary>
        /// 友好的なスキルの対象を決定します
        /// </summary>
        /// <returns>スキルの対象のリスト</returns>
        /// <param name="targets">スキル効果範囲内の対象のリスト</param>
        /// <param name="useSkill">使用するスキル</param>
        private List<IBattleable> decideFriendlyTarget(List<IBattleable> targets, IActiveSkill useSkill) {
            switch (useSkill.getActiveSkillType()) {
                case ActiveSkillType.HEAL:
                    return decideHealTarget(targets,useSkill);
                case ActiveSkillType.BUF:
                    return decideBufTarget(targets,useSkill);
            }
            throw new ArgumentException("invalid type of skill");
        }

        /// <summary>
        /// healSkillの効果対象を決定します
        /// </summary>
        /// <returns>スキル対象のリスト</returns>
        /// <param name="targets">スキル効果範囲内のキャラクターのリスト</param>
        /// <param name="useSkill">使用するスキル</param>
        private List<IBattleable> decideHealTarget(List<IBattleable> targets, IActiveSkill useSkill) {
            if(!(useSkill is HealSkill)){
                throw new ArgumentException(useSkill.getName() + " isn't a healSkill");
            }

            HealSkill healUseSkill = (HealSkill)useSkill;

            switch (healUseSkill.getExtent()) {
                case Extent.SINGLE:
                    return decideHealSingleTarget(targets, healUseSkill);
                case Extent.AREA:
                    return decideHealAreaTarget(healUseSkill);
                case Extent.ALL:
                    //工夫する予定
                    return targets;
            }
            throw new NotSupportedException("unkown extent");
        }

        /// <summary>
        /// スキル効果範囲(Extent)がSINGLEの場合の対象を決定します
        /// </summary>
        /// <returns>対象のリスト</returns>
        /// <param name="targets">射程内のキャラクターのリスト</param>
        /// <param name="useSkill">使用するスキル</param>
        private List<IBattleable> decideHealSingleTarget(List<IBattleable> targets, HealSkill useSkill) {
            //keyに可能性値、要素に対象をもつdictionary
            Dictionary<float, IBattleable> characterAndProbalities = new Dictionary<float, IBattleable>();
            float sumProbality = 0;

            foreach (IBattleable target in targets) {
                //敵対勢力→論外
                if (!target.isHostility(user.getFaction())) {
                    //HPの減った割合が可能性値
                    float probality = 1 - (float)target.getHp() / (float)target.getMaxHp();
                    characterAndProbalities.Add(probality,target);
                    sumProbality += probality;
                }
            }

            //乱数判定
            float rand = UnityEngine.Random.Range(0, sumProbality);
            var probalities = characterAndProbalities.Keys;

            foreach (float probality in probalities){
                if(probality > rand){
                    return new List<IBattleable>() { characterAndProbalities[probality] };
                }else{
                    rand += probality;
                }
            }
            throw new InvalidOperationException("cannot decideHealTarget");
        }

        private List<IBattleable> decideHealAreaTarget(HealSkill useSkill) {
            //keyのposにいる友好的なキャラクターのリスト
            Dictionary<FieldPosition, List<IBattleable>> posCharacters = new Dictionary<FieldPosition, List<IBattleable>>();
            //keyのposの可能性値
            Dictionary<FieldPosition, float> posProbalities = new Dictionary<FieldPosition, float>();
            //可能性値の合計
            int fieldPosMax = Enum.GetNames(typeof(FieldPosition)).Length;
            //現在のpos
            FieldPosition nowPos = BattleManager.getInstance().searchCharacter(user);

            //ループ変数の設定
            int index = (int)nowPos - useSkill.getRange();
            if(index < 0){
                index = 0;
            }else if (index >= fieldPosMax){
                index = fieldPosMax;
            }

            int maxIndex = (int)nowPos + useSkill.getRange();
            if (maxIndex < 0) {
                maxIndex = 0;
            } else if (maxIndex >= fieldPosMax) {
                maxIndex = fieldPosMax;
			}

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
                    return posCharacters[pos];
                }else{
                    rand += posProbalities[pos];
                }
            }

            throw new InvalidOperationException("cannot decide HealAreaCharacter");
        }

        /// <summary>
        /// BufSkillの使用対象を決定します
        /// </summary>
        /// <returns>スキルの対象のリスト</returns>
        /// <param name="targets"> スキル射程内のキャラクターのリスト </param>
        /// <param name="useSkill"> 使用するスキル </param>
        private List<IBattleable> decideBufTarget(List<IBattleable> targets,IActiveSkill useSkill){
            if(!(useSkill is BufSkill)){
                throw new ArgumentException(useSkill.getName() + " isn't a bufSkill");
            }

            BufSkill bufUseSkill = (BufSkill)useSkill;

            switch (bufUseSkill.getExtent()){
                case Extent.SINGLE:
                    return decideSingleBufTarget(targets, bufUseSkill);
                case Extent.AREA:
                    return decideAreaBufTarget(bufUseSkill);
                case Extent.ALL:
                    //くふー
                    return targets;
            }

            throw new NotSupportedException("unknown extent value");
        }

        /// <summary>
        /// スキル効果範囲(Extent)がSINGLEの時の対象を決定します
        /// </summary>
        /// <returns>対象のリスト</returns>
        /// <param name="targets">スキル射程内のキャラクターのリスト</param>
        /// <param name="useSkill">使用するスキル</param>
        private List<IBattleable> decideSingleBufTarget(List<IBattleable> targets, BufSkill useSkill){
            //keyに選択可能性、要素にIBattleable本体を持つリスト
            Dictionary<int, IBattleable> targetsAndProbality = new Dictionary<int, IBattleable>();
            int sumFriendlyLevel = 0;
            foreach(IBattleable target in targets){
                if(!target.isHostility(user.getFaction())){
                    //レベルが高いほど選択可能性が高い
                    targetsAndProbality.Add(target.getLevel(),target);
                    sumFriendlyLevel += target.getLevel();
                }
            }
            //乱数判定
            int random = UnityEngine.Random.Range(0, sumFriendlyLevel);
            var probalities = targetsAndProbality.Keys;
            foreach(int probality in probalities){
                if(probality < random){
                    return new List<IBattleable>() { targetsAndProbality[probality] };
                }else{
                    random -= probality;
                }
            }

            throw new InvalidOperationException("cannot decide singleBufTarget");
        }

        /// <summary>
        /// BufSkillの効果範囲(Extent)がAREAの時の対象を決定します
        /// </summary>
        /// <returns>対象のリスト</returns>
        /// <param name="useSkill">使用するスキル</param>
        private List<IBattleable> decideAreaBufTarget(BufSkill useSkill){
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

			int index = (int)nowPos - useSkill.getRange();
			if (index < 0) {
				index = 0;
			} else if (index >= fieldPosMax) {
				index = fieldPosMax;
			}

			int maxIndex = (int)nowPos + useSkill.getRange();
			if (maxIndex < 0) {
				maxIndex = 0;
			} else if (maxIndex >= fieldPosMax) {
				maxIndex = fieldPosMax;
			}

            //エリアの友軍の合計レベルが高いほど可能性が高くなる
            for (;index < maxIndex;index++){
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
                    return areaFriendlyCharacter[pos];
                }else{
                    rand -= areaProbality[pos];
                }
            }
            throw new InvalidOperationException("cannot decide areaTarget");
        }


        private List<IBattleable> decideHostileTarget(List<IBattleable> targets, IActiveSkill useSkill) {
            Extent extent = ActiveSkillSupporter.searchExtent(useSkill);
            int range = ActiveSkillSupporter.searchRange(useSkill, user);

            if (extent == Extent.SINGLE) {
                List<IBattleable> returnList = new List<IBattleable>();
                returnList.Add(decideHostileSingleTarget(targets));
                return returnList;

            } else if (extent == Extent.AREA) {

                //エリア攻撃の場合、最もレベルが低いエリアを殴ります。
                FieldPosition targetPos = BattleManager.getInstance().whereIsMostSafePositionInRange(user, range);
                return BattleManager.getInstance().getAreaCharacter(targetPos);

            } else if (extent == Extent.ALL) {
                //全体の場合は無条件で全部焼き払います
                List<IBattleable> returnList = new List<IBattleable>();
                foreach (List<IBattleable> list in targets) {
                    returnList.AddRange(list);
                }
                return returnList;
            }
            throw new Exception("invlit state (未実装)");
        }

        //単体の対象を決定します
        private IBattleable decideHostileSingleTarget(List<IBattleable> targets) {
            //レベルを合計する
            int sumLevel = 0;
            List<IBattleable> hostalityTargets = new List<IBattleable>();
            foreach (IBattleable target in targets) {
                if (target.isHostility(user.getFaction())) {
                    hostalityTargets.Add(target);
                    sumLevel += target.getLevel();
                }
            }

            //乱数を出す
            int choose = UnityEngine.Random.Range(0, sumLevel) + 1;
            //最終判定
            //弱い敵を積極的に殴るので、レベル合計-レベルが可能性値です
            foreach (IBattleable target in hostalityTargets) {
                int probality = sumLevel - target.getLevel();
                if (probality >= choose || probality <= 0)
                    return target;
                choose -= probality;
            }
            throw new InvalidOperationException("Cannot decideHostileSingleTarget.");
        }

        //移動距離を決めます
        public int decideMove(MoveSkill useSkill) {
            //HPが最大HPの50%以下なら非戦的行動、以上なら好戦的行動
            if ((user.getHp() / user.getMaxHp()) * 100 <= 50) {
                return recession(useSkill);
            } else {
                return advance(useSkill);
            }
        }

        //好戦的な移動を行います
        private int advance(MoveSkill useSkill) {
            //レベルが高い所に行く
            FieldPosition targetPos = BattleManager.getInstance().whereIsMostDengerPositionInRange(user, useSkill.getMove(user));
            FieldPosition nowPos = BattleManager.getInstance().searchCharacter(user);
            return ((int)targetPos) - ((int)nowPos);
        }

        //非戦的な移動を行います
        private int recession(MoveSkill useSkill) {
            //レベルが低い所に行く
            FieldPosition targetPos = BattleManager.getInstance().whereIsMostSafePositionInRange(user, useSkill.getMove(user));
            FieldPosition nowPos = BattleManager.getInstance().searchCharacter(user);
            return ((int)targetPos) - ((int)nowPos);
        }

        //リアクションを決定します
        public ReactionSkill decideReaction(IBattleable attacker, AttackSkill skill) {
            Dictionary<ReactionSkillCategory, float> riskTable = new Dictionary<ReactionSkillCategory, float>();

            //ダメージからのリスク:攻撃側の攻撃力/現在HP
            int atk = skill.getAtk(attacker);
            atk = (atk > 0) ? atk : 1;
            int hp = user.getHp();
            hp = (hp > 0) ? hp : 1;
            float dodgeDammageRisk = atk / hp;
            //命中からのリスク:命中値/回避値 - 1
            float dodgeHitRisk = (skill.getHit(attacker)) / (user.getDodge() + reactionSkills.getReactionSkillFromCategory(ReactionSkillCategory.DODGE).getDodge()) - 1;
            //回避合計リスク
            float dodgeRisk = dodgeHitRisk + dodgeDammageRisk;
            if (dodgeRisk != 0)
                dodgeRisk /= 2;
            riskTable.Add(ReactionSkillCategory.DODGE, dodgeRisk);

            //攻撃を受けるリスク
            int sumHpGuard = user.getHp() + user.getDef();
            sumHpGuard = (sumHpGuard > 0) ? sumHpGuard : 1;
            float guardRisk = atk / sumHpGuard;
            riskTable.Add(ReactionSkillCategory.GUARD, guardRisk);

            //乱数判定
            float random = UnityEngine.Random.Range(0, dodgeRisk + guardRisk);
            var keys = riskTable.Keys;
            foreach (ReactionSkillCategory category in keys) {
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