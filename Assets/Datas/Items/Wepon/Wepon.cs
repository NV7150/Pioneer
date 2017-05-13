using character;
using battleSystem;

namespace item{
	public abstract class  Wepon :  Item{
		//攻撃力を取得します
		public abstract int getAttack();

		//武器の射程を取得します
		public abstract int getRange();

		//武器の種別を取得します
		public abstract WeponType getWeponType();


		//武器が装備可能かを確認します
		protected abstract bool canEquip(Playable user);

		//装備条件を文章として返します
		public abstract string getNeedAbility();

		//武器を装備します
		public void use(Playable user){
			if (canEquip (user)) {
				user.equipWepon (this);
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

		//武器の基本ディレイ（攻撃後の待ち時間）を取得します
		public abstract float getDelay();
	}

	public enum WeponType{
		SWORD,
		BOW,
		SPEAR,
		BLUNT,
		AX,
		GUN
	}
}