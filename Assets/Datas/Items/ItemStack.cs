using System.Collections;
using System.Collections.Generic;

namespace Item{
	public class ItemStack {
		private Stack<IItem> stack = new Stack<IItem>();
		public IItem take(){
			return stack.Pop();
		}

		public void add(IItem item){
			stack.Push (item);
		}
	}
}
