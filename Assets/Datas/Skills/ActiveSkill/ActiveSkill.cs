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
			ID,
			ATK,
			HEAL,
			MOVE,
			RANGE,
			HIT;

		[SerializeField]
		private readonly bool
			IS_FRIENDLY;

		[SerializeField]
		private readonly float
			DELAY;

		[SerializeField]
		private readonly string
			NAME,
			DESCRIPTION;

		private readonly ActiveSkillType TYPE;

<<<<<<< HEAD
		//このスキルのTYPEがActionだった時の詳細な種別を表します
		private readonly ActionType ACT_TYPE;

		//このスキルの攻撃の属性を表します
		private readonly AttackSkillAttribute ATTRIBUTE;

		//このスキルの回復の属性を表します
		private readonly HealSkillAttribute HEAL_ATTRIBUTE;
=======
		private readonly ActType ACT_TYPE;

		private readonly SkillAttribute ATTRIBUTE;

		private readonly HealAttribute HEAL_ATTRIBUTE;
>>>>>>> parent of cfdbb9b... コードの整理・ファイル構造の変更・いらない部分の削除などを行いました。

		private readonly Ability USE_ABILITY;

		private readonly Extent EXTENT;

		public ActiveSkill(string[] datas){
			this.ID = int.Parse (datas[0]);
			this.NAME = datas [1];
			this.ATK = int.Parse (datas[2]);
			this.HEAL = int.Parse (datas[3]);
			this.MOVE = int.Parse (datas[4]);
			this.RANGE = int.Parse (datas[5]);
			this.HIT = int.Parse (datas[6]);
			this.DELAY = float.Parse (datas [7]);
			this.IS_FRIENDLY = ( 0 == int.Parse(datas [8]));
			this.DESCRIPTION = datas [9];
			this.ATTRIBUTE = (AttackSkillAttribute)Enum.Parse (typeof(AttackSkillAttribute),datas[10]);
			this.HEAL_ATTRIBUTE = (HealSkillAttribute)Enum.Parse (typeof(HealSkillAttribute),datas[11]);
			this.TYPE = (ActiveSkillType)Enum.Parse (typeof(ActiveSkillType),datas[12]);
			this.ACT_TYPE = (ActionType)Enum.Parse (typeof(ActionType),datas[13]);
			this.USE_ABILITY = (Ability)Enum.Parse (typeof(Ability),datas[14]);
			this.EXTENT = (Extent)Enum.Parse (typeof(Extent),datas[15]);


		}

		public void use(IBattleable bal){
			switch(TYPE){
				case ActiveSkillType.ACTION:
					action (bal);
					break;
				case ActiveSkillType.MOVE:
					move (bal);
					break;
				case ActiveSkillType.ACTION_AND_MOVE:
					action (bal);
					move (bal);
					break;
			}
		}

		private void action(IBattleable bal){
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
					attack (bal);
					break;
				case ActType.HEAL:
					heal (bal);
					break;
				case ActType.BOTH:
					attack (bal);
					heal (bal);
>>>>>>> parent of cfdbb9b... コードの整理・ファイル構造の変更・いらない部分の削除などを行いました。
					break;
			}
		}

		private void attack(IBattleable bal){
			List<IBattleable> targets = BattleManager.getInstance ().getTaskFromUniqueId (bal.getUniqueId ()).getTargets ();

			if (targets.Count <= 0)
				throw new InvalidOperationException ("invlid battleTask operation");

			bal.getContainer().StartCoroutine(BattleManager.getInstance ().attackCommand (bal,targets,this));
		}

		private void heal(IBattleable bal){
			BattleManager.getInstance ().healCommand (bal,RANGE,HEAL,HEAL_ATTRIBUTE,USE_ABILITY);
		}

		private void move(IBattleable bal){
			BattleManager.getInstance ().moveCommand (bal, MOVE);
		}

		public int getId() {
			return ID;
		}

		public string getName(){
			return NAME;
		}

		public string getDescription(){
			return DESCRIPTION;
		}

		public bool getIsFriendly(){
			return IS_FRIENDLY;
		}

		public int getAtk() {
			return ATK;
		}

		public int getHeal() {
			return HEAL;
		}

		public int getMove() {
			return MOVE;
		}

		public int getRange() {
			return RANGE;
		}

		public int getHit() {
			return HIT;
		}

		public float getDelay() {
			return DELAY;
		}

		public ActiveSkillType getActiveSkillType() {
			return TYPE;
		}

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
		public ActType getActType() {
			return ACT_TYPE;
		}

		public SkillAttribute getAttribute() {
			return ATTRIBUTE;
		}

		public HealAttribute getHealAttribute() {
>>>>>>> parent of cfdbb9b... コードの整理・ファイル構造の変更・いらない部分の削除などを行いました。
			return HEAL_ATTRIBUTE;
		}

		public Ability getUseAbility() {
			return USE_ABILITY;
		}

		public Extent getExtent() {
			return EXTENT;
		}

		public override string ToString () {
			return "Activeskill " + NAME;
		}
	}
}
