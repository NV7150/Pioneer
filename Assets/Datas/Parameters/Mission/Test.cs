using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Parameter{
	public class Test : Mission{
		public string getName(){
			return "テスト用、使命なし";
		}

		public bool cheak(FlugList flugs){
			return false;
		}
	}
}
