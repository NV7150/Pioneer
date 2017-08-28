using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MasterData;

namespace Character {
    public static class EnemyHelper {
        /// <summary>
        /// 与えられたレベルに準じたエネミーのIDを返します
        /// </summary>
        /// <returns>適正レベル</returns>
        /// <param name="enemyLevel">エネミーのID</param>
        public static int getRandomEnemyFromLevel(int enemyLevel){
            var levelEnemies = EnemyMasterManager.getInstance().getEnemyIdsFromLevel(enemyLevel);

            return MathHelper.getRandomKeyLowerOrderProbality(levelEnemies);
        }
    }
}
