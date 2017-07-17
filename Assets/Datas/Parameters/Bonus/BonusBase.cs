using System;
using UnityEngine;

namespace Parameter{
    /// <summary>
    /// ボーナス処理の共有のための抽象クラス
    /// </summary>
	public abstract class BonusBase {
        /// <summary> ボーナス名 </summary>
		protected string name;
        /// <summary> ボーナス量 </summary>
		protected int bonusValue;
        /// <summary> 効果時間 </summary>
		protected float limit;


		/// <summary>
        /// ボーナス名を取得します
        /// </summary>
        /// <returns>ボーナス名</returns>
		public string getName(){
			return name;
		}

		/// <summary>
        /// ボーナス量を返します
        /// </summary>
        /// <returns>ボーナス量</returns>
		public int getBonusValue(){
			return bonusValue;
		}

        /// <summary>
        /// 次のフレームに移行し、効果時間を更新します
        /// </summary>
        /// <returns><c>true</c>, 効果時間がまだある, <c>false</c> 効果切れ</returns>
		public bool nextFrame(){
            this.limit -= Time.deltaTime;
			return (this.limit > 0);
		}
	}
}

