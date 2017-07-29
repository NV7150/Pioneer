﻿using System.Collections;
using System.Collections.Generic;

namespace Item{
	public class ItemStack {
		/// <summary>
        /// スタックするアイテム
        /// </summary>
        private readonly IItem ITEM;

        /// <summary>
        /// アイテムのスタック数
        /// </summary>
        private int numberOfStack = 0;


        public ItemStack(IItem item){
            this.ITEM = item;
        }

        /// <summary>
        /// アイテムを一つ追加します
        /// </summary>
        /// <param name="item">アイテム</param>
        public void add(IItem item) {
            if (!item.Equals(ITEM))
                throw new System.ArgumentException("item " + item.getName() + " can't be stored!");
            
            numberOfStack++;

            UnityEngine.Debug.Log("add stack is " + numberOfStack);
        }

        /// <summary>
        /// アイテムの合計重量を返します
        /// </summary>
        /// <returns>合計重量</returns>
        public int getMass(){
            return ITEM.getMass() * numberOfStack;
        }

        /// <summary>
        /// アイテムを取得します
        /// </summary>
        /// <returns>アイテム</returns>
        public IItem getItem(){
            return ITEM;
        }

        /// <summary>
        /// スタックを一つ減らします
        /// </summary>
        /// <returns>スタックがもうない時、falseを返します</returns>
        public bool take(){
            numberOfStack--;
            UnityEngine.Debug.Log("remove stack is " + numberOfStack);
            return (numberOfStack > 0);
        }

        public int getNumberOfStack(){
            return numberOfStack;
        }
	}
}
