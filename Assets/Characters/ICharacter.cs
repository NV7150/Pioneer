using UnityEngine;

namespace Character{
	/// <summary>
	/// Characterインタフェースはこのゲームに登場する全てのキャラクターの実体を表します。
	/// キャラクターはそれぞれ外面(Container)と実体(Character)を持ちます。
	/// Characterは内部的処理を行います。
	/// Containerは外面的処理を行います。また、全ての具象のContainerクラスはMonoBehaviourを継承しています。
	/// </summary>
	public interface ICharacter{

		/// <summary>
        /// せってされているConataierを取得します
        /// </summary>
        /// <returns>Contaierオブジェクト</returns>
		Container getContainer();

		/// <summary>
        /// update()毎に実行される処理
        /// </summary>
		void act();

		/// <summary>
        /// ゲームから消滅します
        /// </summary>
		void death();

		/// <summary>
        /// キャラクターがそれぞれ固有に持つユニークIDを取得します
        /// </summary>
        /// <returns>ユニークID</returns>
		long getUniqueId();

		/// <summary>
        /// 名前を取得します
        /// </summary>
        /// <returns>The name.</returns>
		string getName();
	}	
}