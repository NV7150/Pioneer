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
            flavorText;

		[SerializeField]
		private float
			delayBonus;

		/// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="datas">csvによるstring配列データ</param>
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

		public string getFlavorText() {
			return flavorText;
		}

		/// <summary>
        /// string配列から初期化します
        /// </summary>
        /// <param name="datas">データを表すstring配列</param>
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
			this.flavorText = datas [9];
		}

		/// <summary>
        /// 防具を生成します
        /// </summary>
        /// <returns> 生成した防具 </returns>
		public Armor build(){
			return new Armor (this);
		}

		public override string ToString () {
			return "AromorBuilder " + name;
		}
	}
}

