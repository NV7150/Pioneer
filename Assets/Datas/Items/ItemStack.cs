using System.Collections;
using System.Collections.Generic;

namespace item{
	public class ItemStack {
		private Stack<Item> stack = new Stack<Item>();
		public Item take(){
			return stack.Pop();
		}

		public void add(Item item){
			stack.Push (item);
		}
	}
}
