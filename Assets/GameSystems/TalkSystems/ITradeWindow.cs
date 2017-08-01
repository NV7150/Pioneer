using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Item;

namespace TalkSystem {
    public interface ITradeWindow {
        void itemChose(IItem item, TradeItemNode node);
    }
}
