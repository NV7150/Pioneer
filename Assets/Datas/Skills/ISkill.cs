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
        /// スキル種別IDを取得します
        /// </summary>
        /// <returns>スキルID</returns>
		int getId();
    }
}