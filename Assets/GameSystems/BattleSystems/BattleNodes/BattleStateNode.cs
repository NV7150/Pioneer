﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using UnityEngine.UI;

using Character;

namespace BattleSystem {
    public class BattleStateNode : MonoBehaviour {
        IBattleable user;
        public Image hpBar;
        public Image mpBar;
        public Text hpValue;
        public Text mpValue;
        public Text text;
        public Image progressRectangle;

        void Start() {
        }

        // Update is called once per frame
        void Update() {
            if (user != null) {
                hpBar.fillAmount = (float)user.getHp() / (float)user.getMaxHp();
                hpValue.text = user.getHp() + " / " + user.getMaxHp();
                mpBar.fillAmount = (float)user.getMp() / (float)user.getMaxMp();
                mpValue.text = user.getMp() + " / " + user.getMaxMp();
            }

        }

        public void setUser(IBattleable user) {
			this.user = user;
            text.text = user.getName();
        }

		public void advanceProgress(float val) {
			if (val <= 0)
				throw new ArgumentException("invalid value");

			progressRectangle.fillAmount = (progressRectangle.fillAmount < 1.0f) ? progressRectangle.fillAmount + val : progressRectangle.fillAmount;
			cheakColor();
		}

		public void resetProgress() {
			progressRectangle.fillAmount = 0f;
		}

		private void cheakColor() {
			if (progressRectangle.fillAmount >= 1.0f) {
				progressRectangle.color = new Color(0f, 230f, 0f);
			} else if (progressRectangle.fillAmount < 1.0f) {
                progressRectangle.color = Color.grey;
			}
		}
    }
}
