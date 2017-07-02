using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character{
	public class Container : MonoBehaviour {
		public GameObject model;
		public ICharacter character;

		// Use this for initialization
		void Start () {
		}

		// Update is called once per frame
		void Update () {
//			Debug.Log ("a");
			if (character == null) {
//				print ("null.");
			} else {
				character.act ();
			}
		}

		//外面のGameObjectを返します
		public GameObject getModel(){
			return model;
		}

		//MonoBehaviourの機能を動作させるExecutorを返します
		public MonoBehaviour getExcecutor(){
			return this;
		}

		public void setCharacter(ICharacter chara){
			this.character = chara;
		}
	}
}
