using System.Collections.Generic;

using item;
using parameter;

namespace character{
	public abstract class Enemy : BattleableBase{
	    //エンカウントし、戦闘に突入します
	    public abstract void encount();

	    //このEnemyが与える経験値を取得します
	    public abstract int getGiveExp();

	    //このEnemyのドロップアイテムを取得します。ない場合はnullを返します
	    public abstract IItem getDrop();

		public Enemy(Dictionary<Ability,int> parameters) : base(parameters){
		}

		#region implemented abstract members of BattleableBase

		public override int getDef () {
			throw new System.NotImplementedException ();
		}

		public override float getDelay (skill.IActiveSkill skill) {
			throw new System.NotImplementedException ();
		}

		public override int move () {
			throw new System.NotImplementedException ();
		}

		public override void syncronizePositioin (UnityEngine.Vector3 vector) {
			throw new System.NotImplementedException ();
		}

		public override battleSystem.BattleCommand decideCommand () {
			throw new System.NotImplementedException ();
		}

		public override skill.IActiveSkill decideSkill () {
			throw new System.NotImplementedException ();
		}

		public override int getRange (skill.IActiveSkill skill) {
			throw new System.NotImplementedException ();
		}

		public override System.Collections.Generic.List<BattleableBase> decideTarget (System.Collections.Generic.List<BattleableBase> bals) {
			throw new System.NotImplementedException ();
		}

		public override int getHitness (skill.IActiveSkill skill) {
			throw new System.NotImplementedException ();
		}

		public override int battleAction (skill.IActiveSkill skill) {
			throw new System.NotImplementedException ();
		}

		public override skill.IPassiveSkill decidePassiveSkill () {
			throw new System.NotImplementedException ();
		}

		public override int getDodgeNess () {
			throw new System.NotImplementedException ();
		}

		public override UnityEngine.GameObject getModel () {
			throw new System.NotImplementedException ();
		}

		public override void act () {
			throw new System.NotImplementedException ();
		}

		public override void death () {
			throw new System.NotImplementedException ();
		}

		#endregion
	}
}
