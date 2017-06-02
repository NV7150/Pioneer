using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace character{
	public class Container : MonoBehaviour {
		public GameObject model;
		public ICharacter character;

		// Use this for initialization
		void Start () {
		}

		// Update is called once per frame
		void Update () {
			if (character == null) {
				print ("null.");
				return;
			}
			character.act ();
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
			Debug.Log (chara);
			this.character = chara;
		}
	}
}
