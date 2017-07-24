using System;
using System.Collections;
using System.Collections.Generic;
using Parameter;
using UnityEngine;

using MasterData;
using Item;

namespace Character {
    public class Marchent : IFriendly {

        public void act() {

        }

        public void death() {
            throw new NotImplementedException();
        }

        public Container getContainer() {
            throw new NotImplementedException();
        }

        public string getName() {
            throw new NotImplementedException();
        }

        public int getRawFriendlyAbility(CharacterParameters.FriendlyAbility ability) {
            throw new NotImplementedException();
        }

        public long getUniqueId() {
            throw new NotImplementedException();
        }

        public void talk(IFriendly friendly) {
            Debug.Log("into talk");
            //かり
            List<string> massages = new List<string>() {
                "何が欲しいんだい?",
                "武器は装備しないと意味がないぞ！",
                "気をつけろよ!"
            };

            List<IItem> goods = new List<IItem>() {
                WeponMasterManager.getWeponFromId(0),
                WeponMasterManager.getWeponFromId(1),
                ArmorMasterManager.getArmorFromId(0)
            };

            TalkManager.getInstance().trade(massages,1, goods, (Hero)friendly, this);
        }
    }
}

