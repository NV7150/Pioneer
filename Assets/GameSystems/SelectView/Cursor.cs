using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SelectView {
    /// <summary>
    /// セレクトビューのカーソルを表します
    /// </summary>
    public class Cursor<Node> where Node : MonoBehaviour {
        /// <summary> カソールさせる対象のリスト </summary>
        List<Node> datas = new List<Node>();
        /// <summary> ハイライトしているdatasのインデックス </summary>
        int selecting = 0;
        int count = 0;

        /// <summary> カーソルのゲームオブジェクト </summary>
        CursorContainer container;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="container">カーソルのゲームオブジェクト</param>
        public Cursor(CursorContainer container){
            this.container = container;
        }

        /// <summary>
        /// データを設定します
        /// </summary>
        /// <param name="datas">データのリスト</param>
        public void setList(List<Node> datas) {
			this.datas = datas;
			Canvas.ForceUpdateCanvases();
            this.count = datas.Count;
            moveTo(selecting);
        }

        /// <summary>
        /// 対象のインデックスに移動します
        /// </summary>
        /// <param name="i">インデックス</param>
        public void moveTo(int i) {
            if (i < datas.Count && i >= 0 ) {
                selecting = i;
			}
			Vector3 addValue = new Vector3(-200, 0, 0);
			container.transform.position = datas[selecting].transform.position + addValue;
        }

        /// <summary>
        /// ハイライトしているインデックスを取得します
        /// </summary>
        /// <returns>ハイライトしているインデックス</returns>
        public int getSelectingIndex() {
            return selecting;
        }

        /// <summary>
        /// ハイライト中のノードを取得します
        /// </summary>
        /// <returns>ノード</returns>
        public Node getNode() {
            if (datas.Count > 0) {
                return datas[selecting];
            }else{
                return null;
            }
        }

        /// <summary>
        /// 終了処理を行います
        /// </summary>
		public void delete() {
            MonoBehaviour.Destroy(container.gameObject);
            container.cursolDeleted();
        }

        public float getElementNormalizedPos(int i){
            int index = count - i;
            float normalizedPos = (float)index / count;
            return (normalizedPos < 1) ? normalizedPos : 1;
        }
    }
}
