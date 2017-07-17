using System.Collections;
using System.Collections.Generic;

namespace Item{
	public class ItemStack {
		/// <summary>
        /// アイテムのスタックを表すStack
        /// </summary>
		private Stack<IItem> stack = new Stack<IItem>();

		/// <summary>
        /// アイテムを１つ取得します
        /// </summary>
        /// <returns>取得したアイテム</returns>
		public IItem take(){
			return stack.Pop();
		}

        /// <summary>
        /// アイテムを一つ追加します
        /// </summary>
        /// <param name="item">アイテム</param>
        public void add(IItem item) {
            stack.Push(item);
        }
	}
}
