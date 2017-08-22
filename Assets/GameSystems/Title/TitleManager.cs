using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour,IObserver {
    public List<GameObject> titleCompornents;
    public Button newWorldButton;
    public Button loadButton;
    private List<int> worldPasses = new List<int>();
    private int worldIdDefault = 0;

    private GameObject worldDataNodePrefab;
    private GameObject worldTopPrefab;
    private GameObject worldLoadWindowPrefab;

    private void Awake() {
        worldDataNodePrefab = (GameObject)Resources.Load("Prefabs/WorldDataNodeBase");
        worldTopPrefab = (GameObject)Resources.Load("Prefabs/WorldTop");
        worldLoadWindowPrefab = (GameObject)Resources.Load("Prefabs/WorldLoadWindow");
    }

    public void report() {
        ES2.Save<List<int>>(worldPasses,"BasicData?tag=passList");
        ES2.Save<int>(worldIdDefault,"BasicData?tag=defaultId");
    }

    public void reset() {}

    private void Start() {
        if (ES2.Exists("BasicData")) {
            this.worldPasses = ES2.Load<List<int>>("BasicData?tag=passList");
            this.worldIdDefault = ES2.Load<int>("BasicData?tag=defaultId");

            WorldCreator.getInstance().setWorldIdDefault(worldIdDefault);
        }
        loadButton.interactable = (worldPasses.Count > 0);

        PioneerManager.getInstance().setObserver(this);
    }

    public void creatWorld(){
        foreach(var compornent in titleCompornents){
            Destroy(compornent.gameObject);
        }

		WorldCreator.getInstance().setIsLoad(false);
		WorldTop top = Instantiate(worldTopPrefab).GetComponent<WorldTop>();
        top.transform.SetParent(transform);
        top.transform.position = transform.position;
    }

    public void loadWorld(){
        newWorldButton.interactable = false;
        loadButton.interactable = false;

        WorldLoadWindow window = Instantiate(worldLoadWindowPrefab).GetComponent<WorldLoadWindow>();
        window.setState(worldPasses,this);
        window.transform.SetParent(transform);
    }

    public void loadWorldSelected(int id){
        WorldCreator.getInstance().setIsLoad(true);
        WorldCreator.getInstance().setLoadWorldId(id);
		WorldTop top = Instantiate(worldTopPrefab).GetComponent<WorldTop>();
		top.setId(id);
    }

    public void comeBack(){
        WorldCreator.getInstance().setIsLoad(false);

        newWorldButton.interactable = true;
        loadButton.interactable = (worldPasses.Count > 0);
    }
}
