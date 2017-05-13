using character;

namespace character{
	public interface FriendlyNPC :NonPlayerCharacter{
		//話します
		void talk();
		//話術(speech)を返します
		int getSpc();
	}
}
  