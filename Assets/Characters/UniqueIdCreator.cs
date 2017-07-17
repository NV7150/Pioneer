using System;

namespace Character {
	public static class UniqueIdCreator {
		/// <summary> 現在のユニークナンバー </summary>
		private static long nowNumber = 0;

		/// <summary>
        /// ユニークIDを生成します
        /// </summary>
        /// <returns>ユニークID</returns>
		public static long creatUniqueId(){
			long number = nowNumber;
			nowNumber++;
			return number;
		}
	}
}

