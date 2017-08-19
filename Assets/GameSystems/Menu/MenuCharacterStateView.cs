using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Character;

namespace Menus {
    public class MenuCharacterStateView : MonoBehaviour {
        /// <summary> 名前を表示させるテキスト </summary>
        public Text nameText;

        /// <summary> パラメータを表示させるテキスト群 </summary>
        public MenuCharacterParameterView parameterView;

        /// <summary> 武器を表示させるテキスト </summary>
        public Text weaponText;
        /// <summary> 防具を表示させるテキスト </summary>
        public Text armorText;

        /// <summary> HPを表すバー </summary>
        public Image hpBar;
        /// <summary> HPを表示させるテキスト </summary>
        public Text hpValue;
        /// <summary> MPを表すバー </summary>
        public Image mpBar;
        /// <summary> MPを表示させるテキスト </summary>
        public Text mpValue;

        /// <summary>
        /// キャラクターのパラメータを表示せます
        /// </summary>
        /// <param name="character">表示させたいキャラクター</param>
        public void setCharacter(IPlayable character) {
            nameText.text = character.getName();

            parameterView.setAbilities(character.getLevel(), character.getBattleAbilities(), character.getFriendlyAbilities());

            //武器テキストまだ実装していないので注意

            //weponText.text = "E:" + character.getWepon().getName();
            //armorText.text = "E:" + character.getArmor().getName();

            hpBar.fillAmount = (float)character.getHp() / (float)character.getMaxHp();
            mpBar.fillAmount = (float)character.getMp() / (float)character.getMaxMp();
            hpValue.text = character.getHp() + "/" + character.getMaxHp();
            mpValue.text = character.getMp() + "/" + character.getMaxMp();
        }

        /// <summary>
        /// 終了が選択された時の処理
        /// </summary>
        public void finishchose() {
            //Destroy(gameObject);
        }
    }
}
