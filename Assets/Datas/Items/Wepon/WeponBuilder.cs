using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using Item;

namespace MasterData{
	[System.SerializableAttribute]
	public class WeponBuilder{
		//プロパティです

		[SerializeField]
		private int
		id,
		attack,
		range,
		needMft,
		itemValue,
		mass,
		weponTypeID,
		delay;

		[SerializeField]
		private string 
		name,
		description,
		equipDescription;

		public WeponBuilder(string[] datas){
			setFromCSV (datas);
		}

		//各プロパティのgetterです

		public int getId(){
			return id;
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
		public string getEquipDescription(){
			return equipDescription;
		}

		public int getDelay() {
			return delay;
		}

		public WeponType getWeponType(){
			return WeponTypeHelper.getTypeFromId (weponTypeID);
		}

		//csvから初期化します
		private void setFromCSV(string[] datas){
			this.id = int.Parse (datas[0]);
			this.name = datas [1];
			this.attack = int.Parse (datas[2]);
			this.range = int.Parse (datas[3]);
			this.needMft = int.Parse (datas [4]);
			this.itemValue = int.Parse (datas[5]);
			this.mass = int.Parse (datas[6]);
			this.weponTypeID = int.Parse (datas[7]);
			this.description = datas [8];
			this.equipDescription = datas [9];
			this.delay = int.Parse (datas[10]);
		}

		//Weponを取得します
		public Wepon build(){
			return new Wepon(this);
		}

		public override string ToString(){
			return "wepon " + name;
		}
	}
}