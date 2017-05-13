using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace battleSystem{
	public class BattleField{
		private readonly Vector3 STARTER_POSITON;
		private static readonly int distanceOfArea = 10;

		public BattleField(Vector3 position){
			STARTER_POSITON = position;
			Debug.Log (STARTER_POSITON);
		}

		public Vector3 getNextPosition(FiealdPosition now,FiealdPosition going){
			Debug.Log ((int)going - 3);
			Debug.Log (distanceOfArea * ((int)going - 3) + Random.Range (0, 11));
			return STARTER_POSITON + new Vector3 (Random.Range(-15,15),0,distanceOfArea * ((int) going - 3) + Random.Range(0,11) );
		}
	}
}
