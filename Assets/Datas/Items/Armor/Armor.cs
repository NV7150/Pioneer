using System;
using UnityEngine;

using Character;
using MasterData;

namespace Item {
	[System.SerializableAttribute]
	public class Armor : IItem{
		private readonly int
			//この装備のIDです
			ID,
			//この装備が与える防御への影響です
			DEF,
			//この装備が与える回避への影響です
			DODGE,
			//この装備が必要とするphy能力値です
			NEED_PHY,
			//このアイテムの重さです
			MASS,
			//このアイテムの価格です
			ITEM_VALUE;

		private string
			//このアイテムの名前です
			NAME,
			//このアイテムの説明です
			DESCRIPTION,
			//このアイテム装備条件の説明です
			EQUIP_DESCRIPTION;

		private float DELAY_BONUS;

		public Armor(ArmorBuilder builder){
			this.ID = builder.getId ();
			this.DEF = builder.getDef ();
			this.DODGE = builder.getDodge ();
			this.NEED_PHY = builder.getNeedPhy ();
			this.NAME = builder.getName ();
			this.DESCRIPTION = builder.getDescription ();
			this.EQUIP_DESCRIPTION = builder.getEquipDescription ();
			this.DELAY_BONUS = builder.getDelayBonus ();
			this.MASS = builder.getMass ();
			this.ITEM_VALUE = builder.getItemValue ();
		}

		//物理防御を返します
		public int getDef(){
			return DEF;
		}

		//回避修正を返します
		public int getDodge(){
			return DODGE;
		}

		//武器が装備可能かを確認します
		public bool canEquip(IPlayable user){
			return (user.getPhy () >= NEED_PHY);
		}

		//装備条件を文章として返します
		public string getEquipDescription (){
			return EQUIP_DESCRIPTION;
		}

		//アイテム名を取得します
		public string getName(){
			return NAME;
		}

		//アイテム説明を取得します
		public string getDescription(){
			return DESCRIPTION;
		}

		//アイテムの重量を表します
		public int getMass(){
			return MASS;
		}

		//アイテムの基本価格を表します
		public int getItemValue(){
			return ITEM_VALUE;
		}

		//アイテムのidを返します
		public int getId(){
			return ID;
		}

		public float getDelayBonus(){
			return DELAY_BONUS;
		}

		//アイテムを使用します
		public void use (IPlayable user) {
			user.equipArmor (this);
		}
	}
}

