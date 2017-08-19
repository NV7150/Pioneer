using System.Collections;
using System;
using System.Collections.Generic;

using Character;
using MasterData;

using static Skill.ActiveSkillParameters.ActiveSkillType;

using ActiveSkillType = Skill.ActiveSkillParameters.ActiveSkillType;
using FriendlyCharacterType = Parameter.CharacterParameters.FriendlyCharacterType;
using static Parameter.CharacterParameters.FriendlyCharacterType;

namespace Quest{
	public class FlagList{
        private Dictionary<int, int> enemyKilled = new Dictionary<int, int>();
        private List<IFriendly> metCharacter = new List<IFriendly>();
        private int killedLevel = 0;

        private Hero player;

        public FlagList(Hero player){
            this.player = player;
		}

		/// <summary>
        /// レベルを取得します
        /// </summary>
        /// <returns>レベル</returns>
		public int getLevel(){
            return player.getLevel();
		}

		/// <summary>
        /// 討伐数を取得します
        /// </summary>
        /// <returns>討伐数</returns>
        /// <param name="enemyId">取得したい種別</param>
		public int getEnemyKilled(int enemyId){
            return (enemyKilled.ContainsKey(enemyId)) ? enemyKilled[enemyId] : 0;
		}

        public int getEnemyTotalKilled(){
            var keys = enemyKilled.Keys;
            int total = 0;
            foreach(int number in keys){
                total += number;
            }
            return total;
        }

		/// <summary>
        /// 所持金を取得します
        /// </summary>
        /// <returns>所持金</returns>
		public int getMetal(){
            return player.getMetal();
		}

        public bool hasActiveSkill(int id,ActiveSkillType activeType){
            var skills = player.getActiveSkills();

            switch(activeType){
                case ATTACK:
                    var targetAttackSkill = AttackSkillMasterManager.getAttackSkillFromId(id);
                    return skills.Contains(targetAttackSkill);

                 case BUF:
                    var targetBufSkill = BufSkillMasterManager.getBufSkillFromId(id);
                    return skills.Contains(targetBufSkill);

                case DEBUF:
                    var targetDebufSkill = DebufSkillMasterManager.getDebufSkillFromId(id);
                    return skills.Contains(targetDebufSkill);

                case HEAL:
                    var targetHealSkill = HealSkillMasterManager.getHealSkillFromId(id);
                    return skills.Contains(targetHealSkill);

                case MOVE:
                    var targetMoveSkill = MoveSkillMasterManager.getMoveSkillFromId(id);
                    return skills.Contains(targetMoveSkill);
            }

            throw new NotSupportedException("unkown activeSkillType");
        }

        public bool hasReactionSkill(int id){
            var skills = player.getReactionSkills();
            var targetReactionSkill = ReactionSkillMasterManager.getReactionSkillFromId(id);
            return skills.Contains(targetReactionSkill);
        }

        public bool hasMetCharater(int id,FriendlyCharacterType characterType){
            foreach(IFriendly character in metCharacter){
                if(character.getCharacterType() == characterType && character.getId() == id)
                    return true;
            }
            return false;
        }

		public void addEnemyKilled(Enemy enemy) {
			if (enemyKilled.ContainsKey(enemy.getId())) {
				enemyKilled[enemy.getId()]++;
			} else {
				enemyKilled.Add(enemy.getId(), 1);
			}

			if (killedLevel < enemy.getLevel()) {
				killedLevel = enemy.getLevel();
			}
		}

        public void addMetCharacter(IFriendly character){
            metCharacter.Add(character);
        }
	}
}
