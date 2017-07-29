using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Character;

namespace Item {
    public class Inventry {
        /// <summary>
        /// アイテムスタックオブジェクトのリスト
        /// </summary>
        private List<ItemStack> stackables = new List<ItemStack>();

        /// <summary>
        /// スタック不可なアイテムのリスト
        /// </summary>
        private List<IItem> unstackables = new List<IItem>();

        /// <summary>
        /// アイテムをインベントリに追加します
        /// </summary>
        /// <param name="item">アイテム</param>
        public void addItem(IItem item) {
            if (!item.getCanStore())
                throw new System.ArgumentException("item can't be added in inventory");

            if (item.getCanStack()) {
                ItemStack stack = searchStack(item);

                if (stack == null) {
                    //スタックがインベントリになければ作成
                    stack = new ItemStack(item);
                    stackables.Add(stack);
                }
                //すでにあったら追加
                stack.add(item);
            } else {
                //スタック不可は問答無用で追加
                unstackables.Add(item);
            }
        }

        /// <summary>
        /// アイテムのリストを取得します
        /// </summary>
        /// <returns>アイテムのリスト</returns>
        public List<IItem> getItems() {
            List<IItem> items = new List<IItem>();

            foreach (ItemStack stack in stackables) {
                items.Add(stack.getItem());
            }

            foreach (IItem item in unstackables) {
                items.Add(item);
            }

            return items;
        }

        public ItemStack getStack(IItem item){
			if (!item.getCanStack())
				throw new System.ArgumentException("item " + item + " can't stack");

            foreach(ItemStack stack in stackables){
                if (stack.getItem().Equals(item))
                    return stack;
			}

            throw new System.ArgumentException("item " + item + " wasn't found");
        }

        /// <summary>
        /// 指定したアイテムを使用します
        /// </summary>
        /// <param name="item">使用するアイテム</param>
        /// <param name="user">使用者</param>
        public void useItem(IItem item, IPlayable user) {
            removeItem(item);

            //今までの処理でエラーが出なかったら使用
            item.use(user);
        }

        /// <summary>
        /// アイテムスタックを検索します
        /// </summary>
        /// <returns></returns>
        /// <param name="item">Item.</param>
        private ItemStack searchStack(IItem item) {
            if (!item.getCanStack())
                throw new System.ArgumentException("item " + item.getName() + " can't be stack");

            foreach (ItemStack stack in stackables) {
                if (stack.getItem().Equals(item)) {
                    return stack;
                }
            }
            return null;
        }

        /// <summary>
        /// 対象のアイテムをインベントリから削除します
        /// </summary>
        /// <param name="item">削除したいアイテム</param>
        public void removeItem(IItem item){
			if (item.getCanStack()) {
                Debug.Log("into getCanStack");
				ItemStack stack = searchStack(item);

				if (stack == null)
					throw new System.ArgumentException("can't find item " + item.getName());

				//スタックがなくなったらインベントリから削除
				if (!stack.take()) {
					stackables.Remove(stack);
				}
			} else {
				//スタック不可は無条件でインベントリから削除
				unstackables.Remove(item);
			}
        }
    }
}
