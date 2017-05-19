using character;

namespace skill {
    public interface Skill {
        //スキル名を取得します
        string getName();

        //スキル説明を取得します
        string getDescription();

		//スキルを使用します
		int use(BattleableBase user);
    }
}