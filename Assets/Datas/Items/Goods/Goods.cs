using System.Collections.Generic;
using System;

using MasterData;
using ItemType = ItemParameters.ItemType;

namespace Item {
    public class Goods {
        private readonly int ID;
		private readonly string NAME;
		private readonly ItemType GOODS_TYPE;
        private readonly List<int> GOODS_IDS = new List<int>();

        public Goods(string[] datas){
            ID = int.Parse(datas[0]);
            NAME = datas[1];
            GOODS_TYPE = (ItemType)Enum.Parse(typeof(ItemType),datas[2]);
            for (int i = 3; datas[i] != "end";i++){
                GOODS_IDS.Add(int.Parse(datas[i]));
            }
        }

        public List<IItem> getGoods(){
            List<IItem> items = new List<IItem>();
            foreach (int id in GOODS_IDS) {
                switch (GOODS_TYPE) {
                    case ItemType.ARMOR:
                        items.Add(ArmorMasterManager.getArmorFromId(id));
                        break;
                    case ItemType.WEPON:
                        items.Add(WeponMasterManager.getWeponFromId(id));
                        break;
                    case ItemType.HEAL_ITEM:
                        items.Add(HealItemMasterManager.getHealItemFromId(id));
                        break;
                    default:
                        throw new NotSupportedException("the goodsType hasn't made yet");
                }
            }
            return items;
        }

        public IItem getItemFromId(int id){
			switch (GOODS_TYPE) {
				case ItemType.ARMOR:
                    return ArmorMasterManager.getArmorFromId(id);
				case ItemType.WEPON:
                    return WeponMasterManager.getWeponFromId(id);
                case ItemType.HEAL_ITEM:
                    return HealItemMasterManager.getHealItemFromId(id);
				default:
					throw new NotSupportedException("the goodsType hasn't made yet");
			}
        }

        public int getId(){
            return ID;
        }
    }
}
