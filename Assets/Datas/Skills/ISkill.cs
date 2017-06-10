using character;

namespace skill {
    public interface ISkill {
        //スキル名を取得します
        string getName();

        //スキル説明を取得します
        string getDescription();

		//スキルIDを取得します
		int getId();

		//スキルを使用し、ディレイを返します
		float use(IBattleable user);
    }
}