using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Character;
using MasterData;
using Item;

using QuestType = Quest.QuestParameters.QuestType;
using CompentionType = Quest.QuestParameters.CompentionType;

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

        private CompentionType compentionType;

        public ExterminationQuest(FlagList flags, Client client) {
            this.LEVEL = client.Level;
            this.client = client;
            this.flags = flags;

            this.TARGET_ID = EnemyHelper.getRandomEnemyFromLevel(LEVEL);

			this.INTERNAL_NUMBER = flags.getEnemyKilled(TARGET_ID);

            var compensationTypes = Enum.GetValues(typeof(CompentionType));
            int compensationTypeRand = UnityEngine.Random.Range(0,compensationTypes.Length - 1);
            this.compentionType = (CompentionType)compensationTypes.GetValue(compensationTypeRand);

            EXTERMINATION_NUMBER = LEVEL + UnityEngine.Random.Range(2, 6);

            var targetName = EnemyMasterManager.getInstance().getEnemyBuilderFromId(TARGET_ID).getName();

            NAME = "討伐依頼";
            DESCRIPTION = targetName + "を" + EXTERMINATION_NUMBER;
            FLAVOR_TEXT = "最近問題になっている" + targetName + "を駆除する依頼";
		}

        public ExterminationQuest(ExterminationMissonBuilder builder, FlagList flags){
            this.flags = flags;

            this.LEVEL = builder.getLevel();
            this.TARGET_ID = builder.getTargetId();
            this.compentionType = CompentionType.FINISH;
            this.EXTERMINATION_NUMBER = builder.getExterminationNumber();
            INTERNAL_NUMBER = flags.getEnemyKilled(TARGET_ID);

            this.NAME = builder.getName();
            this.DESCRIPTION = builder.getDescription();
            this.FLAVOR_TEXT = builder.getFlavorText();
        }

        public void activateCompensation(Player player) {
            if(compentionType == CompentionType.FINISH){
                PioneerManager.getInstance().resultPrint();
            }else if(compentionType == CompentionType.METAL){
                player.addMetal(LEVEL * 10 * EXTERMINATION_NUMBER);
            }else{
                player.addItem(getCompensationItem());
            }
        }

        private IItem getCompensationItem(){
            int itemLevel = LEVEL + (EXTERMINATION_NUMBER / (LEVEL + 3));
            switch(compentionType){
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

        public CompentionType getCompentionType() {
            return compentionType;
        }
    }

    public class ExterminationMissonBuilder : IMissionBuilder{
        private readonly int
        TARGET_ID,
        LEVEL,
        EXTERMINATION_NUMBER;

        private readonly string
        NAME,
        DESCRIPTION,
        FLAVOR_TEXT;

        public ExterminationMissonBuilder(int baseLevel){
			this.LEVEL = 1;

			var ids = EnemyMasterManager.getInstance().getEnemyIdsFromLevel(LEVEL);
            Debug.Log(ids.Count);
			int idRand = UnityEngine.Random.Range(0, ids.Count);
            this.TARGET_ID = idRand;

			EXTERMINATION_NUMBER = LEVEL + UnityEngine.Random.Range(10, 15);

            this.NAME = "修行";
            this.DESCRIPTION = EnemyMasterManager.getInstance().getEnemyNameFromId(TARGET_ID) + "を" + EXTERMINATION_NUMBER + "体倒す";
            this.FLAVOR_TEXT = "あなたは強さを求めている。";
            this.FLAVOR_TEXT += "そのために、" + EnemyMasterManager.getInstance().getEnemyNameFromId(TARGET_ID) + "を倒すのが最適だと考えた。";

        }

        public int getTargetId(){
            return TARGET_ID;
        }

        public int getLevel(){
            return LEVEL;
        }

        public int getExterminationNumber(){
            return EXTERMINATION_NUMBER;
        }

        public IQuest build(FlagList flags){
            return new ExterminationQuest(this, flags);
        }

        public string getName() {
            return NAME;
        }

        public string getDescription() {
            return DESCRIPTION;
        }

        public string getFlavorText() {
            return FLAVOR_TEXT;
        }
    }
}