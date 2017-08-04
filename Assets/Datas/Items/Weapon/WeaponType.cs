using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using BattleAbility = Parameter.CharacterParameters.BattleAbility;

namespace Item {
    /// <summary>
    /// 武器の種別
    /// </summary>
    public enum WeaponType {
        SWORD = 0,
        BOW = 1,
        SPEAR = 2,
        BLUNT = 3,
        AX = 4,
        GUN = 5
    }

    public static class WeaponTypeHelper{
        public static BattleAbility getTypeAbility(WeaponType type){
            switch(type){
                case WeaponType.SWORD:
					return BattleAbility.MFT;
                case WeaponType.SPEAR:
					return BattleAbility.MFT;
                case WeaponType.AX:
					return BattleAbility.MFT;
                case WeaponType.BLUNT:
					return BattleAbility.MFT;
                case WeaponType.BOW:
                    return BattleAbility.FFT;
                case WeaponType.GUN:
                    return BattleAbility.FFT;
            }
            throw new ArgumentException("unkonwn WeponType");
        }
    }
}
