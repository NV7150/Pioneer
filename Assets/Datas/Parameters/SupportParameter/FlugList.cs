using System.Collections;
using System;
using System.Collections.Generic;

namespace Parameter{
	public class FlugList{
		/// <summary> 現在のレベル </summary>
		private int level;
		/// <summary> 現在のモンスター討伐数 </summary>
		private int numberOfKilled = 0;
		/// <summary> 倒したボスの最大レベル </summary>
		private int levelOfBossKilled = 0;
		/// <summary> 現在の所持金 </summary>
		private int metal;
		/// <summary> 発見した街の数</summary>
		private int numberOfVisitedTown = 0;
		/// <summary> 魔王が倒されたか </summary>
		private bool satanKilled = false;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="metal">所持金</param>
        /// <param name="level">レベル</param>
		public FlugList(int metal,int level = 1){
			this.metal = metal;
			this.level = level;
		}

		/// <summary>
        /// レベルを取得します
        /// </summary>
        /// <returns>レベル</returns>
		public int getLevel(){
			return level;
		}

		/// <summary>
        /// 討伐数を取得します
        /// </summary>
        /// <returns>討伐数</returns>
		public int getNumberOfKilled(){
			return numberOfKilled;
		}

		/// <summary>
        /// 所持金を取得します
        /// </summary>
        /// <returns>所持金</returns>
		public int getMetal(){
			return metal;
		}

		/// <summary>
		/// 発見した街の数を取得します
		/// </summary>
		/// <returns>発見した街の数</returns>
		public int getNumberOfVisitedTown(){
			return numberOfVisitedTown;
		}

		/// <summary>
        /// 魔王が倒されたかを取得します
        /// </summary>
        /// <returns><c>true</c>, 魔王死亡, <c>false</c> 魔王未死亡</returns>
		public bool getSatanKilled(){
			return satanKilled;
		}

		/// <summary>
        /// レベルを設定します
        /// 負の数は無効です
        /// </summary>
        /// <param name="level">設定したいレベル</param>
		public void setLevel(int level){
			if(level < 0)throw new ArgumentException("It's negative number");
			this.level = level;
		}

		/// <summary>
        /// 討伐数を追加します
        /// 負の値は無効です
        /// </summary>
        /// <param name="number">追加したい討伐数</param>
		public void addNumberOfKilled(int number){
			if(number < 0)throw new ArgumentException("It's negative number");
			this.numberOfKilled += number;
		}

		/// <summary>
        /// 所持金を設定します
        /// 負の値は無効です
        /// </summary>
        /// <param name="metal">設定したい所持金</param>
		public void setMetal(int metal){
			if(metal < 0)throw new ArgumentException("It's negative number");
			this.metal = metal;
		}

		/// <summary>
        /// 魔王が倒されたフラグがtrueになります
        /// </summary>
		public void satanisKilled(){
			satanKilled = true;
		}
	}
}
