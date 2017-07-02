using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using Character;
using Skill;
using Parameter;
using AI;

/*BattleManagerクラス
 * 戦闘処理の仲介や戦闘状態の管理などを行います
*/
namespace BattleSystem{
	public class BattleManager{
		private static readonly BattleManager INSTANCE = new BattleManager();
		private Dictionary<FieldPosition,List<IBattleable>> joinedCharacter = new Dictionary<FieldPosition,List<IBattleable>>();
		private Dictionary<long,IBattleTaskManager> enteredManager = new Dictionary<long, IBattleTaskManager>();
		private BattleField field;

		private long playerID;

		//唯一のインスタンスを取得します
		public static BattleManager getInstance(){
			return INSTANCE;
		}

		private BattleManager(){
			foreach(FieldPosition pos in System.Enum.GetValues(typeof(FieldPosition))){
				joinedCharacter.Add (pos, new List<IBattleable> ());
			}
		}

		public void StartNewBattle(Vector3 basicPoint){
			foreach(FieldPosition pos in System.Enum.GetValues(typeof(FieldPosition))){
				joinedCharacter [pos].Clear ();
			}
			field = new BattleField (basicPoint);
		}

		/*引数に渡したBattleableオブジェクトを戦闘に参加させます
		 * Battleable bal 戦闘に参加させたいオブジェクト
		 * FiealdPosition pos 初期の戦闘参加位置
		*/
		public void joinBattle(IBattleable bal,FieldPosition pos,IEnemyAI ai){
			Debug.Log (bal.getName() + " is joined!");

			bal.setIsBattling (true);
			joinedCharacter [pos].Add (bal);

			AIBattleTaskManager manager = MonoBehaviour.Instantiate ((GameObject)Resources.Load("Prefabs/AIBattleManager")).GetComponent<AIBattleTaskManager>();
			manager.setCharacter (bal,ai);
			enteredManager.Add (bal.getUniqueId(),manager);
		}

		public void joinBattle(IPlayable player,FieldPosition pos){
			Debug.Log (player.getName() + " is joined!");

			playerID = player.getUniqueId ();

			player.setIsBattling (true);
			joinedCharacter [pos].Add (player);

			GameObject view = MonoBehaviour.Instantiate ((GameObject)Resources.Load ("Prefabs/BattleNodeController"));
			PlayerBattleTaskManager manager =  view.GetComponent<PlayerBattleTaskManager> ();
			manager.setPlayer (player);
			enteredManager.Add (player.getUniqueId(),manager);
		}

		//攻撃処理を行います
		public void attackCommand(IBattleable bal,List<IBattleable> targets,ActiveSkill skill){
			foreach(IBattleable target in targets){
				//対象のリアクション
				enteredManager[target.getUniqueId()].offerReaction(bal,skill);
			}
		}

		//与えられたキャラクターから射程範囲内にいるキャラクターのリストを返します
		public List<IBattleable> getCharacterInRange(IBattleable bal,int range){
			//Range内のIBattleableを検索
			List<IBattleable> list = new List<IBattleable>();
			FieldPosition pos =  searchCharacter(bal);
			for (int i = (int) pos; i < (int)pos + range + 1; i++) {
				list.AddRange (joinedCharacter[(FieldPosition) i]);
			}
			return list;
		}

		//回復処理をします
		public void healCommand(IBattleable bal,int range,int basicHealRate,HealAttribute attribute,Ability useAbility){
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
			removeBalFromJoinedCharacter (bal);
			bal.setIsBattling (false);
			return 0;
		}

		//渡された位置にある渡されたbalオブジェクトをjoinedCharacterディクショナリから削除します
		private void removeBalFromJoinedCharacter(IBattleable bal){
			FieldPosition pos =  searchCharacter (bal);
			joinedCharacter [pos].Remove (bal);
		}

		//与えられたキャラの位置から与えられた範囲のバトル参加中のキャラクターの数を返します。
		public int sumFromAreaTo(IBattleable bal,int range){
			FieldPosition area = searchCharacter (bal);
			int count = 0;
			for(int i = (int) area;i < (int)area + range;i++){
				count += joinedCharacter[(FieldPosition) i].Count;
			}
			return count;
		}

		//全てのバトル参加中キャラクターの数の合計を返します
		public int sumAll(){
			int returnValue = 0;
			foreach(FieldPosition pos in joinedCharacter.Keys){
				returnValue += joinedCharacter [pos].Count;
			}
			return returnValue;
		}

		//全ての戦闘に参加しているIBattleableキャラクターを取得します
		public List<IBattleable> getJoinedBattleCharacter(){
			List<IBattleable> returnList = new List<IBattleable> ();
			foreach(FieldPosition pos in joinedCharacter.Keys){
				returnList.AddRange (joinedCharacter[pos]);
			}
			return returnList;
		}

		//動きます
		public void moveCommand(IBattleable bal,int moveness){
			//引数に渡されたIBattleableキャラの位置を検索
			FieldPosition nowPos = searchCharacter (bal);

			//値が適切か判断
			if (Enum.GetNames (typeof(FieldPosition)).Length < (int)(nowPos + moveness))
				throw new ArgumentException ("invalid moveness");

			//移動処理
			joinedCharacter [nowPos].Remove (bal);
			joinedCharacter [nowPos + moveness].Add (bal);

		}

		//指定されたキャラクターの指定された範囲でもっとも危険な（敵対キャラクターのレベル合計が高い）ポジションを返します
		public FieldPosition whereIsMostDengerPositionInRange(IBattleable bal,int range){
			//暫定最大危険ポジションのレベル合計より現在のポジションのレベル合計が高い
			Func<int[],bool> function = (int[] list) =>{
				return list[0] > list[1];
			};

			return judgePosition (function, bal, range);
		}

		//指定されたキャラクターの指定された範囲でもっとも安全な（敵対キャラクターのレベル合計が低い）ポジションを返します
		public FieldPosition whereIsMostSafePositionInRange(IBattleable bal,int range ){
			//暫定最大安全ポジションのレベル合計より現在のポジションのレベル合計が低いor暫定が0
			Func<int[],bool> function = (int[] list) =>{
				return list[0] < list[1] || list[1] == 0;
			};

			return judgePosition (function, bal, range);
		}

		//与えられた関数似合う条件の場所を検索します。
		private FieldPosition judgePosition(Func<int[],bool> function,IBattleable bal,int range){
			FieldPosition nowPos = searchCharacter (bal);
			FieldPosition returnPos = nowPos;
			int returnAreaSum = 0;
			for(int i = (int)nowPos - range;i > (int)nowPos + range;i++){
				int areaLevelSum = 0;
				foreach(IBattleable target in joinedCharacter[(FieldPosition) i]){
					if (bal.isHostility (target.getFaction())) {
						areaLevelSum += target.getLevel ();
					}
				}
				if (function(new int[]{areaLevelSum,returnAreaSum})) {
					returnAreaSum = areaLevelSum;
					returnPos = (FieldPosition)i;
				}
			}
			return returnPos;
		}

		//与えられたIBattleableオブジェクトを検索し位置を返します
		public FieldPosition searchCharacter(IBattleable target){
			foreach (FieldPosition pos in joinedCharacter.Keys) {
				foreach (IBattleable character in joinedCharacter[pos]) {
					if (character.Equals (target)) {
						return pos;
					}
				}
			}
			throw new ArgumentException ("Don't found " + target.ToString());
		}

		//指定されたFieldPositionにいるCharacterを返します
		public List<IBattleable> getAreaCharacter(FieldPosition pos){
			return joinedCharacter [pos];
		}
	}


	//戦闘フィールドでの状態を表します。ZEROを中心としてPL側がマイナス、NPC側がプラスです。
	public enum FieldPosition{ZERO = 0,ONE = 1,TWO = 2,THREE = 3,FOUR = 4,FIVE = 5,SIX = 6};

	//戦闘コマンドを表します。ACTION:攻撃・スキルorアイテムの使用 MOVE:戦闘エリアの移動 RUN:逃走
	public enum BattleCommand{ACTION,MOVE,ESCAPE};

	//方向を表します便宜上、等しいを意味するドローも含まれます
	public enum Direction{PLUS = 1,MINUS = -1,DRAW = 0}
}
