using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using character;
using skill;
using parameter;

/*BattleManagerクラス
 * 戦闘処理の仲介や戦闘状態の管理などを行います
*/
namespace battleSystem{
	public class BattleManager{
		private static readonly BattleManager INSTANCE = new BattleManager();
		private Dictionary<FieldPosition,List<IBattleable>> joinedCharacter = new Dictionary<FieldPosition,List<IBattleable>>();
		private BattleField field;


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
			Debug.Log ("called joined");
			bal.setIsBattling (true);
			joinedCharacter [pos].Add (bal);
			FieldPosition position = pos;
			while (bal.getIsBattling()) {
				yield return new WaitForSeconds( actionCommand(bal));
			}
			joinedCharacter [pos].Remove (bal);
		}

		//攻撃・スキル使用を行います。
		private float actionCommand(IBattleable bal){
			ActiveSkill useSkill = bal.decideSkill();
			return useSkill.use (bal);
		}

		public void attackCommand(IBattleable bal,int range,int basicHitness,int attack,SkillAttribute attribute,Ability useAbility){
			//Range内のIBattleableを検索
			List<IBattleable> list = new List<IBattleable> ();
			FieldPosition pos =  searchCharacter(bal);
			for (int i = 0; i < range + 1; i++) {
				list.AddRange (joinedCharacter[pos + i]);
			}
			//targetの決定
			List<IBattleable> targets = bal.decideTarget (list);
			//命中値を求める
			int hitness = bal.getHitness (basicHitness);
			foreach(IBattleable target in targets){
				//対象のリアクション
				IPassiveSkill reaction = target.decidePassiveSkill ();
				reaction.use (target);
				//命中判定
				if (hitness > target.getDodgeness()) {
					//ダメージ処理
					target.dammage (bal.attack(attack,useAbility),attribute);
				}
			}
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
			if(!removeBalFromJoinedCharacter (bal, pos))
				throw new Exception ("balオブジェクトの情報が不正です");
			bal.setIsBattling (false);
			return 0;
		}

		//渡された位置にある渡されたbalオブジェクトをjoinedCharacterディクショナリから削除します
		private bool removeBalFromJoinedCharacter(IBattleable bal,FieldPosition pos){
			if (joinedCharacter [pos].Contains (bal)) {
				joinedCharacter [pos].Remove (bal);
				return true;
			} else {
				return false;
			}
		}

		/*渡されたbalオブジェクトから行動のコマンドを取得し、適切に処理します
		 * Battleable bal 処理したいBattleableオブジェクト
		 * FiealdPosition pos 対象の位置
		*/
//		private float decideCommand(IBattleable bal,ref FieldPosition pos){
//			Debug.Log ("DecideCommand "  + pos);
//			switch (bal.decideCommand ()) {
//				case BattleCommand.ACTION:
//					return actionCommand (bal, pos);
//
//				case BattleCommand.MOVE:
//					return moveCommand (bal, ref pos);
//
//				case BattleCommand.ESCAPE:
//					return escapeCommand (bal, pos);
//			}
//			throw new Exception ("invit 引数");
//		}

		public int sumFromAreaTo(IBattleable bal,int range){
			FieldPosition area = searchCharacter (bal);
			int count = 0;
			for(int i = (int) area;i < (int)area + range;i++){
				count += joinedCharacter[(FieldPosition) i].Count;
			}
			return count;
		}

		public int sumAll(){
			int returnValue = 0;
			foreach(FieldPosition pos in joinedCharacter.Keys){
				returnValue += joinedCharacter [pos].Count;
			}
			return returnValue;
		}

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

		private FieldPosition judgePosition(Func<int[],bool> function,IBattleable bal,int range){
			FieldPosition nowPos = searchCharacter (bal);
			FieldPosition returnPos;
			int returnAreaSum = 0;
			for(int i = (int)nowPos - range;i > (int)nowPos + range;i++){
				int areaLevelSum = 0;
				foreach(IBattleable target in joinedCharacter[(FieldPosition) i]){
					if (bal.isHostility (target)) {
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
	}


	//戦闘フィールドでの状態を表します。ZEROを中心としてPL側がマイナス、NPC側がプラスです。
	public enum FieldPosition{ZERO = 0,ONE = 1,TWO = 2,THREE = 3,FOUR = 4,FIVE = 5,SIX = 6};

	//戦闘コマンドを表します。ACTION:攻撃・スキルorアイテムの使用 MOVE:戦闘エリアの移動 RUN:逃走
	public enum BattleCommand{ACTION,MOVE,ESCAPE};

	//方向を表します便宜上、等しいを意味するドローも含まれます
	public enum Direction{PLUS = 1,MINUS = -1,DRAW = 0}
}
