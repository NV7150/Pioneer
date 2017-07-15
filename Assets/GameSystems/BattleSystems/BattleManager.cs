using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using Character;
using Skill;
using Parameter;
using AI;

using HealSkillAttribute = Skill.ActiveSkillParameters.HealSkillAttribute;
using BattleAbility = Parameter.CharacterParameters.BattleAbility;
using Faction = Parameter.CharacterParameters.Faction;

/*BattleManagerクラス
 * 戦闘処理の仲介や戦闘状態の管理などを行います
*/
namespace BattleSystem{
	public class BattleManager{
        /// <summary>  唯一のインスタンス </summary>
		private static readonly BattleManager INSTANCE = new BattleManager();
        /// <summary> バトルに参加済みのキャラクターのリスト </summary>
		private Dictionary<FieldPosition,List<IBattleable>> joinedCharacter = new Dictionary<FieldPosition,List<IBattleable>>();
        /// <summary> バトルに参加済みのキャラクターのユニークIDとそのバトルマネージャのディクショナリ </summary>
        private Dictionary<long,IBattleTaskManager> joinedManager = new Dictionary<long, IBattleTaskManager>();
        /// <summary> 展開中のバトルフィールド </summary>
		private BattleField field;
        /// <summary> バトルしているか falseだとBattleManagerのほとんどのメソッドがロックされます </summary>
		private bool isBattleing = false;

		//唯一のインスタンスを取得します
		public static BattleManager getInstance(){
			return INSTANCE;
		}

		private BattleManager(){
			foreach(FieldPosition pos in System.Enum.GetValues(typeof(FieldPosition))){
				joinedCharacter.Add (pos, new List<IBattleable> ());
			}
		}

        /// <summary>
        /// 新たにバトルを開始します
        /// </summary>
        /// <param name="basicPoint"> 起点とする座標 </param>
		public void StartNewBattle(Vector3 basicPoint){
			field = new BattleField (basicPoint);
			isBattleing = true;
		}

        /// <summary>
        /// キャラクターの死亡による離脱処理をおこないます
        /// </summary>
        /// <param name="character"> 死亡したキャラクター </param>
		public void deadCharacter(IBattleable character){
			if (!isBattleing)
				throw new InvalidOperationException ("battle isn't started");

			FieldPosition pos = searchCharacter (character);
			joinedCharacter [pos].Remove (character);
			character.death ();
			joinedManager [character.getUniqueId ()].finished ();
			joinedManager.Remove (character.getUniqueId ());

			if (isContinuingBattle ()) {
				finishBattle ();
			} else {
				var keys = joinedManager.Keys;
				foreach(long id in keys){
					IBattleTaskManager taskManager = joinedManager [id];
					taskManager.deleteTaskFromTarget (character);
				}
			}
		}

        /// <summary>
        /// バトルが続いているかを検査します
        /// </summary>
        /// <returns><c>true</c>, バトルは続いている, <c>false</c> バトル終了している </returns>
        private bool isContinuingBattle(){
			var keys = joinedCharacter.Keys;
			List<Faction> factions = new List<Faction> ();
			foreach(FieldPosition pos in keys){
				foreach(IBattleable chara in joinedCharacter[pos]){
					if (!factions.Contains (chara.getFaction ()))
						factions.Add (chara.getFaction());
				}
			}
			return CharacterParameterSupporter.isThereHostality (factions);
		}

		private void finishBattle(){
			var uniqueIds = joinedManager.Keys;
			foreach(long id in uniqueIds){
				joinedManager [id].win ();
				joinedManager [id].finished ();
			}
			joinedManager.Clear ();

			var fieldPositions = System.Enum.GetValues (typeof(FieldPosition));
			foreach(FieldPosition pos in fieldPositions){
				joinedCharacter [pos].Clear ();
			}
			isBattleing = false;
		}

		/*引数に渡したBattleableオブジェクトを戦闘に参加させます
		 * Battleable bal 戦闘に参加させたいオブジェクト
		 * FiealdPosition pos 初期の戦闘参加位置
		*/
		public void joinBattle(IBattleable bal,FieldPosition pos,IEnemyAI ai){
			if (!isBattleing)
				throw new InvalidOperationException ("battle isn't started");

			bal.setIsBattling (true);
			joinedCharacter [pos].Add (bal);

			AIBattleTaskManager manager = MonoBehaviour.Instantiate ((GameObject)Resources.Load("Prefabs/AIBattleManager")).GetComponent<AIBattleTaskManager>();
			manager.setCharacter (bal,ai);
			joinedManager.Add (bal.getUniqueId(),manager);
		}

		public void joinBattle(IPlayable player,FieldPosition pos){
			if (!isBattleing)
				throw new InvalidOperationException ("battle isn't started");

			player.setIsBattling (true);
			joinedCharacter [pos].Add (player);

			GameObject view = MonoBehaviour.Instantiate ((GameObject)Resources.Load ("Prefabs/PlayerBattleTaskManager"));
			PlayerBattleTaskManager manager =  view.GetComponent<PlayerBattleTaskManager> ();
			manager.setPlayer (player);
			joinedManager.Add (player.getUniqueId(),manager);
		}

		//攻撃処理を行います
		public void attackCommand(IBattleable bal,List<IBattleable> targets,AttackSkill skill){
			foreach(IBattleable target in targets){
				//対象のリアクション
				joinedManager[target.getUniqueId()].offerReaction(bal,skill);
			}
		}

		//与えられたキャラクターから射程範囲内にいるキャラクターのリストを返します
		public List<IBattleable> getCharacterInRange(IBattleable bal,int range){
			if (!isBattleing)
				throw new InvalidOperationException ("battle isn't started");

			//Range内のIBattleableを検索
			List<IBattleable> list = new List<IBattleable>();
			FieldPosition pos =  searchCharacter(bal);
			for (int i = (int) pos; i < (int)pos + range + 1; i++) {
				list.AddRange (joinedCharacter[(FieldPosition) i]);
			}
			return list;
		}

		//回復処理をします
		public void healCommand(IBattleable bal,int range,int basicHealRate,HealSkillAttribute attribute,BattleAbility useAbility){
			if (!isBattleing)
				throw new InvalidOperationException ("battle isn't started");

			//Range内のIBattleableを検索
			List<IBattleable> list = new List<IBattleable> ();
			FieldPosition pos =  searchCharacter(bal);
			for (int i = 0; i < range + 1; i++) {
				list.AddRange (joinedCharacter[pos + i]);
			}
			//targetの決定
//			List<IBattleable> targets = bal.decideTarget (list);
			//回復処理
//			foreach(IBattleable target in targets){
//				target.healed (bal.healing(basicHealRate,useAbility),attribute);
//			}

			throw new NotSupportedException ();
		}

		//対象は戦闘から離脱します
		private float escapeCommand(IBattleable bal,FieldPosition pos){
			if (!isBattleing)
				throw new InvalidOperationException ("battle isn't started");

			removeBalFromJoinedCharacter (bal);
			bal.setIsBattling (false);
			return 0;
		}

		//渡された位置にある渡されたbalオブジェクトをjoinedCharacterディクショナリから削除します
		private void removeBalFromJoinedCharacter(IBattleable bal){
			if (!isBattleing)
				throw new InvalidOperationException ("battle isn't started");

			FieldPosition pos =  searchCharacter (bal);
			joinedCharacter [pos].Remove (bal);
		}

		//与えられたキャラの位置から与えられた範囲のバトル参加中のキャラクターの数を返します。
		public int sumFromAreaTo(IBattleable bal,int range){
			if (!isBattleing)
				throw new InvalidOperationException ("battle isn't started");

			FieldPosition area = searchCharacter (bal);
			int count = 0;
			int index = (((int)area - range) < 0) ? 0 : (int)area - range;
			for(;index < (int)area + range + 1;index++){
				count += joinedCharacter[(FieldPosition) index].Count;
			}
			//自分が1つ入るので-1
			return count - 1;
		}

		//全てのバトル参加中キャラクターの数の合計を返します
		public int sumAll(){
			if (!isBattleing)
				throw new InvalidOperationException ("battle isn't started");

			int returnValue = 0;
			foreach(FieldPosition pos in joinedCharacter.Keys){
				returnValue += joinedCharacter [pos].Count;
			}
			return returnValue;
		}

		//全ての戦闘に参加しているIBattleableキャラクターを取得します
		public List<IBattleable> getJoinedBattleCharacter(){
			if (!isBattleing)
				throw new InvalidOperationException ("battle isn't started");

			List<IBattleable> returnList = new List<IBattleable> ();
			foreach(FieldPosition pos in joinedCharacter.Keys){
				returnList.AddRange (joinedCharacter[pos]);
			}
			return returnList;
		}

		//動きます
		public void moveCommand(IBattleable bal,int moveness){
			if (!isBattleing)
				throw new InvalidOperationException ("battle isn't started");

			//引数に渡されたIBattleableキャラの位置を検索
			FieldPosition nowPos = searchCharacter (bal);

			//値が適切か判断
			if (Enum.GetNames (typeof(FieldPosition)).Length < (int)(nowPos + moveness))
				throw new ArgumentException ("invalid moveness");

			int movePosValue = (int)nowPos + moveness;
			if (movePosValue < 0) {
				movePosValue = 0;
			} else if (movePosValue > Enum.GetNames (typeof(FieldPosition)).Length - 1) {
				movePosValue = Enum.GetNames (typeof(FieldPosition)).Length - 1;
			}

			FieldPosition movePos = (FieldPosition)movePosValue;
			//移動処理
			joinedCharacter [nowPos].Remove (bal);
			joinedCharacter [movePos].Add (bal);

			bal.syncronizePositioin (field.getNextPosition(nowPos + moveness));
		}

		//指定されたキャラクターの指定された範囲でもっとも危険な（敵対キャラクターのレベル合計が高い）ポジションを返します
		public FieldPosition whereIsMostDengerPositionInRange(IBattleable bal,int range){
			if (!isBattleing)
				throw new InvalidOperationException ("battle isn't started");

			//暫定最大危険ポジションのレベル合計より現在のポジションのレベル合計が高い
			Func<int[],bool> function = (int[] list) =>{
				return list[0] > list[1];
			};

			return judgePosition (function, bal, range);
		}

		//指定されたキャラクターの指定された範囲でもっとも安全な（敵対キャラクターのレベル合計が低い）ポジションを返します
		public FieldPosition whereIsMostSafePositionInRange(IBattleable bal,int range ){
			if (!isBattleing)
				throw new InvalidOperationException ("battle isn't started");

			//暫定最大安全ポジションのレベル合計より現在のポジションのレベル合計が低いor暫定が0
			Func<int[],bool> function = (int[] list) =>{
				return list[0] < list[1] || list[1] == 0;
			};

			return judgePosition (function, bal, range);
		}

		//与えられた関数似合う条件の場所を検索します。
		private FieldPosition judgePosition(Func<int[],bool> function,IBattleable bal,int range){
			if (!isBattleing)
				throw new InvalidOperationException ("battle isn't started");
			
			FieldPosition nowPos = searchCharacter (bal);
			FieldPosition returnPos = nowPos;
			int returnAreaSum = 0;
			int index = (int)nowPos - range;
			int numberOfField = Enum.GetNames (typeof(FieldPosition)).Length;
			if(index < 0){
				index = 0;
			}else if(index > numberOfField){
				index = numberOfField;
			}

			int maxpos = (int)nowPos + range + 1;
			if(maxpos < 0){
				maxpos = 0;
			}else if(maxpos > numberOfField){
				maxpos = numberOfField;
			}

			for(;index < maxpos;index++){
				int areaLevelSum = 0;
				foreach(IBattleable target in joinedCharacter[(FieldPosition) index]){
					if (bal.isHostility (target.getFaction())) {
						areaLevelSum += target.getLevel ();
					}
				}
				if (function(new int[]{areaLevelSum,returnAreaSum})) {
					returnAreaSum = areaLevelSum;
					returnPos = (FieldPosition)index;
				}
			}
			return returnPos;
		}

		//与えられたIBattleableオブジェクトを検索し位置を返します
		public FieldPosition searchCharacter(IBattleable target){
			if (!isBattleing)
				throw new InvalidOperationException ("battle isn't started");

			var poses = joinedCharacter.Keys;
			foreach (FieldPosition pos in poses) {
				foreach (IBattleable character in joinedCharacter[pos]) {
					if (character.Equals (target)) {
						return pos;
					}
				}
			}
			throw new ArgumentException ("Didn't found " + target.ToString());
		}

		//指定されたFieldPositionにいるCharacterを返します
		public List<IBattleable> getAreaCharacter(FieldPosition pos){
			if (!isBattleing)
				throw new InvalidOperationException ("battle isn't started");

			return joinedCharacter [pos];
		}

        /// <summary>
        /// バトルが始まっているかを返します
        /// </summary>
        /// <returns><c>true</c>, バトルが始まっている, <c>false</c> 始まっていない </returns>
        public bool getIsBattleing(){
            return isBattleing;
        }
       
	}

	//戦闘フィールドでの状態を表します。ZEROを中心としてPL側がマイナス、NPC側がプラスです。
	public enum FieldPosition{ZERO = 0,ONE = 1,TWO = 2,THREE = 3,FOUR = 4,FIVE = 5,SIX = 6};

	//方向を表します。便宜上、等しいを意味するドローも含まれます
	public enum Direction{PLUS = 1,MINUS = -1,DRAW = 0}
}
