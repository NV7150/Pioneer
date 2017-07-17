using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleSystem{
	public class BattleField{
		/// <summary> バトルフィールドの起点となる位置 </summary>
		private readonly Vector3 STARTER_POSITON;
		/// <summary> FieldPosition間の実距離 </summary>
		private static readonly int distanceOfArea = 10;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="position">起点</param>
		public BattleField(Vector3 position){
			STARTER_POSITON = position;
			Debug.Log (STARTER_POSITON);
		}

		/// <summary>
        /// 与えられたポジションの座標を取得します
        /// </summary>
        /// <returns>ポジションの座標</returns>
        /// <param name="going">Going.</param>
		public Vector3 getNextPosition(FieldPosition going){
            int x = Random.Range(-15,15);
			int z = distanceOfArea * ((int)going - 3) + Random.Range (0, 11);

			return STARTER_POSITON + new Vector3 (x,0,z);
		}
	}
}
