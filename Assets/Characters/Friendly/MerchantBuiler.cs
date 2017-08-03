using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Character;
using Item;

using ItemType = Item.ItemParameters.ItemType;

namespace MasterData {
    public class MerchantBuiler : MonoBehaviour {
        //各フィールド
        private readonly int 
            Id,
            StartTradeIndex,
	        Spc,
			Dex,
			GoodsLevel,
			NumberOfGoods;

        private readonly string
            Name,
            ModelId,
            FailMassage;

        private List<string> Massage = new List<string>();

        private ItemType GoodsType;

        /// <summary>
        /// コンストラクタ
        /// マスターデータを登録します
        /// </summary>
        /// <param name="datas">csvによるstring配列データ</param>
        public MerchantBuiler(string[] datas) {
            Id = int.Parse(datas[0]);
			Name = datas[1];
			Dex = int.Parse(datas[2]);
			Spc = int.Parse(datas[3]);
			GoodsLevel = int.Parse(datas[4]);
			NumberOfGoods = int.Parse(datas[5]);
			GoodsType = (ItemType)System.Enum.Parse(typeof(ItemType), datas[6]);
            ModelId = datas[7];
            FailMassage = datas[8];
            for (int i = 9; datas[i] != "end"; i++) {
                if (datas[i] == "trade") {
                    StartTradeIndex = i - 10;
                }else{
                    Massage.Add(datas[i]);
                }
            }
        }

        //各フィールドのゲッター

        public string getName() {
            return this.Name;
        }

        public int getId() {
            return this.Id;
        }

        public List<string> getMassges() {
            return this.Massage;
        }

        public string getModelId() {
            return this.ModelId;
        }

        public Merchant build() {
            return new Merchant(this);
        }

        public int getStartTradeIndex(){
            return StartTradeIndex;
        }

        public int getSpc(){
            return Spc;
        }

        public int getDex(){
            return Dex;
        }

        public string getFailMassage(){
            return FailMassage;
        }

        public int getGoodsLevel(){
            return GoodsLevel;
        }

        public int getNumberOfGoods(){
            return NumberOfGoods;
        }

        public ItemType getGoodsType(){
            return GoodsType;
        }
    }
}
