using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace character{
	public class Container : MonoBehaviour {
		public GameObject model;
		ICharacter chara;

		// Use this for initialization
		void Start () {

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

		//containerにcharacterを設定します
		public void setCharacter(ICharacter character){
			chara = character;
		}
	}
}
