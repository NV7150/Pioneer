using System;
using UnityEngine;

using Character;
using BattleSystem;

using MasterData;

namespace Item{
	public class  Wepon :  IItem{
		private int
			//この武器の攻撃力です
			attack,
			//この武器の射程です
			range,
			//この武器を装備するのに必要なmftです
			needMft,
			//このアイテムの価格です
			itemValue,
			//このアイテムの重さです
			mass;

		private string 
			//このアイテムの名前です
			name,
			//このアイテムの説明です
			description,
			//このアイテムの装備条件の説明です
			equipdescription;

		//この武器のディレイ値です
		private int delay;

		//この武器の種別です
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

		//攻撃力を取得します
		public int getAttack() {
			return attack;
		}

		//射程を取得します
		public int getRange() {
			return range;
		}

		//必要なmftを取得します
		public int getNeedMft() {
			return needMft;
		}

		//価格を取得します
		public int getItemValue() {
			return itemValue;
		}

		//重さを取得します
		public int getMass(){
			return mass;
		}

		//名前を取得します
		public string getName() {
			return name;
		}

		//説明文を取得します
		public string getDescription() {
			return description;
		}

		//ディレイ値を取得します
		public int getDelay() {
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