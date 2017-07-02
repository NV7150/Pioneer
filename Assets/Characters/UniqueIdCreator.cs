using System;

namespace Character {
	public class UniqueIdCreator {
		//現在のナンバーを表します
		private static long nowNumber = 0;

		//シングルトンです
		private UniqueIdCreator(){}

		//ユニークIDを取得します。種族のIdの末尾に現在の番号を加えた数がその値になります。
		public static long creatUniqueId(){
			long number = nowNumber;
			nowNumber++;
			return number;
		}
	}
}

