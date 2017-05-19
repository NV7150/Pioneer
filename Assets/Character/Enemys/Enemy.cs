using item;

namespace character{
	public abstract class Enemy : BattleableBase{
	    //エンカウントし、戦闘に突入します
	    public abstract void encount();

	    //このEnemyが与える経験値を取得します
	    public abstract int getGiveExp();

	    //このEnemyのドロップアイテムを取得します。ない場合はnullを返します
	    public abstract Item getDrop();

		public Enemy(BattleableBaseBuilder builder) : base(builder){
		}
	}
}
