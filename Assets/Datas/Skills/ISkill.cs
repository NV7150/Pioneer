using Character;

namespace Skill {
    public interface ISkill {
        /// <summary>
        /// スキル名を取得します
        /// </summary>
        /// <returns>スキル名</returns>
        string getName();

        /// <summary>
        /// スキルの説明を取得します
        /// </summary>
        /// <returns>The description.</returns>
        string getDescription();

        /// <summary>
        /// スキルのフレーバーテキストを取得します
        /// </summary>
        /// <returns>フレーバーテキスト</returns>
        string getFlavorText();

		/// <summary>
        /// スキル種別IDを取得します
        /// </summary>
        /// <returns>スキルID</returns>
		int getId();

        /// <summary>
        /// スキルのMPコストを取得します
        /// </summary>
        /// <returns>コスト</returns>
        int getCost();
    }
}