using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Character;
using MasterData;

using FriendlyAbility = Parameter.CharacterParameters.FriendlyAbility;
using static Item.ItemParameters.ItemType;

namespace Item {
    public static class ItemHelper {
        /// <summary>
        /// 武器を生成します
        /// </summary>
        /// <returns>生成した武器</returns>
        /// <param name="shape">武器の形状</param>
        /// <param name="material">武器の素材</param>
        /// <param name="creator">武器の作成者</param>
        /// <param name="qualityBonus">品質の補正値</param>
        public static Weapon creatWeaopn(WeaponShape shape, ItemMaterial material, IFriendly creator, float qualityBonus) {
            float dex = creator.getFriendlyAbility(FriendlyAbility.DEX);
            float diffculty = shape.getCreatDifficulty();
            float baseQuality = material.getQuality();

            float quality = weaponQualityCaliculate(dex, diffculty, baseQuality, qualityBonus);
            return new Weapon(shape, material, quality);
        }

        public static Weapon creatRandomLevelWeapon(int level,IFriendly creator = null, float qualityBonus = 0){
            int numberOfWeponShape = WeaponShapeMasterManager.getInstance().getNumberOfShapes();
            int shpeRand = Random.Range(0, numberOfWeponShape);
            WeaponShape shape = WeaponShapeMasterManager.getInstance().getShapeFromId(shpeRand);

            var weponMaterials = ItemMaterialMasterManager.getInstance().getMaterialFromLevel(level);
            int materialRand = Random.Range(0, weponMaterials.Count);
            ItemMaterial material = weponMaterials[materialRand];

            if (creator == null) {
                float dex = material.getQuality() / 20;
                float diffculty = shape.getCreatDifficulty();
                float baseQuality = material.getQuality();
                qualityBonus = dex / 2;

                float quality = weaponQualityCaliculate(dex, diffculty, baseQuality, qualityBonus);
                return new Weapon(shape, material, quality);
            } else {
                return creatWeaopn(shape, material, creator, qualityBonus);
            }
        }

        public static Armor creatArmor(ArmorShape shape, ItemMaterial material, IFriendly creator, float qualityBonus){
			float dex = creator.getFriendlyAbility(FriendlyAbility.DEX);
            float diffculty = shape.getCreatDifficulty();
			float baseQuality = material.getQuality();

            float quality = weaponQualityCaliculate(dex, diffculty, baseQuality, qualityBonus);
            return new Armor(shape, material, quality);
        }

        public static Armor creatRandomLevelArmor(int level,IFriendly creator = null, float qualityBonus = 0){
            int numberOfWeponShape = ArmorShapeMasterManager.getInstance().getNumberOfShapes();
            int shpeRand = Random.Range(0, numberOfWeponShape);
            ArmorShape shape = ArmorShapeMasterManager.getInstance().getShapeFromId(shpeRand);

            var armorMaterials = ItemMaterialMasterManager.getInstance().getMaterialFromLevel(level);
            int materialRand = Random.Range(0, armorMaterials.Count);
            ItemMaterial material = armorMaterials[materialRand];

            if (creator == null) {
                float dex = material.getQuality() / 20;
                float diffculty = shape.getCreatDifficulty();
                float baseQuality = material.getQuality();
                qualityBonus = dex / 2;

                float quality = weaponQualityCaliculate(dex, diffculty, baseQuality, qualityBonus);
                return new Armor(shape, material, quality);
            } else {
                return creatArmor(shape, material, creator, qualityBonus);
            }
        }

        private static float weaponQualityCaliculate(float dex, float difficulty, float baseQuality, float qualityBonus){
            float rand = 2 * baseQuality * Random.Range( - ( dex + qualityBonus ) , dex + qualityBonus );
            float quality = (20 * baseQuality * (dex + qualityBonus) + rand) / (baseQuality + 20 * difficulty);
            return quality;
        }

        public static List<HealItem> creatRandomLevelHealItem(int level,int orderNumber){
            var healItems = new List<HealItem>();
            var registeredHealItemsList = HealItemMasterManager.getInstance().getHealItemsFromLevel(level);
            for (int i = 0;i < orderNumber && registeredHealItemsList.Count > 0;i++){
                int rand = Random.Range(0,registeredHealItemsList.Count);
                healItems.Add(registeredHealItemsList[rand]);
                registeredHealItemsList.Remove(registeredHealItemsList[rand]);
            }
            return healItems;
        }

        public static List<ItemMaterial> creatRandomLevelItemMaterial(int level, int orderNumber) {
            var materials = new List<ItemMaterial>();
            var registeredMaterialsList = ItemMaterialMasterManager.getInstance().getMaterialFromLevel(level);
			for (int i = 0; i < orderNumber && registeredMaterialsList.Count > 0; i++) {
				int rand = Random.Range(0, registeredMaterialsList.Count);
				materials.Add(registeredMaterialsList[rand]);
				registeredMaterialsList.Remove(registeredMaterialsList[rand]);
			}
            return materials;
		}

        public static List<TradeItem> creatRandomLevelTradeItem(int level, int orderNumber){
            var tradeItems = new List<TradeItem>();
            var reagisteredTradeItemList = TradeItemMasterManager.getInstance().getTradeItemsFromLevel(level);
            for (int i = 0; i < orderNumber && reagisteredTradeItemList.Count > 0; i++){
                int rand = Random.Range(0, reagisteredTradeItemList.Count);
                tradeItems.Add(reagisteredTradeItemList[rand]);
                reagisteredTradeItemList.Remove(reagisteredTradeItemList[rand]);
            }
            return tradeItems;
        }

        public static List<SkillBook> creatRandomLevelSkillBook(int level, int orderNumber) {
            var skillBooks = new List<SkillBook>();
            var reagisteredSkillBookList = SkillBookDataManager.getInstance().getSkillBooksFromLevel(level);
			for (int i = 0; i < orderNumber && reagisteredSkillBookList.Count > 0; i++) {
				int rand = Random.Range(0, reagisteredSkillBookList.Count);
				skillBooks.Add(reagisteredSkillBookList[rand]);
				reagisteredSkillBookList.Remove(reagisteredSkillBookList[rand]);
			}
			return skillBooks;
		}

        public static float searchQuality(IItem item){
            switch(item.getItemType()){
                case WEAPON:
                    Weapon wepon = (Weapon)item;
                    return wepon.getQuality();
                case ARMOR:
                    Armor armor = (Armor)item;
                    return armor.getQuality();
            }
            throw new System.ArgumentException("invalid item");
        }

        public static bool isEquipment(IItem item){
            return item.getItemType() == WEAPON || item.getItemType() == ARMOR;
        }
    }
}
