using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Menus;

public class BackWindow : MonoBehaviour {
    Menu menu;

    public void setState(Menu menu){
		this.menu = menu;
		menu.setIsWindowInputing(true);
    }

    public void cancel(){
        menu.setIsWindowInputing(false);
        menu.backChose();
        Destroy(gameObject);
    }

    public void back(){
        menu.setIsDisplaying(false);
        PioneerManager.getInstance().retire();
    }
}
