using System;
using System.Collections;
using System.Collections.Generic;
using Parameter;
using UnityEngine;

using MasterData;
using TalkSystem;
using Quest;
using FieldMap;

using QuestType = Quest.QuestParameters.QuestType;
using FriendlyCharacterType = Parameter.CharacterParameters.FriendlyCharacterType;
using FriendlyAbility = Parameter.CharacterParameters.FriendlyAbility;
using Random = UnityEngine.Random;

using static Parameter.CharacterParameters.FriendlyAbility;

namespace Character {
    public class Client : IFriendly{
        private int id;
		private readonly long UNIQUE_ID;
		private readonly string NAME;
		private readonly List<string> MASSAGES;
		private readonly List<string> UNDERTOOK_MASSAGES;
		private readonly List<string> CLEAR_MASSAGES;
        private Dictionary<FriendlyAbility, int> abilities = new Dictionary<FriendlyAbility, int>();

        private int level;
        private readonly QuestType QUEST_TYPE;
        private IQuest quest;
		private Container container;
        private Town livingTown;

        private bool isActivated = false;
        private bool isCleard = false;
        private Player cleardPlayer;

        private bool needTown;

        public int Id{
            get { return id; }
        }

		public Vector3 ContainerPostion {
			get { return container.transform.position; }
		}

		public Quaternion ContainerRotate {
			get { return container.transform.rotation; }
		}

        public Dictionary<FriendlyAbility,int> Abilities{
            get { return new Dictionary<FriendlyAbility, int>(abilities); }
        }

        public int Level{
            get { return level; }
        }

        public Client(ClientBuilder builder,Town livingTown) {
			this.id = builder.getId();
			this.NAME = builder.getName();
			this.MASSAGES = builder.getMassges();
            this.UNDERTOOK_MASSAGES = builder.getUnderTookMassges();
            this.CLEAR_MASSAGES = builder.getClearedMassges();
            this.level = builder.getLevel();
            this.QUEST_TYPE = builder.getQuestType();
            this.abilities.Add(DEX, builder.getDex());
            this.abilities.Add(SPC,builder.getSpc());
			this.UNIQUE_ID = UniqueIdCreator.creatUniqueId();

			var modelPrefab = (GameObject)Resources.Load("Models/" + builder.getModelId());
			container = MonoBehaviour.Instantiate(modelPrefab).GetComponent<Container>();
			container.setCharacter(this);

            this.livingTown = livingTown;

            needTown = true;
            isActivated = true;
		}

        public Client(int id, Dictionary<FriendlyAbility, int> abilities, int level,Vector3 pos , Quaternion rotate){
            this.id = id;
            this.level = level;
            this.abilities = new Dictionary<FriendlyAbility, int>(abilities);

            var builder = ClientMasterManager.getInstance().getClientBuilderFromId(id);

			this.NAME = builder.getName();
			this.MASSAGES = builder.getMassges();
			this.UNDERTOOK_MASSAGES = builder.getUnderTookMassges();
			this.CLEAR_MASSAGES = builder.getClearedMassges();
			this.QUEST_TYPE = builder.getQuestType();
			this.UNIQUE_ID = UniqueIdCreator.creatUniqueId();
			var modelPrefab = (GameObject)Resources.Load("Models/" + builder.getModelId());
            container = MonoBehaviour.Instantiate(modelPrefab,pos,rotate).GetComponent<Container>();
			container.setCharacter(this);

            needTown = true;
        }

        public Client(IQuest quest, Transform transfrom){
            this.quest = quest;
            this.UNIQUE_ID = UniqueIdCreator.creatUniqueId();

            MASSAGES = new List<string>(){
                "がんばれ"
            };
            UNDERTOOK_MASSAGES = new List<string>() {
                "頑張れ"
            };
            CLEAR_MASSAGES = new List<string>(){
                "おめでとう"
            };

            var modelPrefab = (GameObject)Resources.Load("Models/MissionClient");
            container = MonoBehaviour.Instantiate(modelPrefab).GetComponent<Container>();
            container.setCharacter(this);

            this.container.transform.position = transfrom.position;
            this.container.transform.rotation = transfrom.rotation;

            isActivated = true;
            needTown = false;
        }

        public void setTown(Town livingTown){
            this.livingTown = livingTown;
            isActivated = true;
        }

        public void act() {
            if (isCleard && !TalkManager.getInstance().getIsTalking()) {
                quest.activateCompensation(cleardPlayer);
                cleardPlayer.deleteQuest(quest);
                cleardPlayer = null;
				isCleard = false;

                if (needTown) {
                    livingTown.questCleared(this);
                 }
            }
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
            return id;
        }

        public string getName() {
            return NAME;
        }

        public long getUniqueId() {
            return UNIQUE_ID;
        }

        public void talk(IFriendly friendly) {
            if(isActivated){
                if (friendly is Player) {
                    var player = (Player)friendly;

                    if (quest != null) {
                        if (quest.isCleared()) {
                            clearedTalk(player);
                        } else {
                            undertakingTalk();
                        }
                    } else {
                        requestTalk(player);
                    }
                }
            }
        }

        private IQuest creatQuest(FlagList flags){
            this.quest = QuestHelper.getTypeQuest(QUEST_TYPE, flags, this);
            return quest;
        }

        private void requestTalk(Player player){
            TalkManager.getInstance().talk(MASSAGES);
            var createdQuest = creatQuest(player.getFlagList());
            player.undertake(createdQuest);
        }

        private void undertakingTalk(){
            TalkManager.getInstance().talk(UNDERTOOK_MASSAGES);
        }

        private void clearedTalk(Player player){
            TalkManager.getInstance().talk(CLEAR_MASSAGES);
            cleardPlayer = player;
            isCleard = true;
        }

        public void levelup(){
            this.level++;
            this.abilities[SPC] += Random.Range(0, 2);
            this.abilities[DEX] += Random.Range(1, 3);
        }
    }
}