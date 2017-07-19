using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleSystem{
	public class BattleTaskListView : MonoBehaviour {
        /// <summary> データを取得するPlayerBattleTaskManager </summary>
		private PlayerBattleTaskManager manager;
        /// <summary> 登録済みのBattleTaskNodeのリスト</summary>
        private List<BattleTaskNode> nodes = new List<BattleTaskNode>();
        /// <summary> アタッチされるscrollview </summary>
		public GameObject view;
        /// <summary> viewのcontentオブジェクト(scrollviewの要素) </summary>
		public GameObject content;
        /// <summary> contentに追加するBattleTaskNodeのもとのprefab </summary>
        private GameObject nodePrefab;

        /// <summary> マネージャが指定されているか </summary>
        private bool managerSetted = false;

		// Use this for initialization
		void Start () {
            nodePrefab = (GameObject)Resources.Load("Prefabs/BattleTaskNode");
		}

        /// <summary>
        /// PlayerBattleTaskManagerをセットします
        /// </summary>
        /// <param name="manager"> セットしたいPlayerBattleTaskManager </param>
        public void setManager(PlayerBattleTaskManager manager){
            if(!managerSetted){
                this.manager = manager;
            }
            managerSetted = true;
        }

        /// <summary>
        /// タスクを追加します
        /// </summary>
        /// <param name="task"> 追加するタスク </param>
        public void setTask(BattleTask task){
            GameObject node = Instantiate(nodePrefab);
            node.transform.SetParent(content.transform);
            BattleTaskNode nodeContent = node.GetComponent<BattleTaskNode>();
            nodeContent.setTaskId(task.getBattleTaskId());
            nodeContent.setTask(task);
            nodeContent.setListAndManager(this,manager);
            nodes.Add(nodeContent);
        }

        /// <summary>
        /// タスクを削除します
        /// </summary>
        /// <param name="task"> 削除するタスク </param>
        public void deleteTask(BattleTask task) {
            foreach (BattleTaskNode node in nodes) {
                if (node.getTaskId() == task.getBattleTaskId()) {
                    node.delete();
                    nodes.Remove(node);
                    break;
                }
            }
            manager.finishedTask(task);
		}
	}
}
