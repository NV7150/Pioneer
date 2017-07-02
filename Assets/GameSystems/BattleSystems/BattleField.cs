using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleSystem{
	public class BattleField{
		//このバトルフィールドの起点を表します
		private readonly Vector3 STARTER_POSITON;
		//バトルフィールドのエリアごとの実間隔を表します
		private static readonly int distanceOfArea = 10;

		public BattleField(Vector3 position){
			STARTER_POSITON = position;
			Debug.Log (STARTER_POSITON);
		}

		//与えられたポジションからポジションに移動する際の位置を表します
		public Vector3 getNextPosition(FieldPosition going){
//			Debug.Log ((int)going - 3);
//			Debug.Log (distanceOfArea * ((int)going - 3) + Random.Range (0, 11));
			int x = Random.Range(-15,15);
			int z = distanceOfArea * ((int)going - 3) + Random.Range (0, 11);

			return STARTER_POSITON + new Vector3 (x,0,z);
		}
	}
}
