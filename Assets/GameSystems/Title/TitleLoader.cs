using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleLoader : MonoBehaviour {
    bool titleLoad = false;

    public TitleManager manager;
    public Camera titleCamera;

    public static TitleLoader instance;

    private void Awake(){
        if (instance != null) {
            Destroy(gameObject);
        }else{
			instance = GetComponent<TitleLoader>();
			DontDestroyOnLoad(this);
        }
    }

    private void Update(){
        if (titleLoad) {
            if (WorldCreatFlugHelper.getInstance().getIsNeedToBackToTop()) {
                manager.loadWorldTop();
                WorldCreatFlugHelper.getInstance().setIsNeedToBackToTop(false);
            } else {
                manager.loadTitle();
			}

			titleLoad = false;
			titleCamera.gameObject.SetActive(true);
        }
    }

    public static TitleLoader getInstance(){
        return instance;
    }

    public void titleCameraUnable(){
        titleCamera.gameObject.SetActive(false);
    }

    public void setTitleLoad(bool flag){
        titleLoad = flag;
    }
}
