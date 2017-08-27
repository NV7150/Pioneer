using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using Item;

using ItemAttribute = Item.ItemParameters.ItemAttribute;

namespace MasterData {
    public class TradeItemBuilder{
        private int id;
        public int Id {
            get {
                return id;
            }
        }

        private string name;
        public string Name {
            get {
                return name;
            }
        }

        private string description;
        public string Description {
            get {
                return description;
            }
        }

        private string flavorText;
        public string FlavorText {
            get {
                return flavorText;
            }
        }

        private ItemAttribute itemAttribute;
        public ItemAttribute ItemAttribute {
            get {
                return itemAttribute;
            }
        }

        private int itemValue;
        public int ItemValue {
            get {
                return itemValue;
            }
        }

        private int mass;
        public int Mass {
            get {
                return mass;
            }
        }

        private int level;

        public int Level {
            get {
                return level;
            }
        }

        public TradeItemBuilder(string[] datas){
            id = int.Parse(datas[0]);
            name = datas[1];
            level = int.Parse(datas[2]);
            itemValue = int.Parse(datas[3]);
            mass = int.Parse(datas[4]);
            itemAttribute = (ItemAttribute)Enum.Parse(typeof(ItemAttribute), datas[5]);
            description = datas[6];
            flavorText = datas[7];
        }

        public TradeItem build(){
            return new TradeItem(this);
        }
    }
}