using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using Character;
using BattleSystem;
using Parameter;

using ActiveSkillType = Skill.ActiveSkillParameters.ActiveSkillType;
using ActionType = Skill.ActiveSkillParameters.ActionType;
using AttackSkillAttribute = Skill.ActiveSkillParameters.AttackSkillAttribute;
using HealSkillAttribute = Skill.ActiveSkillParameters.HealSkillAttribute;
using Extent = Skill.ActiveSkillParameters.Extent;
using Ability = Parameter.CharacterParameters.Ability;

namespace Skill{
	[System.SerializableAttribute]
	public class ActiveSkill : ISkill{
		[SerializeField]
		private readonly int
			//このスキルのIDです
			ID,
			//このスキルの攻撃力です
			ATK,
			//このスキルの回復力です
			HEAL,
			//このスキルの移動力です
			MOVE,
			//このスキルの射程です
			RANGE,
			//このスキルの命中値です
			HIT,
			//このスキルのディレイ値です
			DELAY;

		[SerializeField]
		private readonly bool
			//このスキルが友好的かどうかを表します
			IS_FRIENDLY;

		[SerializeField]
		private readonly string
			//このスキルの名前です
			NAME,
			//このスキルの説明です
			DESCRIPTION;

		//このスキルの種類を返します
		private readonly ActiveSkillType TYPE;

<<<<<<< HEAD
<<<<<<< HEAD
		//このスキルのTYPEがActionだった時の詳細な種別を表します
		private readonly ActionType ACT_TYPE;

		//このスキルの攻撃の属性を表します
		private readonly AttackSkillAttribute ATTRIBUTE;

		//このスキルの回復の属性を表します
		private readonly HealSkillAttribute HEAL_ATTRIBUTE;
=======
=======
		//このスキルのTYPEがActionだった時の詳細な種別を表します
>>>>>>> cfdbb9b19b7aff48b2537cc983d1f41f037f910b
		private readonly ActType ACT_TYPE;

		//このスキルの攻撃の属性を表します
		private readonly SkillAttribute ATTRIBUTE;

		//このスキルの回復の属性を表します
		private readonly HealAttribute HEAL_ATTRIBUTE;
>>>>>>> parent of cfdbb9b... コードの整理・ファイル構造の変更・いらない部分の削除などを行いました。

		//このスキルが行う判定で使用する能力値です
		private readonly Ability USE_ABILITY;

		//このスキルの効果範囲です
		private readonly Extent EXTENT;

		//csvによるstring配列から初期化します
		public ActiveSkill(string[] datas){
			this.ID = int.Parse (datas[0]);
			this.NAME = datas [1];
			this.ATK = int.Parse (datas[2]);
			this.HEAL = int.Parse (datas[3]);
			this.MOVE = int.Parse (datas[4]);
			this.RANGE = int.Parse (datas[5]);
			this.HIT = int.Parse (datas[6]);
			this.DELAY = int.Parse (datas [7]);
			this.IS_FRIENDLY = ( 0 == int.Parse(datas [8]));
			this.DESCRIPTION = datas [9];
			this.ATTRIBUTE = (AttackSkillAttribute)Enum.Parse (typeof(AttackSkillAttribute),datas[10]);
			this.HEAL_ATTRIBUTE = (HealSkillAttribute)Enum.Parse (typeof(HealSkillAttribute),datas[11]);
			this.TYPE = (ActiveSkillType)Enum.Parse (typeof(ActiveSkillType),datas[12]);
			this.ACT_TYPE = (ActionType)Enum.Parse (typeof(ActionType),datas[13]);
			this.USE_ABILITY = (Ability)Enum.Parse (typeof(Ability),datas[14]);
			this.EXTENT = (Extent)Enum.Parse (typeof(Extent),datas[15]);


		}

		public void use(){
			
		}

		//与えられたactionerとtaskからActiveSkillを使用します
		public void action(IBattleable actioner,BattleTask task){
			switch(TYPE){
				case ActiveSkillType.ACTION:
					actionTrafic(actioner,task.getTargets());
					break;
				case ActiveSkillType.MOVE:
					move (actioner,task.getMove());
					break;
				case ActiveSkillType.ACTION_AND_MOVE:
					move (actioner,task.getMove());
					actionTrafic (actioner,task.getTargets());
					break;
			}
		}

		//actionスキルの種別に応じて適切なメソッドを呼び出します
		private void actionTrafic(IBattleable actioner,List<IBattleable> targets){
			switch(ACT_TYPE){
<<<<<<< HEAD
				case ActionType.ATTACK:
					attack (actioner,targets);
					break;
				case ActionType.HEAL:
					heal (actioner,targets);
					break;
				case ActionType.BOTH:
					attack (actioner,targets);
					heal (actioner,targets);
=======
				case ActType.ATTACK:
					attack (actioner,targets);
					break;
				case ActType.HEAL:
					heal (actioner,targets);
					break;
				case ActType.BOTH:
<<<<<<< HEAD
					attack (bal);
					heal (bal);
>>>>>>> parent of cfdbb9b... コードの整理・ファイル構造の変更・いらない部分の削除などを行いました。
=======
					attack (actioner,targets);
					heal (actioner,targets);
>>>>>>> cfdbb9b19b7aff48b2537cc983d1f41f037f910b
					break;
			}
		}

		//攻撃を行います
		private void attack(IBattleable bal,List<IBattleable> targets){
			if (targets.Count <= 0)
				throw new InvalidOperationException ("invlid battleTask operation");

			BattleManager.getInstance ().attackCommand (bal,targets,this);
		}

		//回復を行います
		private void heal(IBattleable bal,List<IBattleable> targets){
			BattleManager.getInstance ().healCommand (bal,RANGE,HEAL,HEAL_ATTRIBUTE,USE_ABILITY);
			throw new NotImplementedException ();
		}

		//移動を行います
		private void move(IBattleable bal,int moveness){
			//値が適切か判断
			FieldPosition nowPos = BattleManager.getInstance ().searchCharacter (bal);
			int moveAmountMax = Enum.GetNames (typeof(FieldPosition)).Length - (int)nowPos;
			int moveAmountMin = -1 * (int)nowPos;
			if (moveAmountMax <= MOVE||moveAmountMin >= MOVE)
				throw new ArgumentException ("invlit moveNess");
			
			BattleManager.getInstance ().moveCommand (bal,moveness);
		}

		//IDを取得します
		public int getId() {
			return ID;
		}

		//名前を取得します
		public string getName(){
			return NAME;
		}

		//説明を取得します
		public string getDescription(){
			return DESCRIPTION;
		}

		//友好的なスキルかを表すboolを取得します
		public bool getIsFriendly(){
			return IS_FRIENDLY;
		}

		//攻撃力を取得します
		public int getAtk() {
			return ATK;
		}

		//回復力を取得します
		public int getHeal() {
			return HEAL;
		}

		//移動力を取得します
		public int getMove() {
			return MOVE;
		}

		//射程を取得します
		public int getRange() {
			return RANGE;
		}
			
		//命中力を取得します
		public int getHit() {
			return HIT;
		}

		//ディレイ値を取得します
		public int getDelay() {
			return DELAY;
		}

		//スキルの種別を取得します
		public ActiveSkillType getActiveSkillType() {
			return TYPE;
		}

<<<<<<< HEAD
<<<<<<< HEAD
		//actionの詳細な種別を取得します
		public ActionType getActType() {
			return ACT_TYPE;
		}

		//スキルの攻撃属性を取得します
		public AttackSkillAttribute getAttribute() {
			return ATTRIBUTE;
		}

		//スキルの回復属性を取得します
		public HealSkillAttribute getHealAttribute() {
=======
=======
		//actionの詳細な種別を取得します
>>>>>>> cfdbb9b19b7aff48b2537cc983d1f41f037f910b
		public ActType getActType() {
			return ACT_TYPE;
		}

		//スキルの攻撃属性を取得します
		public SkillAttribute getAttribute() {
			return ATTRIBUTE;
		}

		//スキルの回復属性を取得します
		public HealAttribute getHealAttribute() {
>>>>>>> parent of cfdbb9b... コードの整理・ファイル構造の変更・いらない部分の削除などを行いました。
			return HEAL_ATTRIBUTE;
		}

		//判定に使用する能力値を取得します
		public Ability getUseAbility() {
			return USE_ABILITY;
		}

		//スキルの効果範囲を取得します
		public Extent getExtent() {
			return EXTENT;
		}

		public override string ToString () {
			return "Activeskill " + NAME;
		}
	}
}
