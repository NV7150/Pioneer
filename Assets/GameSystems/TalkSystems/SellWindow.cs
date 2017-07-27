using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Character;
using Item;

public class SellWindow : MonoBehaviour ,ITradeWindow{
    private Hero player;
    private IFriendly trader;
    private Inventry inventry;

    private GameObject tradeItemNodePrefab;
    public GameObject content;
    public Text headerText;


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setState(Hero player, IFriendly trader){
        this.player = player;
        this.trader = trader;
        this.inventry = player.getInventry();

        headerText.text = player.getName();

        tradeItemNodePrefab = (GameObject)Resources.Load("Prefabs/TradeItemNode");

        foreach(IItem item in inventry.getItems()){
            GameObject nodeObject = Instantiate(tradeItemNodePrefab);
            TradeItemNode node = nodeObject.GetComponent<TradeItemNode>();
            node.setGoods(item,this);
            nodeObject.transform.SetParent(content.transform);
        }
    }

    public void updateItem(){
        detachContents();

		foreach (IItem item in inventry.getItems()) {
			GameObject nodeObject = Instantiate(tradeItemNodePrefab);
			TradeItemNode node = nodeObject.GetComponent<TradeItemNode>();
			node.setGoods(item, this);
			nodeObject.transform.SetParent(content.transform);
		}
    }

	/// <summary>
	/// contentsオブジェクトの子ノードを削除します
	/// </summary>
	private void detachContents() {
		Transform children = content.GetComponentInChildren<Transform>();
		foreach (Transform child in children) {
			Destroy(child.gameObject);
		}
		content.transform.DetachChildren();
	}

    public void itemChose(IItem item,TradeItemNode node){
        inventry.removeItem(item);
        //かり
        player.addMetal(item.getItemValue());
        Destroy(node.gameObject);
    }
}
