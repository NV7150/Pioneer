using item;

namespace character{
	public interface Enemy : Battleable, NonPlayerCharacter{
	    //エンカウントし、戦闘に突入します
	    void encount();

	    //このEnemyが与える経験値を取得します
	    int getGiveExp();

	    //このEnemyのドロップアイテムを取得します。ない場合はnullを返します
	    Item getDrop();
	}
}
