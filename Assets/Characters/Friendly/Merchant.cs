using System;
using System.Collections;
using System.Collections.Generic;
using Parameter;
using UnityEngine;

using MasterData;
using Item;
using TalkSystem;

using FriendlyAbility = Parameter.CharacterParameters.FriendlyAbility;

namespace Character {
    public class Merchant : IFriendly {
        private readonly int ID;
        private readonly int TRADE_INDEX;
        private readonly long UNIQUE_ID;

        private readonly string NAME;

        private readonly Goods goods;
        private readonly List<string> massages = new List<string>();
        private readonly string failMassage;

        private Container container;

        private Dictionary<FriendlyAbility, int> abilities = new Dictionary<FriendlyAbility, int>();

        public Merchant(MerchantBuiler builder){
            ID = builder.getId();
            NAME = builder.getName();
            GameObject modelPrefab = (GameObject)Resources.Load("Models/" + builder.getModelId());
            container = MonoBehaviour.Instantiate(modelPrefab).GetComponent<Container>();
            this.goods = builder.getGoods();
            this.massages = builder.getMassges();
            this.TRADE_INDEX = builder.getStartTradeIndex();
            UNIQUE_ID = UniqueIdCreator.creatUniqueId();

            abilities.Add(FriendlyAbility.DEX,builder.getDex());
            abilities.Add(FriendlyAbility.SPC,builder.getSpc());

            failMassage = builder.getFailMassage();

            foreach(IItem item in goods.getGoods()){
                if (!item.getCanStore())
                    throw new InvalidProgramException("item " + item.getName() + " is can't be sold because it can't be stored");
            }
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

        public int getRawFriendlyAbility(FriendlyAbility ability) {
            return abilities[ability];
        }

        public long getUniqueId() {
            return UNIQUE_ID;
        }

        public void talk(IFriendly friendly) {
            TalkManager.getInstance().trade(massages, failMassage,TRADE_INDEX, goods, (Hero)friendly, this);
        }
    }
}

