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
            int sum = 0;
            var ids = levelEnemies.Keys;
            foreach(int id in ids){
                sum += levelEnemies[id];
            }
            int rand = Random.Range(0, sum);

            foreach(int id in ids){
                if((sum - levelEnemies[id]) >= rand){
                    return id;
                }else{
                    rand -= (sum - levelEnemies[id]);
                }
            }

            throw new System.ArgumentException("enemy not found");
        }
    }
}
