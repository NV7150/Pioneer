using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Character;

namespace Item {
    public class Inventory {

        /// <summary>
        /// スタック不可なアイテムのリスト
        /// </summary>
        private List<IItem> inventory = new List<IItem>();

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
                    inventory.Add(stack);
                }

                //すでにあったら追加
                stack.add(item);
            } else {
                //スタック不可は問答無用で追加
                inventory.Add(item);
            }
        }

        /// <summary>
        /// アイテムのリストを取得します
        /// </summary>
        /// <returns>アイテムのリスト</returns>
        public List<IItem> getItems() {
            return new List<IItem>(inventory);
        }

        public ItemStack getStack(IItem item){
			if (!item.getCanStack())
				throw new System.ArgumentException("item " + item + " can't stack");

            foreach(IItem inventoryItem in inventory){
                if (inventoryItem.getCanStack() && item.Equals(inventoryItem))
                    return (ItemStack)inventoryItem;
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

            foreach (IItem inventoryItem in inventory) {
                if (inventoryItem.getCanStack()) {
                    var stack = (ItemStack)inventoryItem;
                    item = (item is ItemStack) ? ((ItemStack)item).getItem() : item;
                    if (item.Equals(stack.getItem())) {
                        return stack;
                    }
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
                    inventory.Remove(stack);
				}
			} else {
				//スタック不可は無条件でインベントリから削除
                inventory.Remove(item);
			}
        }
    }
}
