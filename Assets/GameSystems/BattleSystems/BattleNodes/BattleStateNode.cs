﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
//using static UnityEngine.UI.Image.Type;
using Character;

namespace BattleSystem {
    public class BattleStateNode : MonoBehaviour {
        IBattleable user;
        public Image hpBar;
        public Image mpBar;
        public Text hpValue;
        public Text mpValue;
        public Text name;

        void Start() {
            name.text = user.getName();
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
        }
    }
}
