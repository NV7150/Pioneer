﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using UnityEngine.UI;

using Character;

namespace BattleSystem {
    public class BattleStateNode : MonoBehaviour {
        /// <summary> 担当するキャラクター </summary>
        IBattleable user;
        /// <summary> userのHPを表示するHPバー </summary>
        public Image hpBar;
        /// <summary> userのMPを表示するMPバー </summary>
        public Image mpBar;
        /// <summary> userのHP数値を表示するTextオブジェクト </summary>
        public Text hpValue;
        /// <summary> userのMP数値を表示するTextオブジェクト </summary>
        public Text mpValue;
        /// <summary> 名前を表示するTextオブジェクト </summary>
        public Text nameText;
        /// <summary> ディレイの割合を表すImageオブジェクト </summary>
        public Image progressRectangle;

        // Update is called once per frame
        void Update() {
            if (user != null) {
                hpBar.fillAmount = (float)user.getHp() / (float)user.getMaxHp();
                hpValue.text = user.getHp() + " / " + user.getMaxHp();
                mpBar.fillAmount = (float)user.getMp() / (float)user.getMaxMp();
                mpValue.text = user.getMp() + " / " + user.getMaxMp();
            }

        }

        /// <summary>
        /// IBattleableキャラクターを設定します
        /// </summary>
        /// <param name="user">設定したいIBattleableキャラクター</param>
        public void setUser(IBattleable user) {
			this.user = user;
            nameText.text = user.getName();
        }

        /// <summary>
        /// ディレイの進行状況を更新します
        /// </summary>
        /// <param name="delay">ディレイの状況</param>
        public void advanceProgress(float delay) {
			if (delay <= 0)
				throw new ArgumentException("invalid value");

			progressRectangle.fillAmount = (progressRectangle.fillAmount < 1.0f) ? progressRectangle.fillAmount + delay : progressRectangle.fillAmount;
			cheakColor();
		}

        /// <summary>
        /// ディレイの進行状況をリセットします
        /// </summary>
		public void resetProgress() {
			progressRectangle.fillAmount = 0f;
		}

        /// <summary>
        /// ディレイの状況によってイメージの色を設定します
        /// </summary>
		private void cheakColor() {
			if (progressRectangle.fillAmount >= 1.0f) {
				progressRectangle.color = new Color(0f, 230f, 0f);
			} else if (progressRectangle.fillAmount < 1.0f) {
                progressRectangle.color = Color.grey;
			}
		}
    }
}
