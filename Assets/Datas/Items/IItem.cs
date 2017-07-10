using Character;

namespace Item {
    public interface IItem {
        //アイテム名を取得します
        string getName();

        //アイテム説明を取得します
        string getDescription();

        //アイテムを使用します
		void use(IPlayable user);

        //アイテムの重量を表します
        int getMass();

		//アイテムの基本価格を表します
		int getItemValue();
    }
}
