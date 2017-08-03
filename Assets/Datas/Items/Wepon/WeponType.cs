using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using BattleAbility = Parameter.CharacterParameters.BattleAbility;

namespace Item {
    /// <summary>
    /// 武器の種別
    /// </summary>
    public enum WeponType {
        SWORD = 0,
        BOW = 1,
        SPEAR = 2,
        BLUNT = 3,
        AX = 4,
        GUN = 5
    }

    public static class WeponTypeHelper{
        public static BattleAbility getTypeAbility(WeponType type){
            switch(type){
                case WeponType.SWORD:
					return BattleAbility.MFT;
                case WeponType.SPEAR:
					return BattleAbility.MFT;
                case WeponType.AX:
					return BattleAbility.MFT;
                case WeponType.BLUNT:
					return BattleAbility.MFT;
                case WeponType.BOW:
                    return BattleAbility.FFT;
                case WeponType.GUN:
                    return BattleAbility.FFT;
            }
            throw new ArgumentException("unkonwn WeponType");
        }
    }
}
