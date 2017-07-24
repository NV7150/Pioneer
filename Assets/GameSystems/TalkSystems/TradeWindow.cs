using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Character;
using Item;

public class TradeWindow : MonoBehaviour {
    public GameObject content;

    List<IItem> goods;
    GameObject tradeItemNodePrefab;
    public GameObject view;
    //かり
    Hero player;
    IFriendly trader;
    List<string> post;
    MassageWindow window;

	// Use this for initialization
    void Start () {
        transform.position = new Vector3(Screen.width ,Screen.height,0);
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
        this.window = window;
    }

    public void itemChose(IItem item) {
        player.addItem(item);
    }

    public void finishChose() {
        Debug.Log("into finishchose");
		window.tradeFinished();
        Destroy(view);
    }
}
