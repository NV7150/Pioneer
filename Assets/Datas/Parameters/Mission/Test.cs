using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quest{
	public class Test : Mission{
		public string getName(){
			return "テスト用、使命なし";
		}

		public bool cheak(FlagList flugs){
			return false;
		}
	}
}
