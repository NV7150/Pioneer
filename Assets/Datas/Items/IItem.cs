using Character;

namespace Item {
    public interface IItem {
        /// <summary>
        /// アイテム名を取得します
        /// </summary>
        /// <returns>アイテム名</returns>
        string getName();

        /// <summary>
        /// アイテムの説明文を取得します
        /// </summary>
        /// <returns>アイテムの説明文</returns>
        string getDescription();

        /// <summary>
        /// アイテムを取得します
        /// </summary>
        /// <param name="user">使用するキャラクター</param>
		void use(IPlayable user);

        /// <summary>
        /// アイテムの重量を取得します
        /// </summary>
        /// <returns>アイテムの重量</returns>
        int getMass();

		/// <summary>
        /// アイテムの基本価格を取得します
        /// </summary>
        /// <returns>基本価格</returns>
		int getItemValue();
    }
}
