using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Parameter;

using BattleAbility = Parameter.CharacterParameters.BattleAbility;
using FriendlyAbility = Parameter.CharacterParameters.FriendlyAbility;

namespace CharaMake{
    public class CharaMakeJobView : MonoBehaviour {
        /// <summary> 名前を表示させるテキスト </summary>
        public Text nameText;

        /// <summary> mftを表示させるテキスト </summary>
        public Text mftText;
        /// <summary> fftを表示させるテキスト </summary>
        public Text fftText;
        /// <summary> mgpを表示させるテキスト </summary>
        public Text mgpText;
        /// <summary> phyを表示させるテキスト </summary>
        public Text phyText;
        /// <summary> agiを表示させるテキスト </summary>
        public Text agiText;
        /// <summary> spcを表示させるテキスト </summary>
        public Text spcText;
        /// <summary> dexを表示させるテキスト </summary>
        public Text dexText;


        /// <summary> 説明文を表示させるテキスト </summary>
        public Text descriptionText;
        /// <summary> フレーバーテキストを表示させるテキスト </summary>
        public Text flavorText;

        /// <summary>
        /// 対象の情報を表示させます
        /// </summary>
        /// <param name="job">職業</param>
        public void printText(Job job) {
            nameText.text = job.getName();

            var battleAbilities = job.defaultSettingBattleAbility();
            mftText.text = "" + battleAbilities[BattleAbility.MFT];
            fftText.text = "" + battleAbilities[BattleAbility.FFT];
            mgpText.text = "" + battleAbilities[BattleAbility.MGP];
            phyText.text = "" + battleAbilities[BattleAbility.PHY];
            agiText.text = "" + battleAbilities[BattleAbility.AGI];

            var friendlyAbilities = job.defaultSettingFriendlyAbility();
            spcText.text = "" + friendlyAbilities[FriendlyAbility.SPC];
            dexText.text = "" + friendlyAbilities[FriendlyAbility.DEX];

            descriptionText.text = job.getDescription();
            flavorText.text = job.getFlavorText();
        }
    }
}
