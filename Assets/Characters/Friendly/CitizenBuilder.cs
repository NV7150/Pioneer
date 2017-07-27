using System;
using System.Collections.Generic;

using Character;

namespace MasterData {
    public class CitizenBuilder {
        //各フィールド
        private int id;

        private string
        name,
        modelId;

        private List<string> massage = new List<string>();

        /// <summary>
        /// コンストラクタ
        /// マスターデータを登録します
        /// </summary>
        /// <param name="datas">csvによるstring配列データ</param>
        public CitizenBuilder(string[] datas) {
            id = int.Parse(datas[0]);
            name = datas[1];
			modelId = datas[2];
            for (int i = 3; datas[i] != "end";i++){
                massage.Add(datas[i]);
            }
        }

        //各フィールドのゲッター

        public string getName(){
            return this.name;
        }

        public int getId(){
            return this.id;
        }

        public List<string> getMassges(){
            return this.massage;
        }

        public string getModelId(){
            return this.modelId;
        }

        public Citizen build(){
            return new Citizen(this);
        }
    }
}
