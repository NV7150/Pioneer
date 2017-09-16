using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using BattleSystem;

public class ProgressButton : MonoBehaviour {

    public Sprite stopImage;
    public Sprite progressImage;

    public Image image;

    private bool isShowingProgress;

	// Use this for initialization
	void Start() {
        Canvas.ForceUpdateCanvases();
	}
	
	// Update is called once per frame
	void Update () {
        if(isShowingProgress != BattleManager.getInstance().getIsProgressing()){
            changeState(BattleManager.getInstance().getIsProgressing());
        }

        if(!BattleManager.getInstance().getIsBattleing()){
            Destroy(gameObject);
        }
	}

    public void pushed(){
        BattleManager.getInstance().changeProgressing();
    }

    private void changeState(bool state){
        if(state){
            image.sprite = progressImage;
        }else{
            image.sprite = stopImage;
        }

        isShowingProgress = state;
    }
}
