using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleSystem{
	public class BattleTaskListView : MonoBehaviour {
		private PlayerBattleTaskManager manager;
        private List<BattleTaskNode> nodes = new List<BattleTaskNode>();
		public GameObject view;
		public GameObject content;
        private GameObject nodePrefab;

        private bool managerSetted = false;

		// Use this for initialization
		void Start () {
            nodePrefab = (GameObject)Resources.Load("Prefabs/BattleTaskNode");
		}
		
		// Update is called once per frame
		void Update () {
			
		}

        public void setManager(PlayerBattleTaskManager manager){
            if(!managerSetted){
                this.manager = manager;
            }
            managerSetted = true;
        }

        public void setTask(BattleTask task){
            GameObject node = Instantiate(nodePrefab);
            node.transform.SetParent(content.transform);
            BattleTaskNode nodeContent = node.GetComponent<BattleTaskNode>();
            nodeContent.setTaskId(task.getBattleTaskId());
            nodeContent.setTask(task);
            nodeContent.setList(this);
            nodes.Add(nodeContent);
        }

        public void deleteTask(BattleTask task) {
            foreach (BattleTaskNode node in nodes) {
                if (node.getTaskId() == task.getBattleTaskId()) {
                    node.delete();
                    nodes.Remove(node);
                    break;
                }
            }
            manager.canseledTask(task);
		}
	}
}
