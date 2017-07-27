using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradeView : MonoBehaviour {
    public BuyWindow buyWindow;
    public SellWindow sellWindow;

    public BuyWindow getBuyWindow(){
        return buyWindow;
    }

    public SellWindow getSellWindow(){
        return sellWindow;
    }
}
