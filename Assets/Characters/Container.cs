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

        private static GameObject fieldKeeper;
        private static GameObject battleCharacterKeeper;

        private void Awake(){
            do {
                fieldKeeper = GameObject.Find("FieldKeeper");
            } while (fieldKeeper == null);

            do {
                battleCharacterKeeper = GameObject.Find("BattleCharacterKeeper");
            } while (battleCharacterKeeper == null);

            transform.SetParent(fieldKeeper.transform);
        }

        // Use this for initialization
        void Start () {
		}

		// Update is called once per frame
		void Update () {
			if (user == null) {
			} else {
				user.act ();
			}
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
            isPlayer = chara is Player;
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
		[MethodImpl(MethodImplOptions.Synchronized)]
        private void startBattle(ICharacter opponent){
            //ICharacterをIBattleableにキャスト
            IBattleable oppnentBal = (IBattleable)opponent;
            IBattleable userBal = (IBattleable)user;
            //敵対しているかを判定
            if (oppnentBal.isHostility(userBal.getFaction())) {
                if (userBal is Player)
                    ((Player)userBal).keepPos();

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
