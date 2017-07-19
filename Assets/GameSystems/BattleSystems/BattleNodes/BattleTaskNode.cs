using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BattleSystem{
	public class BattleTaskNode : MonoBehaviour {
        /// <summary> 担当するタスク </summary>
		private BattleTask task;
        /// <summary> データを取得するBattleTaskListView </summary>
		private BattleTaskListView list;
        private PlayerBattleTaskManager manager;
        /// <summary> アタッチされるオブジェクトの子オブジェクトであるTextオブジェクト </summary>
		public Text name;
        /// <summary> アタッチされているオブジェクト </summary>
		public GameObject view;
        /// <summary> このタスクのID </summary>
        private long id;

        /// <summary>
        /// タスクを設定します
        /// </summary>
        /// <param name="task"> 設定するタスク </param>
		public void setTask(BattleTask task){
			this.task = task;
			name.text = task.getName ();
		}

		/// <summary>
		/// キャンセルボタンが押された時の処理です
		/// </summary>
		public void canselChosen() {
			manager.canseledTask(task);
            list.deleteTask(task);
		}

        /// <summary>
        /// このオブジェクトを削除します
        /// </summary>
		public void delete(){
            Destroy (view);
		}

        /// <summary>
        /// IDを設定します
        /// </summary>
        /// <param name="number"> 設定する数値 </param>
		public void setTaskId(long number){
			this.id = number;
		}

        /// <summary>
        /// タスクのIDを取得します
        /// </summary>
        /// <returns> タスクのID </returns>
        public long getTaskId(){
			return id;
		}

        /// <summary>
        /// このオブジェクトにBattleTaskListViewを設定します
        /// </summary>
        /// <param name="list">設定したいList</param>
        public void setListAndManager(BattleTaskListView list,PlayerBattleTaskManager manager){
            this.list = list;
            this.manager = manager;
        }
	}
}
