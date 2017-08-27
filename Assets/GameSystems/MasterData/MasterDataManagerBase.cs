using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace MasterData{
	public abstract class MasterDataManagerBase{
		/// <summary>
        /// 与えられたstring二次配列から指定されたindexにあるデータを抜き出します
        /// </summary>
        /// <returns>使用するstirng配列</returns>
        /// <param name="csv">csvによって生成されたstring二次配列</param>
        /// <param name="row">行数</param>
		protected string[] GetRaw (string[,] csv, int row) {
			string[] data = new string[ csv.GetLength(0) ];
            for (int i = 0; i < csv.GetLength(0); i++) {
                data[i] = csv[i, row];
            }
			return data;
		}

		/// <summary>
        /// 具象クラスがAwake時に呼び出す処理
        /// </summary>
        /// <param name="csvAsset">csvのTextAsset</param>
		protected void constractedBehaviour(TextAsset csvAsset){
			var datas = CSVReader.SplitCsvGrid(csvAsset.text);
			for (int i = 1; i < datas.GetLength(1) - 1 ; i++) {
				addInstance (GetRaw(datas,i));
			}
		}

        /// <summary>
        /// 指定されたセーブデータファイルのデータ
        /// </summary>
        /// <returns>セーブデータのインスタンス</returns>
        /// <param name="id">取得したいもののID</param>
        /// <param name="fileName">記録ファイルのアドレス</param>
        /// <typeparam name="E">セーブデータのクラスの型</typeparam>
        public static E loadSaveData<E>(int id ,string fileName){
            ES2Reader reader = ES2Reader.Create(getLoadPass(id,fileName));
            return reader.Read<E>("" + id);
		}

        public static string getLoadPass(int id, string fileName){
            return getLoadPassExceptTag(fileName) + "?tag=" + id;
        }

        public static string getLoadPassExceptTag(string fileName){
            return "Progresses/" + fileName;
        }

		/// <summary>
        /// 与えられたstring配列データからインスタンスを生成し、登録します
        /// </summary>
        /// <param name="datas">Datas.</param>
		protected abstract void addInstance (string[] datas);
	}
}