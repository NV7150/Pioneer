namespace SelectView{
    /// <summary> セレクトビューに追加する要素のオブジェクト </summary>
	public interface INode<E> {
        /// <summary>
        /// 格納している要素を取得します
        /// </summary>
        /// <returns>格納している要素</returns>
	    E getElement();
	}
}
