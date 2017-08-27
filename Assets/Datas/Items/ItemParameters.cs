using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Item{
    public static class ItemParameters {
        public enum ItemType {
            /// <summary> 武器 </summary>
            WEAPON,
            /// <summary> 防具 </summary>
            ARMOR,
            /// <summary> 回復アイテム </summary>
            HEAL_ITEM,
            /// <summary> 素材アイテム </summary>
            ITEM_MATERIAL,
            /// <summary> スキル書 </summary>
            SKILL_BOOK,
            /// <summary> 換金アイテム </summary>
            TRADING_ITEM
        }

        public enum ItemAttribute{
            /// <summary>
            /// 武器
            /// </summary>
            WEAPON,
            /// <summary>
            /// 防具
            /// </summary>
            ARMOR,
            /// <summary>
            /// 薬品
            /// </summary>
            DRUG,
            /// <summary>
            /// 金属
            /// </summary>
            METAL,
            /// <summary>
            /// 森林資源
            /// </summary>
            FOREST,
            /// <summary>
            /// 海産資源
            /// </summary>
            SEA,
			/// <summary>
			/// 農業資源
			/// </summary>
			AGRICULTURE,
			/// <summary>
			/// 畜産資源
			/// </summary>
			ANIMAL,
            /// <summary>
            /// 機械
            /// </summary>
            MACHINE,
			/// <summary>
			/// 加工品
			/// </summary>
			PROCESSED,
            /// <summary>
            /// 魔法資源
            /// </summary>
            MAGIC,
            /// <summary>
            /// 芸術品
            /// </summary>
            ART
        }
    }
}
