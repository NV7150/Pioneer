using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BattleSystem{
	public class BattleTaskNode : MonoBehaviour {
		private BattleTask task;
		private BattleTaskListView list;
		public Text name;
		public GameObject view;
        private long id;

		public void setTask(BattleTask task){
			this.task = task;
			name.text = task.getName ();
		}

		public void canselChosen(){
            list.deleteTask(task);
		}

		public void delete(){
            Destroy (view);
		}

		public void setTaskId(long number){
			this.id = number;
		}

        public long getTaskId(){
			return id;
		}

        public void setList(BattleTaskListView list){
            this.list = list;
        }
	}
}
