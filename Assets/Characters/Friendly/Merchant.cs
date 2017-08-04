using System;
using System.Collections;
using System.Collections.Generic;
using Parameter;
using UnityEngine;

using MasterData;
using Item;
using FieldMap;
using TalkSystem;

using FriendlyAbility = Parameter.CharacterParameters.FriendlyAbility;
using ItemType = Item.ItemParameters.ItemType;
using ItemAttribute = Item.ItemParameters.ItemAttribute;

namespace Character {
    public class Merchant : IFriendly {
        private readonly int ID;
        private readonly int TRADE_INDEX;
        private readonly long UNIQUE_ID;

        private readonly string NAME;

        private readonly List<IItem> GOODS = new List<IItem>();
        private readonly int NUMBER_OF_GOODS;
        private readonly int GOODS_LEVEL;
        private readonly List<string> massages = new List<string>();
        private readonly string failMassage;

        private readonly ItemType GOODS_TYPE;

        private Container container;

        private Dictionary<FriendlyAbility, int> abilities = new Dictionary<FriendlyAbility, int>();

        private Town livingTown;

        public Merchant(MerchantBuiler builder,Town livingTown){
            ID = builder.getId();
            NAME = builder.getName();
            GameObject modelPrefab = (GameObject)Resources.Load("Models/" + builder.getModelId());
            container = MonoBehaviour.Instantiate(modelPrefab).GetComponent<Container>();
            container.setCharacter(this);

            this.massages = builder.getMassges();
            this.TRADE_INDEX = builder.getStartTradeIndex();
            UNIQUE_ID = UniqueIdCreator.creatUniqueId();
            GOODS_TYPE = builder.getGoodsType();
            NUMBER_OF_GOODS = builder.getNumberOfGoods();
            GOODS_LEVEL = builder.getGoodsLevel();

            abilities.Add(FriendlyAbility.DEX,builder.getDex());
            abilities.Add(FriendlyAbility.SPC,builder.getSpc());

            failMassage = builder.getFailMassage();

            if(GOODS_TYPE == ItemType.WEAPON || GOODS_TYPE == ItemType.ARMOR){
                for (int i = 0; i < NUMBER_OF_GOODS;i++){
                    GOODS.Add(creatEquipment());
                }
            }else{
                GOODS.AddRange(creatItem());
            }

            this.livingTown = livingTown;
        }

        private IItem creatEquipment(){
            switch(GOODS_TYPE){
                case ItemType.WEAPON:
                    return ItemHelper.creatRandomLevelWeapon(GOODS_LEVEL, this, abilities[FriendlyAbility.DEX] / 2);
                case ItemType.ARMOR:
                    return ItemHelper.creatRandomLevelArmor(GOODS_LEVEL, this, abilities[FriendlyAbility.DEX] / 2);
            }
            throw new NotSupportedException("unkown itemType");
        }

        private List<IItem> creatItem(){
            switch(GOODS_TYPE){
                case ItemType.HEAL_ITEM:
                    return ItemHelper.creatRandomLevelHealItem(GOODS_LEVEL, NUMBER_OF_GOODS).ConvertAll(c => (IItem)c);
                case ItemType.ITEM_MATERIAL:
                    return ItemHelper.creatRandomLevelItemMaterial(GOODS_LEVEL, NUMBER_OF_GOODS).ConvertAll(c => (IItem)c);
			}
			throw new NotSupportedException("unkown itemType");
        }

        public void act() {
            //実装しないと思う
        }

        public void death() {
            MonoBehaviour.Destroy(container.getModel());
        }

        public Container getContainer() {
            return container;
        }

        public string getName() {
            return NAME;
        }

        public int getFriendlyAbility(FriendlyAbility ability) {
            return abilities[ability];
        }

        public long getUniqueId() {
            return UNIQUE_ID;
        }

        public void talk(IFriendly friendly) {
            TalkManager.getInstance().trade(massages, failMassage,TRADE_INDEX, GOODS, (Hero)friendly, this);
        }

		public int getNumberOfGoods() {
            return NUMBER_OF_GOODS;
		}

		public ItemType getGoodsType() {
            return GOODS_TYPE;
		}

        public int getGoodsLevel(){
            return GOODS_LEVEL;
        }

        public float getValueMag(ItemAttribute itemAttribute){
            return livingTown.getItemValueMag(itemAttribute);
        }
    }
}

