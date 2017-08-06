using System;
using System.Collections;
using System.Collections.Generic;
using Parameter;
using UnityEngine;

using MasterData;
using TalkSystem;
using Quest;

using QuestType = Quest.QuestParameters.QuestType;
using FriendlyCharacterType = Parameter.CharacterParameters.FriendlyCharacterType;
using FriendlyAbility = Parameter.CharacterParameters.FriendlyAbility;

namespace Character {
    public class Client : IFriendly{
		private readonly int ID;
		private readonly long UNIQUE_ID;
		private readonly string NAME;
		private readonly List<string> MASSAGES;
		private readonly List<string> UNDERTOOK_MASSAGES;
		private readonly List<string> CLEAR_MASSAGES;
        private Dictionary<FriendlyAbility, int> abilities = new Dictionary<FriendlyAbility, int>();

        private readonly int LEVEL;
        private readonly QuestType QUEST_TYPE;
        private IQuest quest;
		private Container container;

        public Client(ClientBuilder builer) {
			this.ID = builer.getId();
			this.NAME = builer.getName();
			this.MASSAGES = builer.getMassges();
            this.UNDERTOOK_MASSAGES = builer.getUnderTookMassges();
            this.CLEAR_MASSAGES = builer.getClearedMassges();
            this.LEVEL = builer.getLevel();
            this.QUEST_TYPE = builer.getQuestType();
            this.abilities.Add(FriendlyAbility.DEX,builer.getDex());
            this.abilities.Add(FriendlyAbility.SPC,builer.getSpc());
			this.UNIQUE_ID = UniqueIdCreator.creatUniqueId();

			var modelPrefab = (GameObject)Resources.Load("Models/" + builer.getModelId());
			container = MonoBehaviour.Instantiate(modelPrefab).GetComponent<Container>();
			container.setCharacter(this);
		}

        public void act() {
            
        }

        public void death() {
            MonoBehaviour.Destroy(container.gameObject);
        }

        public FriendlyCharacterType getCharacterType() {
            return FriendlyCharacterType.CLIENT;
        }

        public Container getContainer() {
            return container;
        }

        public int getFriendlyAbility(FriendlyAbility ability) {
            return abilities[ability];
        }

        public int getId() {
            return ID;
        }

        public string getName() {
            return NAME;
        }

        public long getUniqueId() {
            return UNIQUE_ID;
        }

        public int getLevel(){
            return LEVEL;
        }

        public void talk(IFriendly friendly) {
			if (friendly is Hero) {
				var player = (Hero)friendly;

                if(this.quest != null){
                    if(quest.isCleared()){
                        clearedTalk(player);
                    }else{
                        undertakingTalk();
                    }
                }else{
                    requestTalk(player.getFlagList());
                }
            }
        }

        private IQuest creatQuest(FlagList flags){
            this.quest = QuestHelper.getTypeQuest(QUEST_TYPE, flags, this);
            return quest;
        }

        private void requestTalk(FlagList flags){
            TalkManager.getInstance().talk(MASSAGES);
            creatQuest(flags);
        }

        private void undertakingTalk(){
            TalkManager.getInstance().talk(UNDERTOOK_MASSAGES);
        }

        private void clearedTalk(Hero player){
            TalkManager.getInstance().talk(CLEAR_MASSAGES);
            quest.activateCompensation(player);
            player.deleteQuest(quest);
        }
    }
}