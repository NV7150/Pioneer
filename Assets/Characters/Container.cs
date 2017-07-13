using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.CompilerServices;

using BattleSystem;

namespace Character{
	public class Container : MonoBehaviour {
		public GameObject model;
        public ICharacter user;
        private bool isBattleable = false;

		// Use this for initialization
		void Start () {
		}

		// Update is called once per frame
		void Update () {
//			Debug.Log ("a");
			if (user == null) {
//				print ("null.");
			} else {
				user.act ();
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

		//Characterを設定します
		public void setCharacter(ICharacter chara){
			this.user = chara;
            isBattleable = chara is IBattleable;
		}

        public ICharacter getCharacter(){
            return this.user;
        }

        private void OnCollisionEnter(Collision collision){
            Container container = collision.gameObject.GetComponent<Container>();
            if(container != null){
                if(container.getCharacter() is IBattleable && isBattleable){
                    Vector3 averagePos = (transform.position + collision.transform.position) / 2;
                    startBattle(container.getCharacter(),averagePos);
                }
            }
        }

		[MethodImpl(MethodImplOptions.Synchronized)]
        private void startBattle(ICharacter opponent,Vector3 averageEachPos){
            //ICharacterをIBattleableにキャスト
            IBattleable oppnentBal = (IBattleable)opponent;
            IBattleable userBal = (IBattleable)user;
            //敵対しているかを判定
            if (oppnentBal.isHostility(userBal.getFaction())) {
				//バトル開始していなかったら開始
				if (!BattleManager.getInstance().getIsBattleing()) {
					BattleManager.getInstance().StartNewBattle(averageEachPos);
				}

                //ユーザーをエンカウント
                userBal.encount();
            }
        }
    }
}
