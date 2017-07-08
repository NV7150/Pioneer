using System;

namespace Parameter{
	public abstract class BonusBase {
		protected string name;
		protected int bonusValue;
		protected int limit;


		//このボーナスの名前を返します
		public string getName(){
			return name;
		}

		//ボーナス値を返します
		public int getBonusValue(){
			return bonusValue;
		}

		//次のフレームへ行き、残りフレーム数がなくなるとtureを返します
		public bool nextFrame(){
			this.limit--;
			return (this.limit <= 0);
		}
	}
}

