using System;

namespace Character {
    /// <summary>
    /// IFriednlyは全ての友好的なキャラクターを表します
    /// </summary>
    public interface  IFriendly : ICharacter{
		/// <summary>
        /// 話術(speech / spc)を取得します
        /// </summary>
        /// <returns>話術</returns>
		int getSpc();

		/// <summary>
        /// 話します
        /// </summary>
        /// <param name="friendly">話す対象のIFriendlyキャラクター</param>
		void talk(IFriendly friendly);
	}
}

