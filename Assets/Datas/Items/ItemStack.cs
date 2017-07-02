using System.Collections;
using System.Collections.Generic;

namespace Item{
	public class ItemStack {
		//アイテムのスタックです
		private Stack<IItem> stack = new Stack<IItem>();

		//アイテムを一つ外します
		public IItem take(){
			return stack.Pop();
		}

		//アイテムを一つ追加します
		public void add(IItem item){
			stack.Push (item);
		}
	}
}
