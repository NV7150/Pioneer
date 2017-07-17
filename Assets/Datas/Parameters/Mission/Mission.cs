
namespace Parameter{
	public interface Mission{
		/// <summary>
        /// 使命名を取得します
        /// </summary>
        /// <returns>使命名</returns>
		string getName ();

		/// <summary>
		/// 使命が達成されていたか判定します
		/// </summary>
		/// /// <returns><c>true</c>, 達成, <c>false</c> 未達成</returns>
		/// <param name="flugs">判定したいフラグリスト</param>
		bool cheak(FlugList flugs);
	}
}
