using UnityEngine;

namespace Character{

	/*Characterインタフェースはこのゲームに登場する全てのキャラクターの実体を表します。
	 *キャラクターはそれぞれ外面(Container)と実体(Character)を持ちます。
	 *Characterは内部的処理を行います。
	 *Containerは外面的処理を行います。また、全ての具象のContainerクラスはMonoBehaviourを継承しています。
	*/
	public interface ICharacter{
		//外面を取得します
		GameObject getModel();

		//Containerを取得します
		Container getContainer();

		//何か行動します。
		void act();

		//いなくなります
		void death();

		//キャラクターそれぞれが固有に持つIDを取得します
		long getUniqueId();

		//名前を取得します
		string getName();
	}	
}