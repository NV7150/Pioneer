using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Character;
using Item;

public class TradeWindow : MonoBehaviour {
    List<IItem> goods;
    GameObject tradeItemNodePrefab;
    //かり
    Hero player;

	// Use this for initialization
	void Start () {
        tradeItemNodePrefab = (GameObject)Resources.Load("Prefabs/TradeGoodsNode");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setGoods(List<IItem> goods) {
        this.goods = goods;
        foreach (IItem item in goods) {
            TradeItemNode node = Instantiate(tradeItemNodePrefab).GetComponent<TradeItemNode>();
            node.setGoods(item,this);
        }
    }

    public void itemChose(IItem item) {
        player.addItem(item);
    }
}
