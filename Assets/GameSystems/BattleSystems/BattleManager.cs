﻿using System.Collections;
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

        /// <summary>
        /// コンストラクタ
        /// 一度しか呼ばれません
        /// </summary>
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

		/// <summary>
        /// 引数に渡したキャラクターをバトルに参加させます
        /// </summary>
        /// <param name="bal">参加させるキャラクター</param>
        /// <param name="pos">参加させる位置</param>
        /// <param name="ai">キャラクターのAI</param>
		public void joinBattle(IBattleable bal,FieldPosition pos,IEnemyAI ai){
			if (!isBattleing)
				throw new InvalidOperationException ("battle isn't started");

			bal.setIsBattling (true);
			joinedCharacter [pos].Add (bal);

			AIBattleTaskManager manager = MonoBehaviour.Instantiate ((GameObject)Resources.Load("Prefabs/AIBattleManager")).GetComponent<AIBattleTaskManager>();
			manager.setCharacter (bal,ai);
			joinedManager.Add (bal.getUniqueId(),manager);
		}

		/// <summary>
		/// 引数に渡したキャラクターをバトルに参加させます
		/// </summary>
		/// <param name="player">参加させるキャラクター</param>
		/// <param name="pos">参加させる位置</param>
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

		/// <summary>
        /// 与えられたキャラクターの位置から与えられた位置までにいるキャラクターを返します
        /// </summary>
        /// <returns>位置の中にいたキャラクターのリスト</returns>
        /// <param name="bal">起点となるキャラクター</param>
        /// <param name="range">判定したい範囲</param>
		public List<IBattleable> getCharacterInRange(IBattleable bal,int range){
			if (!isBattleing)
				throw new InvalidOperationException ("battle isn't started");
			//Range内のIBattleableを検索
			List<IBattleable> list = new List<IBattleable>();
			FieldPosition pos =  searchCharacter(bal);

            int index = restructionPositionValue(pos, -1 * range);
            int maxPos = restructionPositionValue(pos, range);

            for (; index <= maxPos; index++) {
				list.AddRange (joinedCharacter[(FieldPosition) index]);
			}
			return list;
		}

		/// <summary>
        /// 対象はバトルから離脱します
        /// </summary>
        /// <param name="bal">離脱するキャラクター</param>
        private void escapeCommand(IBattleable bal){
			if (!isBattleing)
				throw new InvalidOperationException ("battle isn't started");

			removeBalFromJoinedCharacter (bal);
			bal.setIsBattling (false);
		}

		/// <summary>
        /// 渡されたキャラクターをバトルから削除します
        /// </summary>
        /// <param name="bal">削除するキャラクター</param>
		private void removeBalFromJoinedCharacter(IBattleable bal){
			if (!isBattleing)
				throw new InvalidOperationException ("battle isn't started");

			FieldPosition pos =  searchCharacter (bal);
			joinedCharacter [pos].Remove (bal);
		}

		/// <summary>
        /// 起点となるキャラクターから指定された位置までのキャラクター数を取得します
        /// </summary>
        /// <returns>キャラクター数</returns>
        /// <param name="bal">起点となるキャラクター</param>
        /// <param name="range">検索したい範囲</param>
		public int sumFromAreaTo(IBattleable bal,int range){
			if (!isBattleing)
				throw new InvalidOperationException ("battle isn't started");

			FieldPosition nowPos = searchCharacter (bal);
			int count = 0;
            int index = restructionPositionValue(nowPos, -1 * range);
            int maxPos = restructionPositionValue(nowPos, range);
            for(;index <= maxPos;index++){
				count += joinedCharacter[(FieldPosition) index].Count;
			}
			//自分が1つ入るので-1
			return count - 1;
		}

		/// <summary>
		/// 全てのバトル参加中キャラクター数を取得します
		/// </summary>
		/// <returns>全てのバトル参加中キャラクター数</returns>
		public int sumAll(){
			if (!isBattleing)
				throw new InvalidOperationException ("battle isn't started");

			int returnValue = 0;
            var keys = joinedCharacter.Keys;
            foreach(FieldPosition pos in keys){
				returnValue += joinedCharacter [pos].Count;
			}
			return returnValue;
		}

		/// <summary>
		/// 全てのバトル参加中キャラクターオブジェクトを取得します
		/// </summary>
		/// <returns>全てのバトル参加中キャラクターオブジェクトリスト</returns>
		public List<IBattleable> getJoinedBattleCharacter(){
			if (!isBattleing)
				throw new InvalidOperationException ("battle isn't started");

			List<IBattleable> returnList = new List<IBattleable> ();
			foreach(FieldPosition pos in joinedCharacter.Keys){
				returnList.AddRange (joinedCharacter[pos]);
			}
			return returnList;
		}

		/// <summary>
        /// 対象を指定された量移動させます
        /// </summary>
        /// <param name="bal">移動させるキャラクター</param>
        /// <param name="moveness">移動させる量</param>
		public void moveCommand(IBattleable bal,int moveness){
            Debug.Log("into moveCommand");
			if (!isBattleing)
				throw new InvalidOperationException ("battle isn't started");

			//引数に渡されたIBattleableキャラの位置を検索
			FieldPosition nowPos = searchCharacter (bal);

			//値が適切か判断
			if (Enum.GetNames (typeof(FieldPosition)).Length < (int)(nowPos + moveness))
				throw new ArgumentException ("invalid moveness");

            int movePosValue = restructionPositionValue(nowPos, moveness);
			FieldPosition movePos = (FieldPosition)movePosValue;
			//移動処理
			joinedCharacter [nowPos].Remove (bal);
			joinedCharacter [movePos].Add (bal);

			bal.syncronizePositioin(field.getNextPosition(nowPos + moveness));
            Debug.Log(nowPos + "→" + movePos);
			Debug.Log("end moveCommand");
		}

		/// <summary>
        /// 起点となるキャラクターから指定された範囲まででもっとも危険な(敵性レベルが高い)位置を検索します
        /// </summary>
        /// <returns>もっとも危険な位置</returns>
        /// <param name="bal">起点となるキャラクター</param>
        /// <param name="range">検索する範囲</param>
		public FieldPosition whereIsMostDengerPositionInRange(IBattleable bal,int range){
			if (!isBattleing)
				throw new InvalidOperationException ("battle isn't started");

			//暫定最大危険ポジションのレベル合計より現在のポジションのレベル合計が高い
			Func<int[],bool> function = (int[] list) =>{
				return list[0] > list[1];
			};

			return judgePosition (function, bal, range);
		}

		/// <summary>
        /// 起点となるキャラクターから指定された範囲でもっとも危険な(敵性レベルが低い)位置を検索します
        /// </summary>
        /// <returns>もっとも安全な位置</returns>
        /// <param name="bal">起点となるキャラクター</param>
        /// <param name="range">検索する範囲</param>
		public FieldPosition whereIsMostSafePositionInRange(IBattleable bal,int range ){
			if (!isBattleing)
				throw new InvalidOperationException ("battle isn't started");

			//暫定最大安全ポジションのレベル合計より現在のポジションのレベル合計が低いor暫定が0
			Func<int[],bool> function = (int[] list) =>{
				return list[0] < list[1] || list[1] == 0;
			};

			return judgePosition (function, bal, range);
		}

		/// <summary>
        /// 与えられた関数に適する位置を検索します
        /// </summary>
        /// <returns>検索結果のField</returns>
        /// <param name="function">検索条件の関数</param>
        /// <param name="bal">起点となるキャラクター</param>
        /// <param name="range">検索範囲</param>
		private FieldPosition judgePosition(Func<int[],bool> function,IBattleable bal,int range){
			if (!isBattleing)
				throw new InvalidOperationException ("battle isn't started");
			
			FieldPosition nowPos = searchCharacter (bal);
			FieldPosition returnPos = nowPos;
			int returnAreaSum = 0;
            int index = restructionPositionValue(nowPos, -1 * range);
            int maxpos = restructionPositionValue(nowPos, range);

            Debug.Log(index + " ind max " + maxpos);

			for(;index <= maxpos;index++){
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

		/// <summary>
        /// 与えられたキャラクターの位置を検索します
        /// </summary>
        /// <returns>キャラクターの位置</returns>
        /// <param name="target">検索したいキャラクター</param>
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

		/// <summary>
		/// 指定した位置にいるキャラクターオブジェクトのリストを取得します
		/// </summary>
		/// <returns>指定した位置にいるキャラクターオブジェクトのリスト</returns>
		/// <param name="pos">指定する位置</param>
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

        /// <summary>
        /// 与えられた位置を起点として指定した数値移動する場合、最大と最小を割らないようにして返します
        /// </summary>
        /// <returns>正当な値</returns>
        /// <param name="pos">起点</param>
        /// <param name="range">判定したい値</param>
        public int restructionPositionValue(FieldPosition pos,int range){
            int positionValue = (int)pos + range;
			int numberOfField = Enum.GetNames(typeof(FieldPosition)).Length;
			if (positionValue < 0) {
				positionValue = 0;
			} else if (positionValue >= numberOfField) {
				positionValue = numberOfField - 1;
			}
            return positionValue;
        }
       
        /// <summary>
        /// 指定したユニークIDのTaskManagerを返します
        /// </summary>
        /// <returns>指定したTaskManager</returns>
        /// <param name="uniqueId">指定するユニークID</param>
        public IBattleTaskManager getTaskManager(long uniqueId){
            var keys = joinedManager.Keys;
            foreach(long id in keys){
                if (id == uniqueId)
                    return joinedManager[id];
            }
            throw new ArgumentException("unknown id " + uniqueId);
        }

	}

	//戦闘フィールドでの状態を表します。ZEROを中心としてPL側がマイナス、NPC側がプラスです。
	public enum FieldPosition{ZERO = 0,ONE = 1,TWO = 2,THREE = 3,FOUR = 4,FIVE = 5,SIX = 6};

	//方向を表します。便宜上、等しいを意味するドローも含まれます
	public enum Direction{PLUS = 1,MINUS = -1,DRAW = 0}
}
