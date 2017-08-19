using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SelectView;

namespace SelectView {
    public class SelectView<Node, Element> where Node : MonoBehaviour, INode<Element> {
        /// <summary> スクロールビューのコンテントオブジェクト </summary>
        private GameObject content;
        /// <summary> アクティブなカーソル </summary>
        private Cursor<Node> cursor;
        /// <summary> 格納されてるSelectViewContainer </summary>
        private SelectViewContainer container;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="nodes">追加したいゲームオブジェクトのリスト</param>
        /// <param name="content">スクロールビューのコンテント</param>
        /// <param name="container">格納されているContainer</param>
        public SelectView(List<Node> nodes, GameObject content, SelectViewContainer container) {
            this.content = content;
            this.container = container;
            foreach (Node node in nodes) {
                node.transform.SetParent(content.transform);
            }

            GameObject cursorObject = MonoBehaviour.Instantiate((GameObject)Resources.Load("Prefabs/CursorContainer"));
            this.cursor = cursorObject.GetComponent<CursorContainer>().creatCursor<Node>();
            cursorObject.transform.SetParent(container.transform);


            cursor.setList(nodes);
        }

        /// <summary>
        /// ハイライトしているオブジェクトを取得します
        /// </summary>
        /// <returns>ハイライトしているオブジェクト</returns>
        public Element getElement(){
            return cursor.getNode().getElement();
        }

        /// <summary>
        /// 対象の位置にカーソルを移動させます
        /// </summary>
        /// <returns>移動先のオブジェクト</returns>
        /// <param name="i">移動させた位置</param>
        public Element moveTo(int i) {
            cursor.moveTo(i);
            return cursor.getNode().getElement();
        }

        /// <summary>
        /// ハイライト中のインデックスを取得します
        /// </summary>
        /// <returns>ハイライトしているインデックス</returns>
        public int getIndex() {
            return cursor.getSelectingIndex();
        }

        /// <summary>
        /// 終了処理を行います
        /// </summary>
        public void delete() {
            if (cursor != null) {
                cursor.delete();
                cursor = null;
            }
        }
    }
}
