using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Item {
    public class WeponShape {
        private readonly int
            ID,
            ATTACK,
            HIT,
            VALUE,
            RANGE,
            CREAT_DIFFICULTY,
            MASS;

        private readonly float
            DELAY;

        private readonly string
            NAME,
            DESRIPTION,
            FLAVOR_TEXT,
	        ADDITIONAL_DESCRIPTION,
	        ADDITIONAL_FLAVOR;

        private readonly WeponType TYPE;

        public WeponShape(string[]datas){
            ID = int.Parse(datas[0]);
            NAME = datas[1];
            ATTACK = int.Parse(datas[2]);
            HIT = int.Parse(datas[3]);
            VALUE = int.Parse(datas[4]);
            DELAY = float.Parse(datas[5]);
            RANGE = int.Parse(datas[6]);
            MASS = int.Parse(datas[7]);
            CREAT_DIFFICULTY = int.Parse(datas[8]);
            TYPE = (WeponType)System.Enum.Parse(typeof(WeponType), datas[9]);
            DESRIPTION = datas[10];
            FLAVOR_TEXT = datas[11];
            ADDITIONAL_DESCRIPTION = datas[12];
            ADDITIONAL_FLAVOR = datas[13];
        }

		public int getId() {
			return ID;
		}

		public int getAttack() {
			return ATTACK;
		}

		public int getHit() {
			return HIT;
		}

		public int getValue() {
			return VALUE;
		}

		public int getCreatDifficulty() {
			return CREAT_DIFFICULTY;
		}

		public float getDelay() {
			return DELAY;
		}

		public string getName() {
			return NAME;
		}

		public string getDescription() {
			return DESRIPTION;
		}

        public string getFlavorText() {
			return FLAVOR_TEXT;
		}

        public int getRange(){
            return RANGE;
        }

        public int getMass() {
            return MASS;
        }

        public WeponType getWeponType(){
            return TYPE;
        }

		public string getAdditionalDescription() {
			return ADDITIONAL_DESCRIPTION;
		}

		public string getAdditionalFlavor() {
			return ADDITIONAL_FLAVOR;
		}
    }
}
