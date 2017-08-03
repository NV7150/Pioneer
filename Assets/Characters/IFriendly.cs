using System;

using FriendlyAbility = Parameter.CharacterParameters.FriendlyAbility;

namespace Character {
    /// <summary>
    /// IFriednlyは全ての友好的なキャラクターを表します
    /// </summary>
    public interface  IFriendly : ICharacter{
        /// <summary>
        /// 友好的な能力値を取得します
        /// </summary>
        /// <returns>指定された能力値</returns>
        /// <param name="ability">取得したい能力値</param>
        int getFriendlyAbility(FriendlyAbility ability);

		/// <summary>
        /// 話します
        /// </summary>
        /// <param name="friendly">話す対象のIFriendlyキャラクター</param>
		void talk(IFriendly friendly);
	}
}

