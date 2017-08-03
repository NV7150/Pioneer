using System;
using UnityEngine;

using Character;
using MasterData;

using BattleAbility = Parameter.CharacterParameters.BattleAbility;
using static Item.ItemParameters.ItemType;
using ItemType = Item.ItemParameters.ItemType;

namespace Item {
	[System.SerializableAttribute]
	public class Armor : IItem{
        private readonly int
            CLASSIFICATION_CODE,
            /// <summary> 防具の防御力への修正値 </summary>
            BASE_DEF,
            /// <summary> アイテムの基本価格 </summary>
            ITEM_VALUE,
            HEAVINESS,
            MASS;

		private readonly string
			/// <summary> アイテム名 </summary>
			NAME,
			/// <summary> アイテムの説明 </summary>
			DESCRIPTION,
			/// <summary> アイテムのフレーバーテキスト </summary>
            FLAVOR_TEXT;

        /// <summary> 防具のディレイへの修正値 </summary>
        private readonly float
	        DELAY_DISTURB_MAG,
	        DODGE_DISTURB_MAG,
	        MAGIC_DISTURB_MAG,
            CONSUMABILITY;

        private float quality;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Armor(ArmorShape shape, ItemMaterial material, float quality){
            CLASSIFICATION_CODE = shape.getId() + material.getId();
            BASE_DEF = shape.getDef();
            ITEM_VALUE = shape.getItemValue();
            DELAY_DISTURB_MAG = shape.getDelayDisturbMag();
            DODGE_DISTURB_MAG = shape.getDodgeDisturbMag();
            MAGIC_DISTURB_MAG = shape.getMagicDisturbMag();
            this.quality = quality;
            CONSUMABILITY = material.getConsumability();
            NAME = material.getName() + "の" + shape.getName();
			DESCRIPTION = material.getAdditionalDescription() + shape.getAdditionalDescription();
			FLAVOR_TEXT = material.getAdditionalFlavor() + shape.getAdditionalFlavor();
            MASS = shape.getMass() + material.getMass();
            HEAVINESS = material.getHeaviness();
		}

		/// <summary>
        /// 防御値を取得します
        /// </summary>
        /// <returns>防御値</returns>
		public int getDef(){
            int def =(int)((float)BASE_DEF * (quality / 100));
            return def;
		}

        public int defenceWith(){
            quality -= CONSUMABILITY;
            return getDef();
        }

		/// <summary>
        /// 回避への修正値を取得します
        /// </summary>
        /// <returns>修正値</returns>
		public int getDodgeCorrection(){
            int dodgeCorrection = (int)( (DODGE_DISTURB_MAG / 80) * (20 * HEAVINESS + quality) );
            return dodgeCorrection;
		}

		/// <summary>
		/// 防具のディレイ修正を取得します
		/// </summary>
		/// <returns>修正値</returns>
		public float getDelayBonus() {
            float delayCorrection = (DELAY_DISTURB_MAG / 800) * (20 * HEAVINESS + quality);
            return delayCorrection;
		}

        /// <summary>
        /// 防具のIDを取得します
        /// </summary>
        /// <returns>The identifier.</returns>
		public int getId() {
            return CLASSIFICATION_CODE;
		}

        public float getQuality(){
            return quality;
        }

		#region IItem implementation

		public string getName(){
			return NAME;
		}

		public string getDescription(){
			return DESCRIPTION;
		}

		public int getMass(){
			return MASS;
		}

		public int getItemValue(){
			return ITEM_VALUE;
		}

        public void use(IPlayable user) {
			user.equipArmor(this);
		}

        public bool getCanStore() {
            return true;
        }

        public string getFlavorText() {
            return FLAVOR_TEXT;
        }

		public bool getCanStack() {
            return false;
		}

		public ItemType getItemType() {
            return ARMOR;
		}
		#endregion

        public override bool Equals(object obj) {
            //Armorであり、IDが同じなら等価
            if(!(obj is Armor)){
                return false;
            }

            Armor armor = (Armor)obj;

            return armor.getId() == this.getId() && armor.getQuality() == armor.getQuality();
        }


    }
}

