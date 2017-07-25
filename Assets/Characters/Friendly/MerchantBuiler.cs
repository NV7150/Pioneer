using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Character;
using Item;

namespace MasterData {
    public class MerchantBuiler : MonoBehaviour {
        //各フィールド
        private int 
            id,
            startTradeIndex,
	        spc,
	        dex,
            goodsId;

        private string
            name,
            modelId,
            failMassage;

        private List<string> massage = new List<string>();

        private Goods goods;

        /// <summary>
        /// コンストラクタ
        /// マスターデータを登録します
        /// </summary>
        /// <param name="datas">csvによるstring配列データ</param>
        public MerchantBuiler(string[] datas) {
            id = int.Parse(datas[0]);
            name = datas[1];
            modelId = datas[2];
            goodsId = int.Parse(datas[3]);
            spc = int.Parse(datas[4]);
            dex = int.Parse(datas[5]);
            failMassage = datas[6];
            for (int i = 7; datas[i] != "end"; i++) {
                if (datas[i] == "trade") {
                    startTradeIndex = i - 8;
                }else{
                    massage.Add(datas[i]);
                }
            }
        }

        //各フィールドのゲッター

        public string getName() {
            return this.name;
        }

        public int getId() {
            return this.id;
        }

        public List<string> getMassges() {
            return this.massage;
        }

        public string getModelId() {
            return this.modelId;
        }

        public Merchant build() {
            return new Merchant(this);
        }

        public Goods getGoods(){
            return GoodsMasterManager.getGoodsFromId(goodsId);
        }

        public int getStartTradeIndex(){
            return startTradeIndex;
        }

        public int getSpc(){
            return spc;
        }

        public int getDex(){
            return dex;
        }

        public string getFailMassage(){
            return failMassage;
        }
    }
}
