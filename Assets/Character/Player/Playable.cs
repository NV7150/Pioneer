using character;
using item;
using parameter;

namespace character{
	public interface Playable : Battleable{
		//対象(武器)を装備します
		void equipWepon(Wepon wepon);

		//対象(防具)を装備します
		void equipArmor(Armor armor);
	}
}
