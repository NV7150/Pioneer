using System;
using UnityEngine;

using Character;
using MasterData;

using BattleAbility = Parameter.CharacterParameters.BattleAbility;

namespace Item {
	[System.SerializableAttribute]
	public class Armor : IItem{
		private readonly int
			/// <summary> この防具のID </summary>
			ID,
			/// <summary> 防具の防御力への修正値 </summary>
			DEF,
			/// <summary> 防具の回避への修正値 </summary>
			DODGE,
			/// <summary> 装備に必要とするphy </summary>
			NEED_PHY,
			/// <summary> アイテムの重さ </summary>
			MASS,
			/// <summary> アイテムの基本価格 </summary>
			ITEM_VALUE;

		private string
			/// <summary> アイテム名 </summary>
			NAME,
			/// <summary> アイテムの説明 </summary>
			DESCRIPTION,
			/// <summary> アイテムのフレーバーテキスト </summary>
            FLAVOR_TEXT;

        /// <summary> 防具のディレイへの修正値 </summary>
		private float DELAY_BONUS;

        /// <summary> アイテムとしてインベントリに格納できるか </summary>
        private bool canStore;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="builder">ビルダー</param>
		public Armor(ArmorBuilder builder){
			this.ID = builder.getId ();
			this.DEF = builder.getDef ();
			this.DODGE = builder.getDodge ();
			this.NEED_PHY = builder.getNeedPhy ();
			this.NAME = builder.getName ();
			this.DESCRIPTION = builder.getDescription ();
			this.FLAVOR_TEXT = builder.getFlavorText ();
			this.DELAY_BONUS = builder.getDelayBonus ();
			this.MASS = builder.getMass ();
			this.ITEM_VALUE = builder.getItemValue ();
		}

		/// <summary>
        /// 防御値を取得します
        /// </summary>
        /// <returns>防御値</returns>
		public int getDef(){
			return DEF;
		}

		/// <summary>
        /// 回避への修正値を取得します
        /// </summary>
        /// <returns>修正値</returns>
		public int getDodge(){
			return DODGE;
		}

		/// <summary>
        /// 装備可能かを判定します
        /// </summary>
        /// <returns><c>true</c>, 装備可能, <c>false</c> 装備不可能</returns>
        /// <param name="user">装備したいキャラウター</param>
		public bool canEquip(IPlayable user){
            return (user.getRawAbility (BattleAbility.PHY) >= NEED_PHY);
		}

		/// <summary>
        /// 装備条件を文章として取得します
        /// </summary>
        /// <returns>装備条件の文章</returns>
		public string getEquipDescription (){
			return FLAVOR_TEXT;
		}

		/// <summary>
		/// 防具のディレイ修正を取得します
		/// </summary>
		/// <returns>修正値</returns>
		public float getDelayBonus() {
			return DELAY_BONUS;
		}

        /// <summary>
        /// 防具のIDを取得します
        /// </summary>
        /// <returns>The identifier.</returns>
		public int getId() {
			return ID;
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
            return canStore;
        }

        #endregion
    }
}

