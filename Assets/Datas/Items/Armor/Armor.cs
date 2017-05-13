using character;

namespace item {
	public abstract class Armor : Item{
		//物理防御を返します
		public abstract int getDef();

		//回避修正を返します
		public abstract int getDodgeBonus();

		//武器が装備可能かを確認します
		protected abstract bool canEquip(Playable user);

		//装備条件を文章として返します
		public abstract string getNeedAbility ();

		//武器を装備します
		public void use(Playable user){
			if (canEquip (user)) {
				user.equipArmor (this);
			}
		}

		//アイテム名を取得します
		public abstract string getName();

		//アイテム説明を取得します
		public abstract string getDescription();

		//アイテムの重量を表します
		public abstract int getMass();

		//アイテムの基本価格を表します
		public abstract int getItemValue();
	}
}

