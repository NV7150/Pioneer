using System;

namespace character {
	public interface NonPlayerCharacter : Character{
		//AIに応じた行動を行います
		void act();
	}
}

