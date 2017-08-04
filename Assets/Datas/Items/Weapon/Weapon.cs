using System;
using UnityEngine;

using Character;
using BattleSystem;

using MasterData;

using BattleAbility = Parameter.CharacterParameters.BattleAbility;
using ItemType = Item.ItemParameters.ItemType;
using ItemAttribute = Item.ItemParameters.ItemAttribute;
using static Item.ItemParameters.ItemType;

namespace Item {
    public class Weapon : IItem {
        private readonly int
            CLASSIFICATION_CODE,
            /// <summary> 武器の基礎_攻撃力 </summary>
            BASE_ATTACK,
            /// <summary> この武器の射程 </summary>
            RANGE,
            /// <summary> アイテムの基本価格 </summary>
            BASE_VALUE,
            /// <summary> アイテムの重さ </summary>
            MASS,
            BASE_HIT;

        private readonly string
            /// <summary> アイテム名 </summary>
            NAME,
            /// <summary> アイテムの説明 </summary>
            DESCRIPTION,
            /// <summary> アイテムのフレーバーテキスト </summary>
            FLAVOR_TEXT;

        /// <summary> 武器のディレイ値 </summary>
        private readonly float 
	        BASE_DELAY,
	        CONSUMABILITY;

        /// <summary> 武器の種別 </summary>
        private readonly WeaponType TYPE;

        /// <summary> 武器が使用するBattleAbility </summary>
        private readonly BattleAbility WEAPON_ABILITY;

        private float quality;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Weapon(WeaponShape shape, ItemMaterial material,float quality){
            CLASSIFICATION_CODE = shape.getId() + material.getId();
            BASE_ATTACK = shape.getAttack();
            RANGE = shape.getRange();
            BASE_VALUE = material.getItemValue();
            MASS = shape.getMass() + material.getMass();
            BASE_HIT = shape.getHit();
            NAME = material.getName() + "の" + shape.getName();
            BASE_DELAY = shape.getDelay();
            TYPE = shape.getWeaponType();
            CONSUMABILITY = material.getConsumability();
            this.quality = quality;
            DESCRIPTION = material.getAdditionalDescription() + shape.getAdditionalDescription();
            FLAVOR_TEXT = material.getAdditionalFlavor() + shape.getAdditionalFlavor();
            WEAPON_ABILITY = WeaponTypeHelper.getTypeAbility(TYPE);
		}

		/// <summary>
        /// 攻撃力を取得します
        /// </summary>
        /// <returns>攻撃力</returns>
		public int getAttack() {
            int attack = (int)((float)BASE_ATTACK * (quality / 100f));
            return attack;
		}

        public int attackWith(){
            quality -= CONSUMABILITY;
            quality = (quality > 0) ? quality : 0;
            return this.getAttack();
        }

		/// <summary>
        /// 射程を取得します
        /// </summary>
        /// <returns>射程</returns>
		public int getRange() {
			return RANGE;
		}

		/// <summary>
		/// 武器の種別を取得します
		/// </summary>
		/// <returns>武器の種別</returns>
        public WeaponType getWeaponType() {
			return TYPE;
		}

		/// <summary>
		/// 武器を使用するのに使うBattleAbilityを取得します
		/// </summary>
		/// <returns>使用するBattleAbility</returns>
		public BattleAbility getWeaponAbility() {
			return WEAPON_ABILITY;
		}

		/// <summary>
		/// ディレイ値を取得します
		/// </summary>
		/// <returns>The delay.</returns>
		public float getDelay() {
            float delay = BASE_DELAY + (0.3f * (quality * 0.003f));
            delay = (delay >= 0.5f) ? delay : 0.5f;
            return delay;
		}

        public float getQuality(){
            return quality;
        }

		#region IItem implementation

        public int getId(){
            return CLASSIFICATION_CODE;
        }

		public int getItemValue() {
            int itemValue = (int)((float)BASE_VALUE * (quality / 100));
            return itemValue;
		}

		public int getMass(){
			return MASS;
		}

		public string getName() {
			return NAME;
		}

        public string getDescription() {
            return DESCRIPTION;
        }

		public void use(IPlayable user) {
			user.equipWeapon(this);
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
            return WEAPON;
		}

		public ItemAttribute getItemAttribute() {
            return ItemAttribute.WEAPON;
		}
        #endregion

        public override bool Equals(object obj) {
            if(!(obj is Weapon)){
                return false;
            }
            Weapon item = (Weapon)obj;
            //種別IDが同じ（同じ素材でできている）で、品質値が同じなら等価
            return item.getId() == this.getId() && item.getQuality() == this.getQuality();
        }


    }
}