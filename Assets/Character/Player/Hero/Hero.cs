
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using item;
using skill;
using parameter;
using battleSystem;

namespace character{
	public class Hero :Playable {

		//このキャラクターのHPを表します
		private int hp;
		//このキャラクターのMPを表します
		private int mp;
		//ゲームスコアを表します
		private int score;
		//キャラクターのレベルを表します
		private int level;
		//キャタクターの経験値を表します
		private int exp;
		//キャラクターの白兵戦闘能力を表します(melee fighting)
		private int mft;
		//このキャラクターの遠戦闘能力を表します(far fighting)
		private int fft;
		//このキャラクターの魔力を表します(magic power)
		private int mgp;
		//このキャラクターの敏捷性を表します(agility)
		private int agi;
		//このキャラクターの体力を表します(physical)
		private int phy;
		//このキャラクターの器用さを表します(dexerity)
		private int dex;
		//このキャラクターの話術を表します(speech)
		private int spc;
		//このキャラクターの所持金(metal)を表します
		private int mt;
		//使命達成用のflugリストです。
		private FlugList flugs;
		//このキャラクターの職業を表します
		private Job job;
		//このキャラクターの個性を表します
		private Identity identity;
		//このキャラクターの使命を表します
		private Mission mission;
		//このキャラクターが装備中の武器を表します
		private Wepon wepon;
		//このキャラクターのインベントリを表します
		private Dictionary<string,ItemStack> inventry = new Dictionary<string,ItemStack>();
		//このキャラクターの装備状態を表します
		private Armor armor;
		//このキャラクターが使用できる能動スキルを表します
		private Dictionary<string,ActiveSkill> activeSkills = new Dictionary<string,ActiveSkill>();
		//このキャラクターが使用できる受動スキルを表します
		private Dictionary<string,PassiveSkill> passiveSkills = new Dictionary<string,PassiveSkill>();
		//このキャラクターがバトルに突入しているかを示します
		//テスト用に仮にtrueにします
		private bool isBattling = true;
		//このキャラクターの外面(container)を表します
		private Container container;

		void Update(){
		}

		/*
		 * Heroのコンストラクタです
		 * Job job Heroの職業を設定します
		 * Identity identity Heroの個性を設定します
		 * Mission mission Heroの使命を設定します
		 */
		public Hero(Job job,Identity identity,Mission mission,Container container){
			Debug.Log ("called constracter job:" + job);
			this.job = job;
			this.identity = identity;
			this.mission = mission;
			flugs = new FlugList (mt, level);

			hp = 100;

			this.container = container;

			this.mft = job.defaultSetting()[Ability.MFT];
			this.fft = job.defaultSetting()[Ability.FFT];
			this.mgp = job.defaultSetting()[Ability.MGP];
			this.phy = job.defaultSetting()[Ability.PHY];
			this.agi = job.defaultSetting()[Ability.AGI];
			this.dex = job.defaultSetting()[Ability.DEX];
			this.spc = job.defaultSetting()[Ability.SPC];

			this.mft = 100;
			Debug.Log ("contracted");
		}

		//HPを取得します
		public int getHp(){
			return hp;
		}

		//MPを取得します
		public int getMp(){
			return mp;
		}

		//scoreを取得します
		public int getScore(){
			return score;
		}

		//mft(melee fighting)を取得します
		public int getMft(){
			return mft;
		}

		//fft(far fighting)を取得します
		public int getFft(){
			return fft;
		}

		//mgp(magic power)を取得します
		public int getMgp(){
			return mgp;
		}

		//agi(agility)を取得します
		public int getAgi(){
			return agi;
		}

		//phy(phygical)を取得します
		public int getPhy(){
			return phy;
		}

		//dex(dexerity)を取得します
		public int getDex(){
			return dex;
		}

		//spc(speech)を取得します
		public int getSpc(){
			return spc;
		}

		/*scoreを増減します
		 * int value scoreの増減の値。増やす時には正、減らす時には負の値を設定してください。
		*/
		public void fluctateScore(int value){
			score += value;
			if (score < 0)
				score = 0;
		}

		//levelを上昇させ、能力値を成長させます。
		void levelUp(){
			level += 1;
			//能力値の成長処理を追加
		}

		//このキャラクターのJobを取得します
		public Job getJob(){
			return job;
		}

		//このキャラクターのIdentityを取得します
		public Identity getIdentity(){
			return identity;
		}

		//このキャラクターのMissionを取得します
		public Mission getMission(){
			return mission;
		}

		/* Aromor属性のものを指定された部位に装備します
		 * Aromor aromor 装備するAromor
		 * Part part 装備する部位
		 */
		public void equipArmor(Armor equipArmor){
			if (armor != null)
				addItem (armor);
			this.armor = equipArmor;
		}

		/* Wepon属性のものを装備します
		* Wepon wepon 装備するwepon
		*/
		public void equipWepon(Wepon equipWepon){
			if (wepon != null)
				addItem (wepon);
			this.wepon = equipWepon;
		}

		public int getWeponRange(){
			return wepon.getRange ();
		}

		/* 指定されたItemを使用します
		* String itemName 使用するItemの名称
		*/
		public void useItem(string itemName){
			if (!inventry.ContainsKey (itemName))
				throw new System.Exception ("引数違いますよ");
			Item useItem = inventry[itemName].take();
			useItem.use(this);
			inventry.Remove(itemName);
		}

		//inventry内のアイテムの名称をList<String>として返します
		public List<string> getItems(){
			return new List<string>(this.inventry.Keys);
		}

		//inventryにアイテムを追加します
		public void addItem(Item item){
			if (!inventry.ContainsKey (item.getName())) 
				inventry.Add (item.getName(),new ItemStack());
			inventry [item.getName()].add (item);
		}

		/* 指定されたSkillを追加します
		* Skill skill 追加するSkill
		*/
		public void addActiveSkill(ActiveSkill activeSkill){
			Debug.Log ("プレイヤ" + activeSkill.ToString());
//			activeSkills.Add(activeSkill.getName(),activeSkill);
		}

		//スキル名の一覧をStringのListで返します
		public List<string> getActiveSkills(){
			return new List<string>(this.activeSkills.Keys);
		}

		public void gameClear (){
			throw new System.NotImplementedException ();
		}

		public void gameOver (){
			throw new System.NotImplementedException ();
		}

		public void dammage (int dammage, SkillType type){
			hp -= dammage;
			if (hp < 0)
				hp = 0;
		}

		public int getDef (){
			return phy + armor.getDef ();
		}

		public bool getIsBattling (){
			return isBattling;
		}

		public void setIsBattling (bool boolean){
			isBattling = boolean;
		}

		public ActiveSkill decideSkill (){
			//かり
			return new NormalAttack();
		}

		public List<Battleable> target (List<Battleable> bals){
			//かり
			return bals;
		}

		public int getHitness (ActiveSkill skill){
			return skill.getSuccessRate (this);
		}

		public int buttleAction (ActiveSkill skill){
			Debug.Log ("called battleAction");
			return skill.use (this) + Random.Range (0, 11);
		}

		public BattleCommand decideCommand (){
			//かり
			return BattleCommand.MOVE;
		}

		public PassiveSkill decidePassive (){
			//かり
			return new NoGuard();
		}

		public int dodgeSuccessed (){
			return agi;
		}

		public int getRange (ActiveSkill skill){
			return skill.getRange (this,0);
		}

		public void setDefBonus (int bonus){
			throw new System.NotImplementedException ();
		}

		public void setDodBonus (int bonus){
			throw new System.NotImplementedException ();
		}

		public void setDoCounter (bool flag){
			throw new System.NotImplementedException ();
		}

		public void resetBonus (){
			throw new System.NotImplementedException ();
		}

		public int move (){
			return agi / 5 + 1;
		}

		public Wepon getWepon(){
			return wepon;
		}
		void Battleable.divMp (int value) {
			mp -= value;
		}
		void Battleable.heal (int value) {
			hp += value;
		}
		void Battleable.healMp (int value) {
			hp += value;
		}
		void Battleable.setAtkBonus (int bonus) {
			throw new System.NotImplementedException ();
		}

		AttackType Battleable.getAttackType () {
			//かり
			return AttackType.MELEE;
		}
		void Character.death () {
			throw new System.NotImplementedException ();
		}

		float Battleable.getDelay (ActiveSkill skill) { 
			return skill.getDelay (this,wepon.getRange());
		}

		public void encount(){
			container.getExcecutor().StartCoroutine (BattleManager.getInstance().joinBattle(this,FiealdPosition.MTHREE));
		}

		public void syncronizePositioin (Vector3 vector) {
			container.getModel ().transform.position = vector;
		}
			
		GameObject Character.getModel () {
			return container.getModel ();
		}

		void Character.act () {
			//処理を記入
		}

		public int getLevel () {
			return level;
		}
	}
}