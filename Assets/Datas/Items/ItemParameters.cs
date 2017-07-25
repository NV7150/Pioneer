using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ItemParameters{
    public enum ItemType{
        /// <summary> 武器 </summary>
        WEPON,
        /// <summary> 防具 </summary>
        ARMOR,
        /// <summary> 回復アイテム </summary>
        HEAL_ITEM,
        /// <summary> 換金アイテム </summary>
        TRADING_ITEM
    }
}
