using System;
using UnityEngine;

using Character;
using BattleSystem;

using MasterData;

using Ability = Parameter.CharacterParameters.Ability;

namespace Item{
	public class  Wepon :  IItem{
		private readonly int
			//この武器の攻撃力です
			ATTACK,
			//この武器の射程です
			RANGE,
			//この武器を装備するのに必要なmftです
			NEED_MFT,
			//このアイテムの価格です
			ITEM_VALUE,
			//このアイテムの重さです
			MASS,
			//このアイテムのディレイ値です
			DELAY;

		private readonly string 
			//このアイテムの名前です
			name,
			//このアイテムの説明です
			description,
			//このアイテムの装備条件の説明です
			equipdescription;


		//この武器の種別です
		private WeponType type;

		private Ability ability;

		public Wepon(WeponBuilder builder){
			ATTACK = builder.getAttack ();
			RANGE = builder.getRange ();
			NEED_MFT = builder.getNeedMft ();
			ITEM_VALUE = builder.getItemValue ();
			MASS = builder.getMass ();
			name = builder.getName ();
			description = builder.getDescription ();
			equipdescription = builder.getEquipDescription ();
			type = builder.getWeponType ();
		}

		//攻撃力を取得します
		public int getAttack() {
			return ATTACK;
		}

		//射程を取得します
		public int getRange() {
			return RANGE;
		}

		//必要なmftを取得します
		public int getNeedMft() {
			return NEED_MFT;
		}

		//価格を取得します
		public int getItemValue() {
			return ITEM_VALUE;
		}

		//重さを取得します
		public int getMass(){
			return MASS;
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
			return DELAY;
		}

		//武器の種別を取得します
		public WeponType getWeponType(){
			return type;
		}

		//武器が装備可能かを確認します
		public bool canEquip(IPlayable user){
			return (NEED_MFT <= user.getMft());
		}

		//装備条件の説明を返します
		public string getEquipDescription(){
			return equipdescription;
		}

		public Ability getWeponAbility(){
			return ability;
		}

		public void use (IPlayable use) {
			throw new NotImplementedException ();
		}
	}
}