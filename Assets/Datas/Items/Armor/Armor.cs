using System;
using UnityEngine;

using Character;
using MasterData;

namespace Item {
	[System.SerializableAttribute]
	public class Armor : IItem{
		private int 
			id,
			def,
			dodgeBonus,
			needPhy,
			mass,
			itemValue;

		private string
			name,
			descpription,
			equipDescription;

		private float delayBonus;

		public Armor(ArmorBuilder builder){
			this.id = builder.getId ();
			this.def = builder.getDef ();
			this.dodgeBonus = builder.getDodgeBonus ();
			this.needPhy = builder.getNeedPhy ();
			this.name = builder.getName ();
			this.descpription = builder.getDescription ();
			this.equipDescription = builder.getEquipDescription ();
			this.delayBonus = builder.getDelayBonus ();
		}

		//物理防御を返します
		public int getDef(){
			return def;
		}

		//回避修正を返します
		public int getDodgeBonus(){
			return dodgeBonus;
		}

		//武器が装備可能かを確認します
		public bool canEquip(IPlayable user){
			return (user.getPhy () >= needPhy);
		}

		//装備条件を文章として返します
		public string getEquipDescription (){
			return equipDescription;
		}

		//アイテム名を取得します
		public string getName(){
			return name;
		}

		//アイテム説明を取得します
		public string getDescription(){
			return descpription;
		}

		//アイテムの重量を表します
		public int getMass(){
			return mass;
		}

		//アイテムの基本価格を表します
		public int getItemValue(){
			return itemValue;
		}

		//アイテムのidを返します
		public int getId(){
			return id;
		}

		public void use (IPlayable use) {
			throw new NotImplementedException ();
		}
	}
}

