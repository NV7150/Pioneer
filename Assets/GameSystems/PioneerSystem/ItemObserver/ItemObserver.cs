using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Item;

public abstract class ItemObserver : IObserver{
    protected readonly int OBSERVE_ITEM_ID;

    protected int useFrequency = 0;

    protected ItemObserver(int itemId){
		OBSERVE_ITEM_ID = itemId;
		PioneerManager.getInstance().setObserver(this);
    }

    public void usedItem(){
        useFrequency++;
    }

	public abstract void report();
	public abstract void reset();
}
