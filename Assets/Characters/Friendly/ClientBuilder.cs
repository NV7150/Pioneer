using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Character;

using QuestType = Quest.QuestParameters.QuestType;

namespace MasterData {
    public class ClientBuilder {
        //各フィールド
        private int
        id,
        level,
        spc,
        dex;

        private string
        name,
        modelId;

        private QuestType questType;

        private List<string> massage = new List<string>();
        private List<string> underTookMassage = new List<string>();
        private List<string> clearedMassage = new List<string>();

        /// <summary>
        /// コンストラクタ
        /// マスターデータを登録します
        /// </summary>
        /// <param name="datas">csvによるstring配列データ</param>
        public ClientBuilder(string[] datas) {
            id = int.Parse(datas[0]);
            name = datas[1];
            modelId = datas[2];
            spc = int.Parse(datas[3]);
            dex = int.Parse(datas[4]);
            level = int.Parse(datas[5]);
            questType = (QuestType)Enum.Parse(typeof(QuestType), datas[6]);
            int i = 7;
            for (; datas[i] != "end"; i++) {
                massage.Add(datas[i]);
            }
            i++;

            for (; datas[i] != "end";i++){
                underTookMassage.Add(datas[i]);
            }
            i++;

            for (; datas[i] != "end";i++){
                clearedMassage.Add(datas[i]);
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

		public List<string> getUnderTookMassges() {
            return this.underTookMassage;
		}

		public List<string> getClearedMassges() {
            return this.clearedMassage;
		}

        public string getModelId() {
            return this.modelId;
        }

        public QuestType getQuestType(){
            return questType;
        }

        public int getLevel(){
            return level;
        }

        public int getDex(){
            return dex;
        }

        public int getSpc(){
            return spc;
        }

        public Client build(){
            return new Client(this);
        }
    }
}