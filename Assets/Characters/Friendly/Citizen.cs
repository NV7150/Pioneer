using System;using System.Collections;using System.Collections.Generic;using Parameter;using UnityEngine;using MasterData;using TalkSystem;using FriendlyAbility = Parameter.CharacterParameters.FriendlyAbility;using FriendlyCharacterType = Parameter.CharacterParameters.FriendlyCharacterType;namespace Character {    public class Citizen : IFriendly {        private int id;        private readonly long UNIQUE_ID;        private readonly string NAME;        private readonly List<string> MASSAGES;        private Container container;        public int Id{            get { return this.id; }        }        public Transform ContainerTransfrom{            get { return container.transform; }        }        private Dictionary<FriendlyAbility, int> abilities = new Dictionary<FriendlyAbility, int>(){
            {FriendlyAbility.DEX,0},            {FriendlyAbility.SPC,0}
        };        public Citizen(CitizenBuilder builder){            this.id = builder.getId();            this.NAME = builder.getName();            this.MASSAGES = builder.getMassges();            this.UNIQUE_ID = UniqueIdCreator.creatUniqueId();            var modelPrefab = (GameObject)Resources.Load("Models/" + builder.getModelId());            container = MonoBehaviour.Instantiate(modelPrefab).GetComponent<Container>();            container.setCharacter(this);        }        public Citizen(int id, Transform transform){            this.id = id;
			var builder = CitizenMasterManager.getCitizenBuilderFromId(id);
			this.NAME = builder.getName();
			this.MASSAGES = builder.getMassges();
			this.UNIQUE_ID = UniqueIdCreator.creatUniqueId();

			var modelPrefab = (GameObject)Resources.Load("Models/" + builder.getModelId());
            container = MonoBehaviour.Instantiate(modelPrefab,transform.position,transform.rotation).GetComponent<Container>();
			container.setCharacter(this);            MonoBehaviour.Destroy(transform.gameObject);        }        public int getId(){            return id;        }        public void act() {            //実装まだです        }        public void death() {            MonoBehaviour.Destroy(container.getModel());        }        public Container getContainer() {            return container;        }        public string getName() {            return NAME;        }        public int getFriendlyAbility(FriendlyAbility ability) {            return abilities[ability];        }        public long getUniqueId() {            return UNIQUE_ID;        }        public void talk(IFriendly friendly) {            TalkManager.getInstance().talk(MASSAGES);        }

        public FriendlyCharacterType getCharacterType() {            return FriendlyCharacterType.CITIZEN;
        }
    }}