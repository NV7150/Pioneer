using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using Character;
using BattleSystem;
using Parameter;

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

		private readonly SkillType TYPE;

		private readonly ActType ACT_TYPE;

		private readonly SkillAttribute ATTRIBUTE;

		private readonly HealAttribute HEAL_ATTRIBUTE;

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
			this.ATTRIBUTE = (SkillAttribute)Enum.Parse (typeof(SkillAttribute),datas[10]);
			this.HEAL_ATTRIBUTE = (HealAttribute)Enum.Parse (typeof(HealAttribute),datas[11]);
			this.TYPE = (SkillType)Enum.Parse (typeof(SkillType),datas[12]);
			this.ACT_TYPE = (ActType)Enum.Parse (typeof(ActType),datas[13]);
			this.USE_ABILITY = (Ability)Enum.Parse (typeof(Ability),datas[14]);
			this.EXTENT = (Extent)Enum.Parse (typeof(Extent),datas[15]);
		}

		public void use(IBattleable bal){
			switch(TYPE){
				case SkillType.ACTION:
					action (bal);
					break;
				case SkillType.MOVE:
					move (bal);
					break;
				case SkillType.ACTION_AND_MOVE:
					action (bal);
					move (bal);
					break;
			}
		}

		private void action(IBattleable bal){
			switch(ACT_TYPE){
				case ActType.ATTACK:
					attack (bal);
					break;
				case ActType.HEAL:
					heal (bal);
					break;
				case ActType.BOTH:
					attack (bal);
					heal (bal);
					break;
			}
		}

		private void attack(IBattleable bal){
			BattleManager.getInstance ().attackCommand (bal,BattleManager.getInstance().getTaskFromUniqueId(bal.getUniqueId()).getTargets(),HIT,ATK,ATTRIBUTE,USE_ABILITY);
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

		public SkillType getType() {
			return TYPE;
		}

		public ActType getActType() {
			return ACT_TYPE;
		}

		public SkillAttribute getAttribute() {
			return ATTRIBUTE;
		}

		public HealAttribute getHealAttribute() {
			return HEAL_ATTRIBUTE;
		}

		public Ability getUseAbility() {
			return USE_ABILITY;
		}

		public Extent getExtent() {
			return EXTENT;
		}
	}
}
