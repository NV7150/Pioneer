using System.Collections;
using System;
using System.Collections.Generic;

namespace Parameter{
	public class FlugList{
		//現在のレベルを表します
		private int level;
		//現在の討伐数を表します
		private int numberOfKilled = 0;
		//現在の倒したボスの最大レベルを表します
		private int levelOfBossKilled = 0;
		//現在メタル(所持金)を表します
		private int metal;
		//現在の発見した街の数を表します
		private int numberOfVisitedTown = 0;
		//魔王が倒されたかを表します
		private bool satanKilled = false;

		public FlugList(int metal,int level = 1){
			this.metal = metal;
			this.level = level;
		}

		//レベルを返します
		public int getLevel(){
			return level;
		}

		//討伐数を返します
		public int getNumberOfKilled(){
			return numberOfKilled;
		}

		//現在のメタル(所持金)を返します
		public int getMetal(){
			return metal;
		}

		//発見街数を返します
		public int getNumberOfVisitedTown(){
			return numberOfVisitedTown;
		}

		//魔王が倒されたかを返します
		public bool getSatanKilled(){
			return satanKilled;
		}

		//レベルを設定します。負の値は不適切です。
		public void setLevel(int level){
			if(level < 0)throw new ArgumentException("It's negative number");
			this.level = level;
		}

		//討伐数を設定します。負の値は不適切です。
		public void setNumberOfKilled(int number){
			if(number < 0)throw new ArgumentException("It's negative number");
			this.numberOfKilled = number;
		}

		//現在のメタル(所持金)を設定します。負の値は不適切です。
		public void setMetal(int metal){
			if(metal < 0)throw new ArgumentException("It's negative number");
			this.metal = metal;
		}

		//魔王が倒されたフラグがたちます
		public void satanisKilled(){
			satanKilled = true;
		}
	}
}
