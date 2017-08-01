using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SelectView {
    /// <summary>
    /// カーソルのゲームオブジェクト
    /// </summary>
    public class CursorContainer : MonoBehaviour {
        /// <summary>
        /// カーソルを作ったかのフラグ
        /// true中は新たにカーソルを作れません
        /// </summary>
        private bool cursorCreated = false;

        /// <summary>
        /// カーソルを生成します
        /// </summary>
        /// <returns>生成したカーソル</returns>
        /// <typeparam name="Node">生成したカーソルを対応させるNode</typeparam>
        public Cursor<Node> creatCursor<Node>() where Node:MonoBehaviour{
            if (cursorCreated)
                throw new System.ArgumentException("cursor is created but it called again");
            cursorCreated = true;
            return new Cursor<Node>(this);
        }

        /// <summary>
        /// カーソルの終了処理を行います
        /// </summary>
        public void cursolDeleted(){
            cursorCreated = false;
        }
    }
}