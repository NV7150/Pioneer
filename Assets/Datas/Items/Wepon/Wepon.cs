using System;
using UnityEngine;

using character;
using battleSystem;

using masterData;

namespace item{
	public class  Wepon :  IItem{
		private int
			attack,
			range,
			needMft,
			itemValue,
			mass;

		private string 
			name,
			description,
			equipdescription;

		private float delay;

		private WeponType type;

		public Wepon(WeponBuilder builder){
			attack = builder.getAttack ();
			range = builder.getRange ();
			needMft = builder.getNeedMft ();
			itemValue = builder.getItemValue ();
			mass = builder.getMass ();
			name = builder.getName ();
			description = builder.getDescription ();
			equipdescription = builder.getEquipDescription ();
			type = builder.getWeponType ();
		}

		public int getAttack() {
			return attack;
		}

		public int getRange() {
			return range;
		}

		public int getNeedMft() {
			return needMft;
		}

		public int getItemValue() {
			return itemValue;
		}

		public int getMass(){
			return mass;
		}

		public string getName() {
			return name;
		}

		public string getDescription() {
			return description;
		}

		public float getDelay() {
			return delay;
		}

		//武器の種別を取得します
		public WeponType getWeponType(){
			return type;
		}

		//武器が装備可能かを確認します
		public bool canEquip(IPlayable user){
			return (needMft <= user.getMft());
		}

		//装備条件の説明を返します
		public string getEquipDescription(){
			return equipdescription;
		}


		public void use (IPlayable use) {
			throw new NotImplementedException ();
		}
	}
}