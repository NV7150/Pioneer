using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using character;
using skill;


/*BattleManagerクラス
 * 戦闘処理の仲介や戦闘状態の管理などを行います
*/
namespace battleSystem{
	public class BattleManager{
		private static readonly BattleManager INSTANCE = new BattleManager();
		private Dictionary<FiealdPosition,List<BattleableBase>> joinedCharacter = new Dictionary<FiealdPosition,List<BattleableBase>>();
		private BattleField field;


		//唯一のインスタンスを取得します
		public static BattleManager getInstance(){
			return INSTANCE;
		}

		private BattleManager(){
			foreach(FiealdPosition pos in System.Enum.GetValues(typeof(FiealdPosition))){
				joinedCharacter.Add (pos, new List<BattleableBase> ());
			}
		}

		public void StartNewBattle(Vector3 basicPoint){
			foreach(FiealdPosition pos in System.Enum.GetValues(typeof(FiealdPosition))){
				joinedCharacter [pos].Clear ();
			}
			field = new BattleField (basicPoint);
		}

		/*引数に渡したBattleableオブジェクトを戦闘に参加させます
		 * Battleable bal 戦闘に参加させたいオブジェクト
		 * FiealdPosition pos 初期の戦闘参加位置
		*/
		public IEnumerator joinBattle(BattleableBase bal,FiealdPosition pos){
			Debug.Log ("called joined");
			bal.setIsBattling (true);
			joinedCharacter [pos].Add (bal);
			FiealdPosition position = pos;
			while (bal.getIsBattling()) {
				yield return new WaitForSeconds( decideCommand (bal,ref position));
			}
			joinedCharacter [pos].Remove (bal);
		}

		//攻撃・スキル使用を行います。
		private float actionCommand(BattleableBase bal,FiealdPosition pos){
			Debug.Log ("melee" + bal.getMft());
//			Debug.Log (joinedCharacter[FiealdPosition.ZERO].Count);
//			if(bal.getMft() == 101)
//				Debug.Log (bal.ToString());
			ActiveSkill useSkill = bal.decideSkill();
//			Debug.Log ("end decide");
			int range = bal.getRange (useSkill);
//			Debug.Log ("end range");
			List<BattleableBase> list = new List<BattleableBase> ();
//			Debug.Log ("end list");
			for (int i = 0; i < range + 1; i++) {
				list.AddRange (joinedCharacter[pos + i]);
//				Debug.Log ("end addrange" + list.Count);
			}
			int hitness = bal.getHitness (useSkill);
//			Debug.Log ("end hitness");
			List<BattleableBase> targets = bal.decideTarget (list);
//			Debug.Log ("end targets");
			foreach(BattleableBase target in targets){
				PassiveSkill reaction = target.decidePassiveSkill ();
//				Debug.Log ("end passive");
				reaction.use (target);
//				Debug.Log ("end passive use");
//				Debug.Log ("end dodge");
				if (hitness > target.getDodgeNess()) {
//					Debug.Log ("end suc");
					target.dammage (bal.battleAction (useSkill), useSkill.getSkillType ());
				}
//				Debug.Log ("end all");
			}
			return bal.getDelay (useSkill);
		}
			
		//現在位置から移動します
		private float moveCommand(BattleableBase bal,ref FiealdPosition pos){
//			Debug.Log ("moveCommand" + pos);
			int moveness = bal.move ();
			Debug.Log ("end move");
			int movenessMax = Enum.GetNames (typeof(FiealdPosition)).Length - (int)pos;
			Debug.Log ("end getnames");
			int movenessMin = -1 * (int)pos;
			Debug.Log ("end *-1");
			if (movenessMax < moveness|| moveness < movenessMin) {
				throw new ArgumentException ("不正なmovenessです。");
			}
			FiealdPosition beforePos = pos;
			Debug.Log ("end beforePos");
			if (removeBalFromJoinedCharacter(bal,pos)) {
				Debug.Log ("end if");
				pos = pos + moveness;
				Debug.Log ("end add math");
				joinedCharacter [pos].Add (bal);
				Debug.Log ("end joined");
			} else {
				throw new Exception ("balオブジェクトの情報が不正です");
			}

			Debug.Log ("before :" + beforePos + "after :" + pos);

			bal.syncronizePositioin (field.getNextPosition(beforePos,pos));
			return 0.5f;
		}

		//対象は戦闘から離脱します
		private float escapeCommand(BattleableBase bal,FiealdPosition pos){
			if(!removeBalFromJoinedCharacter (bal, pos))
				throw new Exception ("balオブジェクトの情報が不正です");
			bal.setIsBattling (false);
			return 0;
		}

		//渡された位置にある渡されたbalオブジェクトをjoinedCharacterディクショナリから削除します
		private bool removeBalFromJoinedCharacter(BattleableBase bal,FiealdPosition pos){
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
		private float decideCommand(BattleableBase bal,ref FiealdPosition pos){
			Debug.Log ("DecideCommand "  + pos);
			switch (bal.decideCommand ()) {
				case BattleCommand.ACTION:
					return actionCommand (bal, pos);

				case BattleCommand.MOVE:
					return moveCommand (bal, ref pos);

				case BattleCommand.ESCAPE:
					return escapeCommand (bal, pos);
			}
			throw new Exception ("invit 引数");
		}

		public int sumArea(FiealdPosition pos){
			return joinedCharacter [pos].Count;
		}
		public int sumAll(){
			int returnValue = 0;
			foreach(FiealdPosition pos in joinedCharacter.Keys){
				returnValue += joinedCharacter [pos].Count;
			}
			return returnValue;
		}
	}

	//戦闘フィールドでの状態を表します。ZEROを中心としてPL側がマイナス、NPC側がプラスです。
	public enum FiealdPosition{MTHREE,MTWO,MONE,ZERO,PONE,PTWO,PTHREE};

	//戦闘コマンドを表します。ACTION:攻撃・スキルorアイテムの使用 MOVE:戦闘エリアの移動 RUN:逃走
	public enum BattleCommand{ACTION,MOVE,ESCAPE};
}
