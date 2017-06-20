using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using Character;
using Skill;
using Parameter;

/*BattleManagerクラス
 * 戦闘処理の仲介や戦闘状態の管理などを行います
*/
namespace BattleSystem{
	public class BattleManager{
		private static readonly BattleManager INSTANCE = new BattleManager();
		private Dictionary<FieldPosition,List<IBattleable>> joinedCharacter = new Dictionary<FieldPosition,List<IBattleable>>();
		private BattleField field;
		private Dictionary<long,List<BattleTask>> entriedTasks; 

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
		public IEnumerator joinBattle(IBattleable bal,FieldPosition pos){
			Debug.Log (bal.getName() + " is joined!");

			bal.setIsBattling (true);
			joinedCharacter [pos].Add (bal);

			List<BattleTask> tasks = new List<BattleTask> ();

			while (bal.getIsBattling()) {
				WaitForSeconds seconds = new WaitForSeconds (action (bal, tasks));
				yield return seconds;
			}
			joinedCharacter [pos].Remove (bal);
		}

		public IEnumerator joinBattle(IPlayable player,FieldPosition pos){
			Debug.Log (player.getName() + " is joined!");

			player.setIsBattling (true);
			joinedCharacter [pos].Add (player);

			GameObject view = (GameObject) MonoBehaviour.Instantiate ((GameObject)Resources.Load ("Prefabs/BattleNodeController"));
			view.GetComponent<BattleNodeController> ().setPlayer(player);
			List<BattleTask> tasks = view.GetComponent<BattleNodeController> ().getTasks();

			while (player.getIsBattling()) {
				WaitForSeconds seconds = new WaitForSeconds (action (player, tasks));

				yield return seconds;
			}
			joinedCharacter [pos].Remove (player);
		}

		//攻撃・スキル使用を行います。
		private float action(IBattleable bal,List<BattleTask> tasks){
			if (tasks.Count <= 0) {
				return 0f;
			} else {
				ActiveSkill useSkill = tasks [0].getSkill ();
				useSkill.use (bal);
				tasks.Remove (tasks[0]);
				return useSkill.getDelay() + bal.getDelay();
			}
		}

		public void attackCommand(IBattleable bal,List<IBattleable> targets,int basicHitness,int attack,SkillAttribute attribute,Ability useAbility){
			//命中値を求める
			int hitness = bal.getHitness (basicHitness);
			foreach(IBattleable target in targets){
				//対象のリアクション
				PassiveSkill reaction = target.decidePassiveSkill ();
				reaction.use (target);
				//命中判定
				if (hitness > target.getDodgeness()) {
					//ダメージ処理
					target.dammage (bal.attack(attack,useAbility),attribute);
				}
			}
		}

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
			List<IBattleable> targets = bal.decideTarget (list);
			//回復処理
			foreach(IBattleable target in targets){
				target.healed (bal.healing(basicHealRate,useAbility),attribute);
			}
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
		public void moveCommand(IBattleable bal,int basicMoveAmount){
			//引数に渡されたIBattleableキャラの位置を検索
			FieldPosition nowPos = searchCharacter (bal);

			//移動量を決定
			int moveAmount = bal.move(basicMoveAmount);

			//値が適切か判断
			int moveAmountMax = Enum.GetNames (typeof(FieldPosition)).Length - (int)nowPos;
			int moveAmountMin = -1 * (int)nowPos;
			if (moveAmountMax >= basicMoveAmount||moveAmountMin <= basicMoveAmount)
				throw new ArgumentException ("invlit moveAmount");

			//移動処理
			joinedCharacter [nowPos].Remove (bal);
			joinedCharacter [nowPos + moveAmount].Add (bal);
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
			throw new ArgumentException ("Don't found " + target);
		}

		//指定されたFieldPositionにいるCharacterを返します
		public List<IBattleable> getAreaCharacter(FieldPosition pos){
			return joinedCharacter [pos];
		}

		//ユニークIDからタスクを読み込みます
		public BattleTask getTaskFromUniqueId(long uniqueId){
			return entriedTasks [uniqueId][0];
		}
	}


	//戦闘フィールドでの状態を表します。ZEROを中心としてPL側がマイナス、NPC側がプラスです。
	public enum FieldPosition{ZERO = 0,ONE = 1,TWO = 2,THREE = 3,FOUR = 4,FIVE = 5,SIX = 6};

	//戦闘コマンドを表します。ACTION:攻撃・スキルorアイテムの使用 MOVE:戦闘エリアの移動 RUN:逃走
	public enum BattleCommand{ACTION,MOVE,ESCAPE};

	//方向を表します便宜上、等しいを意味するドローも含まれます
	public enum Direction{PLUS = 1,MINUS = -1,DRAW = 0}
}
