using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.CompilerServices;

using BattleSystem;

namespace Character{
    /// <summary>
    /// Containerはキャラクターの外面的な(GameObject側の)処理を担当します
    /// </summary>
	public class Container : MonoBehaviour {
		public GameObject model;
        public ICharacter user;
        private bool isBattleable = false;
        private bool isPlayer = false;
        private Camera camera;
        private int distance = 10000;

		// Use this for initialization
		void Start () {
		}

		// Update is called once per frame
		void Update () {
			if (user == null) {
			} else {
				user.act ();
			}

            if (isPlayer) {
                if (Input.GetKeyDown(KeyCode.Return)) {
                    searchFront();
                }
            }
		}

        private void searchFront() {
            Vector3 center = new Vector3(Screen.width / 2, Screen.height / 2);
            Ray ray = camera.ScreenPointToRay(center);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, distance)) {
                Container hitContainer = hitInfo.transform.GetComponent<Container>();
                if(hitContainer != null) {
                    ICharacter character = hitContainer.getCharacter();
                    if(character is IFriendly) {
                        startTalk((IFriendly)character);
                    }
                }
            }
        }

        private void startTalk(IFriendly character) {
            character.talk((IFriendly)character);
        }

        /// <summary>
        /// ContainerがアタッチされているGameObjectを取得します
        /// </summary>
        /// <returns>アタッチされているGameObject</returns>
        public GameObject getModel() {
            return model;
        }

		/// <summary>
        /// Containerにキャラクターを設定します
        /// </summary>
        /// <param name="chara">設定するキャラクター</param>
		public void setCharacter(ICharacter chara){
			this.user = chara;
            isBattleable = chara is IBattleable;
            isPlayer = chara is Hero;
            if (isPlayer)
                camera = GameObject.Find("MainCamera").GetComponent<Camera>();
		}

        /// <summary>
        /// 設定されているキャラクターを取得します
        /// </summary>
        /// <returns>設定されているキャラクター</returns>
        public ICharacter getCharacter(){
            return this.user;
        }

        private void OnCollisionEnter(Collision collision){
            Container container = collision.gameObject.GetComponent<Container>();
            if(container != null){
                if(container.getCharacter() is IBattleable && isBattleable){
                    startBattle(container.getCharacter());
                }
            }
        }

        /// <summary>
        /// エンカウントし、キャラクターをバトルに参加させます
        /// </summary>
        /// <param name="opponent">エンカウントしたキャラクター</param>
        /// <param name="averageEachPos">エンカウントしたキャラクターと自身の位置の平均</param>
		[MethodImpl(MethodImplOptions.Synchronized)]
        private void startBattle(ICharacter opponent){
            //ICharacterをIBattleableにキャスト
            IBattleable oppnentBal = (IBattleable)opponent;
            IBattleable userBal = (IBattleable)user;
            //敵対しているかを判定
            if (oppnentBal.isHostility(userBal.getFaction())) {
				//バトル開始していなかったら開始
				if (!BattleManager.getInstance().getIsBattleing()) {
					BattleManager.getInstance().startNewBattle();
				}

                //ユーザーをエンカウント
                userBal.encount();
            }
        }
    }
}
