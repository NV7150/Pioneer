using System;
using UnityEngine;

using Character;
using BattleSystem;

using MasterData;

using BattleAbility = Parameter.CharacterParameters.BattleAbility;

namespace Item{
	public class  Wepon :  IItem{
		private readonly int
            ID,
			/// <summary> 武器の攻撃力 </summary>
			ATTACK,
			/// <summary> この武器の射程 </summary>
			RANGE,
			/// <summary> 装備するのに必要な能力値　この能力値はweponAbitliyで設定します </summary>
            NEED_ABILITY,
			/// <summary> アイテムの基本価格 </summary>
			ITEM_VALUE,
			/// <summary> アイテムの重さ </summary>
			MASS;

		private readonly string 
			/// <summary> アイテム名 </summary>
            NAME,
			/// <summary> アイテムの説明 </summary>
            DESCRIPTION,
			/// <summary> アイテムのフレーバーテキスト </summary>
            FLAVOR_TEXT;

		/// <summary> 武器のディレイ値 </summary>
        private readonly float DELAY;

		/// <summary> 武器の種別 </summary>
        private readonly WeponType TYPE;

        /// <summary> 武器が使用するBattleAbility </summary>
        private readonly BattleAbility WEPON_ABILITY;

        /// <summary> 武器がストックできるかを表すフラグ </summary>
        private readonly bool CAN_STORE;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="builder">ビルダー</param>
		public Wepon(WeponBuilder builder){
            ID = builder.getId();
			ATTACK = builder.getAttack ();
			RANGE = builder.getRange ();
			NEED_ABILITY = builder.getNeedMft ();
			ITEM_VALUE = builder.getItemValue ();
			MASS = builder.getMass ();
			NAME = builder.getName ();
			DESCRIPTION = builder.getDescription ();
			FLAVOR_TEXT = builder.getFlavorText ();
			TYPE = builder.getWeponType ();
			DELAY = builder.getDelay ();
            WEPON_ABILITY = builder.getWeponAbility();
            CAN_STORE = builder.getCanStore();
		}

		/// <summary>
        /// 攻撃力を取得します
        /// </summary>
        /// <returns>攻撃力</returns>
		public int getAttack() {
			return ATTACK;
		}

		/// <summary>
        /// 射程を取得します
        /// </summary>
        /// <returns>射程</returns>
		public int getRange() {
			return RANGE;
		}

		/// <summary>
        /// 装備に必要な能力値を取得します
        /// </summary>
        /// <returns>必要な能力値</returns>
        public int getNeedAbility() {
			return NEED_ABILITY;
		}

		/// <summary>
		/// 武器の種別を取得します
		/// </summary>
		/// <returns>武器の種別</returns>
		public WeponType getWeponType() {
			return TYPE;
		}

		/// <summary>
		/// 武器が装備可能かを判定します
		/// </summary>
		/// <returns><c>true</c>, 装備可能 , <c>false</c> 装備不可能 </returns>
		/// <param name="user">装備したいキャラクター</param>
		public bool canEquip(IPlayable user) {
            return (NEED_ABILITY <= user.getRawAbility(WEPON_ABILITY));
		}

		/// <summary>
		/// 武器を使用するのに使うBattleAbilityを取得します
		/// </summary>
		/// <returns>使用するBattleAbility</returns>
		public BattleAbility getWeponAbility() {
			return WEPON_ABILITY;
		}

		/// <summary>
		/// ディレイ値を取得します
		/// </summary>
		/// <returns>The delay.</returns>
		public float getDelay() {
			return DELAY;
		}

		#region IItem implementation

        public int getId(){
            return ID;
        }

		public int getItemValue() {
			return ITEM_VALUE;
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
			user.equipWepon(this);
		}

        public bool getCanStore() {
            return CAN_STORE;
        }

        public string getFlavorText() {
            return FLAVOR_TEXT;
        }

		public bool getCanStack() {
			return false;
		}
        #endregion

        public override bool Equals(object obj) {
            if(!(obj is Wepon)){
                return false;
            }

            Wepon item = (Wepon)obj;

            return (item.getId() == this.getId());
        }


    }
}