using System.Collections;
using System.Collections.Generic;
using Character;

namespace Item{
    public class ItemStack : IItem {
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
            UnityEngine.Debug.Log("into take");
            numberOfStack--;
            return (numberOfStack > 0);
        }

        public bool hasStack(){
			return (numberOfStack > 0);
        }


        public int getNumberOfStack(){
            return numberOfStack;
        }

        public int getId(){
            return ITEM.getId();
        }

        public string getName(){
            return ITEM.getName();
        }

        public string getDescription(){
            return ITEM.getDescription();
        }

        public void use(IPlayable user){
            if (hasStack()) {
                ITEM.use(user);
                take();
            }
        }

        public int getItemValue(){
            return ITEM.getItemValue();
        }

        public bool getCanStore(){
            return ITEM.getCanStore();
        }

        public string getFlavorText(){
            return ITEM.getFlavorText();
        }

        public bool getCanStack(){
            return ITEM.getCanStack();
        }

        public ItemParameters.ItemType getItemType(){
            return ITEM.getItemType();
        }

        public ItemParameters.ItemAttribute getItemAttribute(){
            return ITEM.getItemAttribute();
        }
    }
}
