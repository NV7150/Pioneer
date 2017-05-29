using System;

using character;

namespace item {
	public abstract class Armor : IItem{
		//物理防御を返します
		public int getDef(){
			throw new NotSupportedException ();
		}

		//回避修正を返します
		public int getDodgeBonus(){
			throw new NotSupportedException ();
		}

		//武器が装備可能かを確認します
		public bool canEquip(IPlayable user){
			throw new NotSupportedException ();
		}

		//装備条件を文章として返します
		public string getNeedAbility (){
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

		public void use (IPlayable use) {
			throw new NotImplementedException ();
		}
	}
}

