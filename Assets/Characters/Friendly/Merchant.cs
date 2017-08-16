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
using FriendlyCharacterType = Parameter.CharacterParameters.FriendlyCharacterType;
using Random = UnityEngine.Random;

using static Parameter.CharacterParameters.FriendlyAbility;

namespace Character {
    public class Merchant : IFriendly {
        private readonly int TRADE_INDEX;
        private readonly long UNIQUE_ID;

        private readonly string NAME;

        private readonly List<IItem> GOODS = new List<IItem>();

        private readonly List<string> massages = new List<string>();
        private readonly string failMassage;

        private readonly ItemType GOODS_TYPE;

        private Town livingTown;

        private int id;
        public int Id{
            get { return id; }
        }

		private int numberOfGoods;
        public int NumberOfGoods{
            get { return numberOfGoods; }
        }

		private int goodsLevel;
        public int GoodsLevel{
            get { return goodsLevel; }
        }

		private Container container;
        public Transform containerTransform{
            get { return container.transform; }
            set{
                this.container.transform.position = value.position;
                this.container.transform.rotation = value.rotation;
            }
        }

		private Dictionary<FriendlyAbility, int> abilities = new Dictionary<FriendlyAbility, int>();
        public Dictionary<FriendlyAbility,int> Abilities{
            get { return new Dictionary<FriendlyAbility, int>(abilities); }
        }

        public Merchant(MerchantBuiler builder,Town livingTown){
            id = builder.getId();
            NAME = builder.getName();
            GameObject modelPrefab = (GameObject)Resources.Load("Models/" + builder.getModelId());
            container = MonoBehaviour.Instantiate(modelPrefab).GetComponent<Container>();
            container.setCharacter(this);

            massages = builder.getMassges();
            TRADE_INDEX = builder.getStartTradeIndex();
            UNIQUE_ID = UniqueIdCreator.creatUniqueId();
            GOODS_TYPE = builder.getGoodsType();
            numberOfGoods = builder.getNumberOfGoods();
            goodsLevel = builder.getGoodsLevel();

            abilities.Add(FriendlyAbility.DEX,builder.getDex());
            abilities.Add(FriendlyAbility.SPC,builder.getSpc());

            failMassage = builder.getFailMassage();

            updateGoods();

            this.livingTown = livingTown;
        }

        public Merchant(int id, int level, int goodsNumber, Dictionary<FriendlyAbility, int> abilities, Transform transform){
            this.goodsLevel = level;
            this.numberOfGoods = goodsNumber;

            this.abilities = new Dictionary<FriendlyAbility, int>(abilities);

            var builder = MerchantMasterManager.getMerchantBuilderFromId(id);
			massages = builder.getMassges();
			TRADE_INDEX = builder.getStartTradeIndex();
			UNIQUE_ID = UniqueIdCreator.creatUniqueId();
			GOODS_TYPE = builder.getGoodsType();

			GameObject modelPrefab = (GameObject)Resources.Load("Models/" + builder.getModelId());
			MonoBehaviour.Destroy(transform.gameObject);
            this.container = MonoBehaviour.Instantiate(modelPrefab,transform.position,transform.rotation).GetComponent<Container>();
            container.setCharacter(this);

            Debug.Log("<color=yellow>called</color>");

            updateGoods();
        }

        private void updateGoods(){
			if (GOODS_TYPE == ItemType.WEAPON || GOODS_TYPE == ItemType.ARMOR) {
				for (int i = 0; i < numberOfGoods; i++) {
					GOODS.Add(creatEquipment());
				}
			} else {
				GOODS.AddRange(creatItem());
			}
        }

        private IItem creatEquipment(){
            switch(GOODS_TYPE){
                case ItemType.WEAPON:
                    return ItemHelper.creatRandomLevelWeapon(goodsLevel, this, abilities[FriendlyAbility.DEX] / 2);
                case ItemType.ARMOR:
                    return ItemHelper.creatRandomLevelArmor(goodsLevel, this, abilities[FriendlyAbility.DEX] / 2);
            }
            throw new NotSupportedException("unkown itemType");
        }

        private List<IItem> creatItem(){
            switch(GOODS_TYPE){
                case ItemType.HEAL_ITEM:
                    return ItemHelper.creatRandomLevelHealItem(goodsLevel, numberOfGoods).ConvertAll(c => (IItem)c);
                case ItemType.ITEM_MATERIAL:
                    return ItemHelper.creatRandomLevelItemMaterial(goodsLevel, numberOfGoods).ConvertAll(c => (IItem)c);
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
            return numberOfGoods;
		}

		public ItemType getGoodsType() {
            return GOODS_TYPE;
		}

        public int getGoodsLevel(){
            return goodsLevel;
        }

        public float getValueMag(ItemAttribute itemAttribute){
            return livingTown.getItemValueMag(itemAttribute);
        }

        public FriendlyCharacterType getCharacterType() {
            return FriendlyCharacterType.MERCHANT;
        }

        public int getId() {
            return id;
        }

        public int getLevel(){
            return goodsLevel;
        }

        public void setTown(Town town){
            this.livingTown = town;
            container.setCharacter(this);
        }

        public void levelup(){
            goodsLevel++;
            numberOfGoods += Random.Range(1, 4);
			abilities[SPC] += Random.Range(1, 3);
            abilities[DEX] += Random.Range(1, 3);

            updateGoods();
        }

        public void traded(int tradedValue,IItem item){
            livingTown.traded(this,tradedValue,item.getItemAttribute());
        }
    }
}

