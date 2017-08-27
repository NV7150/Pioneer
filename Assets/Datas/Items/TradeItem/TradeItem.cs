using System;
using System.Collections;
using System.Collections.Generic;
using Character;
using UnityEngine;

using MasterData;

using ItemAttribute = Item.ItemParameters.ItemAttribute;
using ItemType = Item.ItemParameters.ItemType;

namespace Item {
    public class TradeItem : IItem {
        private readonly int ID;
        private readonly string NAME;
        private readonly string DESCRIPTION;
        private readonly string FLAVOR_TEXT;
        private readonly ItemAttribute ITEM_ATTRIBUTE;
        private readonly int ITEM_VALUE;
        private readonly int MASS;
        private readonly int LEVEL;

        public TradeItem(TradeItemBuilder builder){
            this.ID = builder.Id;
            this.NAME = builder.Name;
            this.DESCRIPTION = builder.Description;
            this.FLAVOR_TEXT = builder.FlavorText;
            this.ITEM_ATTRIBUTE = builder.ItemAttribute;
            this.ITEM_VALUE = builder.ItemValue;
            this.MASS = builder.Mass;
            this.LEVEL = builder.Level;
        }

        public bool getCanStack() {
            return true;
        }

        public bool getCanStore() {
            return true;
        }

        public string getDescription() {
            return DESCRIPTION;
        }

        public string getFlavorText() {
            return FLAVOR_TEXT;
        }

        public int getId() {
            return ID;
        }

        public ItemAttribute getItemAttribute() {
            return ITEM_ATTRIBUTE;
        }

        public ItemType getItemType() {
            return ItemType.TRADING_ITEM;
        }

        public int getItemValue() {
            return ITEM_VALUE;
        }

        public int getMass() {
            return MASS;
        }

        public string getName() {
            return NAME;
        }

        public void use(IPlayable user) {}
    }
}
