namespace Parameter{﻿
	public interface IIdentity {
		/// <summary>
        /// 特徴名を返します
        /// </summary>
        /// <returns>特徴名</returns>
		string getName();

		/// <summary>
        /// 特徴を適用します
        /// </summary>
		void activate();
	}
}
