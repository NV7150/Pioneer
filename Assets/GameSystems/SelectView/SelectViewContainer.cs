using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SelectView{
    public class SelectViewContainer : MonoBehaviour {
        /// <summary> スクロールビューのコンテント </summary>
        public GameObject content;
        /// <summary>
        /// セレクトビューが生成されたかを表すフラグ
        /// trueだと新たにセレクトビューが生成されない
        /// </summary>
        public bool viewCreated = false;

        public ScrollRect viewRect;

        /// <summary>
        /// セレクトビュー新たに生成します
        /// </summary>
        /// <returns>生成したセレクトビュー</returns>
        /// <param name="datas">追加するノードオブジェクトのリスト</param>
        /// <typeparam name="Node">ノードオブジェクト</typeparam>
        /// <typeparam name="Element">ノードの中身</typeparam>
        public SelectView<Node, Element> creatSelectView<Node, Element>(List<Node> datas)
            where Node : MonoBehaviour, INode<Element> {
            if (viewCreated)
                throw new System.InvalidOperationException("view is created but called creatSelectView again");
            viewCreated = true;
            return new SelectView<Node, Element>(datas, content, this,viewRect);
        }

        /// <summary>
        /// 子ノードを削除します
        /// </summary>
        public void detach() {
            foreach (Transform child in this.content.transform) {
                Destroy(child.gameObject);
            }
            viewCreated = false;
        }
    }
}
