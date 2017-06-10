using System;
using UnityEngine;

using item;

namespace masterdata {
	[System.SerializableAttribute]
	public class ArmorBuilder {
		[SerializeField]
		private int
			id,
			def,
			dodgeBonus,
			needPhy,
			itemValue,
			mass;

		[SerializeField]
		private string
			name,
			description,
			equipDescription;

		[SerializeField]
		private float
			delayBonus;

		public ArmorBuilder (string[] datas) {
			setDataFromCSV (datas);
		}

		public int getId(){
			return this.id;
		}

		public int getDef() {
			return def;
		}

		public int getDodgeBonus() {
			return dodgeBonus;
		}

		public float getDelayBonus(){
			return delayBonus;
		}

		public int getNeedPhy() {
			return needPhy;
		}

		public int getItemValue() {
			return itemValue;
		}

		public int getMass() {
			return mass;
		}

		public string getName() {
			return name;
		}

		public string getDescription() {
			return description;
		}

		public string getEquipDescription() {
			return equipDescription;
		}

		private void setDataFromCSV(string[] datas){
			this.id = int.Parse (datas [0]);
			this.name = datas [1];
			this.def = int.Parse (datas[2]);
			this.dodgeBonus = int.Parse (datas[3]);
			this.delayBonus = float.Parse (datas [4]);
			this.needPhy = int.Parse (datas[5]);
			this.itemValue = int.Parse (datas[6]);
			this.mass = int.Parse (datas[7]);
			this.description = datas [8];
			this.equipDescription = datas [9];
		}

		public Armor build(){
			return new Armor (this);
		}
	}
}

