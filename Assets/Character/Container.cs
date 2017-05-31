using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace character{
	public class Container : MonoBehaviour {
		public GameObject model;
		private ICharacter chara;

		// Use this for initialization
		void Start () {

		}
		public Container(GameObject model,ICharacter character){
			this.model = model;
			this.chara = character;
		}

		// Update is called once per frame
		void Update () {
			chara.act ();
		}

		//外面のGameObjectを返します
		public GameObject getModel(){
			return model;
		}

		//MonoBehaviourの機能を動作させるExecutorを返します
		public MonoBehaviour getExcecutor(){
			return this;
		}
	}
}
