using System;

namespace Character {
	public interface  IFriendly {
		//話術(spc)を取得します
		int getSpc();

		//話します
		void talk(IFriendly friendly);
	}
}

