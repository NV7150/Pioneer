using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Character;
using MasterData;

namespace FieldMap {
    [System.SerializableAttribute]
    public class Building : MonoBehaviour {
        IFriendly ownerCharacter;

        [SerializeField]
        string buildingName;
        int level;

        public string getName() {
            return buildingName;
        }

        public int getLevel() {
            return level;
        }

        public IFriendly getOwnerCharacter() {
            return ownerCharacter;
        }

        public void setState(string name, IFriendly character, Vector3 pos ,int level = 0) {
			this.buildingName = name;
			this.level = level;

            transform.position = pos;

            ownerCharacter = character;
            ownerCharacter.getContainer().transform.position = transform.position;
        }
    }
}
