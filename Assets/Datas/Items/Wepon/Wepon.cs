using System;

using character;
using battleSystem;


namespace item{
	public class  Wepon :  IItem{

		//攻撃力を取得します
		public int getAttack(){
			throw new NotSupportedException ();
		}

		//武器の射程を取得します
		public int getRange(){
			throw new NotSupportedException ();
		}

		//武器の種別を取得します
		public WeponType getWeponType(){
			throw new NotSupportedException ();
		}

		//武器が装備可能かを確認します
		public bool canEquip(IPlayable user){
			throw new NotSupportedException ();
		}

		//装備条件の説明を返します
		public string getNeedAbilityDescription(){
			throw new NotSupportedException ();
		}

		//アイテム名を取得します
		public string getName(){
			throw new NotSupportedException ();
		}

		//アイテム説明を取得します
		public string getDescription(){
			throw new NotSupportedException ();
		}

		//アイテムの重量を表します
		public int getMass(){
			throw new NotSupportedException ();
		}

		//アイテムの基本価格を表します
		public int getItemValue(){
			throw new NotSupportedException ();
		}

		//武器の基本ディレイ（攻撃後の待ち時間）を取得します
		public float getDelay(){
			throw new NotSupportedException ();
		}

		public void use (IPlayable use) {
			throw new NotImplementedException ();
		}
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