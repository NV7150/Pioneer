using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Character;
using Item;

public class TradeWindow : MonoBehaviour {
    public GameObject content;

    List<IItem> goods;
    GameObject tradeItemNodePrefab;
    //かり
    Hero player;
    IFriendly trader;
    List<string> post;
    MassageWindow win;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setState(List<IItem> goods,Hero player,IFriendly trader,MassageWindow window) {
        tradeItemNodePrefab = (GameObject)Resources.Load("Prefabs/TradeItemNode");
        this.goods = goods;
        foreach (IItem item in goods) {
            TradeItemNode node = Instantiate(tradeItemNodePrefab).GetComponent<TradeItemNode>();
            node.setGoods(item,this);
            node.transform.SetParent(content.transform);
        }
        this.player = player;
        this.trader = trader;
    }

    public void itemChose(IItem item) {
        player.addItem(item);
    }

    public void finishChose() {
        win.tradeFinished();
        Destroy(this.gameObject);
    }
}
