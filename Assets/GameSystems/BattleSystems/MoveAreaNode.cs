using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace BattleSystem{
	public class MoveAreaNode : MonoBehaviour {
		public Text text;
		private FieldPosition pos;
		private PlayerBattleTaskManager manager;

		// Use this for initialization
		void Start () {
			
		}
		
		// Update is called once per frame
		void Update () {
			
		}

		public void setState(FieldPosition pos,PlayerBattleTaskManager manager){
			this.pos = pos;
			this.manager = manager;

			text.text = Enum.GetName(typeof(FieldPosition),pos);
		}

		public void chosen(){
			manager.moveAreaChose (pos);
		}
	}
}
