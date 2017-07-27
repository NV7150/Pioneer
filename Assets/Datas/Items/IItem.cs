using Character;

namespace Item {
    public interface IItem {
        /// <summary>
        /// アイテムのIDを取得します
        /// </summary>
        /// <returns>アイテムのID</returns>
        int getId();

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

        /// <summary>
        /// アイテムをインベントリに格納できるかを取得します
        /// </summary>
        /// <returns><c>true</c>, 格納可能, <c>false</c> 不可能</returns>
        bool getCanStore();

        /// <summary>
        /// アイテムのフレーバーテキストを取得します
        /// </summary>
        /// <returns>フレーバーテキスト</returns>
        string getFlavorText();

        /// <summary>
        /// スタック(複数のアイテムを一つのアイテムとして所持)可能かを表すフラグを取得します
        /// </summary>
        /// <returns><c>true</c>, スタック可能, <c>false</c> スタック不可</returns>
        bool getCanStack();
    }
}
