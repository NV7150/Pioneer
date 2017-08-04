using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Character;
using MasterData;
using Item;

using QuestType = Quest.QuestParameters.QuestType;

using ItemType = Item.ItemParameters.ItemType;
using static Item.ItemParameters.ItemType;

namespace Quest {
    public class ExterminationQuest : IQuest {
        private readonly int
        TARGET_ID,
        LEVEL,
        EXTERMINATION_NUMBER,
        INTERNAL_NUMBER;

        private readonly string
        NAME,
        DESCRIPTION,
        FLAVOR_TEXT;

        private FlagList flags;

        private Client client;

        private CompentionType type;

        public ExterminationQuest(FlagList flags, Client client) {
            this.LEVEL = client.getLevel();
            this.client = client;
            this.flags = flags;

            var ids = EnemyMasterManager.getEnemyIdsFromLevel(LEVEL);
            int idRand = UnityEngine.Random.Range(0, ids.Count);
			this.TARGET_ID = ids[idRand];

			this.INTERNAL_NUMBER = flags.getEnemyKilled(TARGET_ID);

            var compensationTypes = Enum.GetValues(typeof(CompentionType));
            int compensationTypeRand = UnityEngine.Random.Range(0,compensationTypes.Length - 1);
            this.type = (CompentionType)compensationTypes.GetValue(compensationTypeRand);

            EXTERMINATION_NUMBER = LEVEL + UnityEngine.Random.Range(2, 6);

            Debug.Log(EnemyMasterManager.getEnemyFromId(TARGET_ID).getName() + "こすう" + EXTERMINATION_NUMBER);
		}

        public void activateCompensation(Hero player) {
            if(type == CompentionType.FINISH){
                
            }else if(type == CompentionType.METAL){
                player.addMetal(LEVEL * 10 * EXTERMINATION_NUMBER);
            }else{
                player.addItem(getCompensationItem());
            }
        }

        private IItem getCompensationItem(){
            int itemLevel = LEVEL + (EXTERMINATION_NUMBER / (LEVEL + 3));
            switch(type){
                case CompentionType.WEAPON:
                    return ItemHelper.creatRandomLevelWeapon(itemLevel, client);
                case CompentionType.ARMOR:
                    return ItemHelper.creatRandomLevelArmor(itemLevel, client);
                case CompentionType.MATERIAL:
                    return ItemHelper.creatRandomLevelItemMaterial(itemLevel,1)[0];
                case CompentionType.HEAL_ITEM:
                    return ItemHelper.creatRandomLevelHealItem(itemLevel, 1)[0];
            }
            throw new NotSupportedException("unknown CompentionType");
        }

        public string getDescription() {
            return DESCRIPTION;
        }

        public string getFlavorText() {
            return FLAVOR_TEXT;
        }

        public string getName() {
            return NAME;
        }

        public int getCurrentNumber(){
            return flags.getEnemyKilled(TARGET_ID) - INTERNAL_NUMBER;
        }

        public int getExterminationNumber(){
            return EXTERMINATION_NUMBER;
        }

        public bool isCleared() {
            return (flags.getEnemyKilled(TARGET_ID) - INTERNAL_NUMBER) >= EXTERMINATION_NUMBER;
        }

        public QuestType getQuestType() {
            return QuestType.EXTERMINATION;
        }

        public Client getQuester() {
            return client;
        }

        private enum CompentionType {
            WEAPON,
            ARMOR,
            MATERIAL,
            HEAL_ITEM,
            METAL,
            FINISH
        }
    }
}