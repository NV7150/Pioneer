using System;
using UnityEngine;

namespace Parameter{
	public abstract class BonusBase {
		protected string name;
		protected int bonusValue;
		protected float limit;


		//このボーナスの名前を返します
		public string getName(){
			return name;
		}

		//ボーナス値を返します
		public int getBonusValue(){
			return bonusValue;
		}

		//次のフレームへ行き、残りフレーム数がなくなるとfalseを返します
		public bool nextFrame(){
            this.limit -= Time.deltaTime;
			return (this.limit > 0);
		}
	}
}

