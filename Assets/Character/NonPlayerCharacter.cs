using System;

namespace character {
	public interface NonPlayerCharacter : ICharacter{
		//AIに応じた行動を行います
		void act();
	}
}

