using System;
using parameter;
using System.Collections;
using System.Collections.Generic;

namespace character{
	//BattleableのBuilderです。
	public class BattleableBaseBuilder{
		//最大HPを表します。必ず0より大きい数になります。
		private int maxHp = 1;
		//最大MPを表します。必ず0より大きい数になります。
		private int maxMp = 1;
		//白兵戦闘力(melee fighting)を表します。必ず0以上です。
		private int mft;
		//遠戦闘力(far fighting)を表します。必ず0以上です。
		private int fft;
		//魔力(magic power)を表します。必ず0以上です。
		private int mgp;
		//敏捷性(agility)を表します。必ず0以上です。
		private int agi;
		//体力(phygical)を表します。必ず0以上です。
		private int phy;
		//レベルを表します。必ず0以上です。
		private int level;

		public BattleableBaseBuilder(Dictionary<Ability,int> parameters){
		}

		public int getMaxHp() {
			return maxHp;
		}
		public void setMaxHp(int maxHp) {
			if (!(0 < maxHp))
				throw new ArgumentException ("maxHp in builder is worng.");
			this.maxHp = maxHp;
		}
		public int getMaxMp() {
			return maxMp;
		}
		public void setMaxMp(int maxMp) {
			if (!(0 < maxMp))
				throw new ArgumentException ("maxMp in builder is worng");
			this.maxMp = maxMp;
		}
		public int getMft() {
			return mft;
		}
		public void setMft(int mft) {
			if (!(0 <= mft))
				throw new ArgumentException ("mft in builder is worng");
			this.mft = mft;
		}
		public int getFft() {
			return fft;
		}
		public void setFft(int fft) {
			if (!(0 <= fft))
				throw new ArgumentException ("fft in builder is worng");
			this.fft = fft;
		}
		public int getMgp() {
			return mgp;
		}
		public void setMgp(int mgp) {
			if (!(0 <= mgp))
				throw new ArgumentException ("mgp in builder is worng");
			this.mgp = mgp;
		}
		public int getAgi() {
			return agi;
		}
		public void setAgi(int agi) {
			if (!(0 <= agi))
				throw new ArgumentException ("agi in builder is worng");
			this.agi = agi;
		}
		public int getPhy() {
			return phy;
		}
		public void setPhy(int phy) {
			if (!(0 <= phy))
				throw new ArgumentException ("phy in builder is worng");
			this.phy = phy;
		}
		public int getLevel() {
			return level;
		}
		public void setLevel(int level) {
			if (!(0 <= level))
				throw new ArgumentException ("level in builder is worng");
			this.level = level;
		}
	}
}

