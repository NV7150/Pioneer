using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Item;

public class TradeItemNode : MonoBehaviour {
    private IItem item;
    public Text name;
    public Text value;
    private TradeWindow window;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setGoods(IItem item,TradeWindow window) {
        this.item = item;
        name.text = this.item.getName();
        //かり
        value.text = "" + item.getItemValue();
    }

    public void chosen() {
        window.itemChose(item);
    }
}
