using System;
using UnityEngine;

using Item;

namespace MasterData {
	[System.SerializableAttribute]
	public class ArmorBuilder {
		//プロパティです
		[SerializeField]
		private int
			id,
			def,
			dodge,
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

		//csvによるstring配列から初期化します
		public ArmorBuilder (string[] datas) {
			setDataFromCSV (datas);
		}

		//各プロパティのgetterです

		public int getId(){
			return this.id;
		}

		public int getDef() {
			return def;
		}

		public int getDodge() {
			return dodge;
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

		//csvのstring配列から初期化します
		private void setDataFromCSV(string[] datas){
			this.id = int.Parse (datas [0]);
			this.name = datas [1];
			this.def = int.Parse (datas[2]);
			this.dodge = int.Parse (datas[3]);
			this.delayBonus = float.Parse (datas [4]);
			this.needPhy = int.Parse (datas[5]);
			this.itemValue = int.Parse (datas[6]);
			this.mass = int.Parse (datas[7]);
			this.description = datas [8];
			this.equipDescription = datas [9];
		}

		//Armorを取得します
		public Armor build(){
			return new Armor (this);
		}

		public override string ToString () {
			return "AromorBuilder " + name;
		}
	}
}

