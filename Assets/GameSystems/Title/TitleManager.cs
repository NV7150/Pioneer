using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour{
    public List<GameObject> titleCompornents;
    public Button newWorldButton;
    public Button loadButton;

    private GameObject worldDataNodePrefab;
    private GameObject worldTopPrefab;
    private GameObject worldLoadWindowPrefab;

    private void Awake() {
        worldDataNodePrefab = (GameObject)Resources.Load("Prefabs/WorldDataNodeBase");
        worldTopPrefab = (GameObject)Resources.Load("Prefabs/WorldTop");
        worldLoadWindowPrefab = (GameObject)Resources.Load("Prefabs/WorldLoadWindow");
    }

    private void Start() {
        loadButton.interactable = (WorldCreator.getInstance().getWorldPasses().Count > 0);
    }

    public void creatWorld(){
        foreach(var compornent in titleCompornents){
            compornent.SetActive(false);
        }

		WorldCreator.getInstance().setIsLoad(false);
		WorldTop top = Instantiate(worldTopPrefab).GetComponent<WorldTop>();
        top.setState(this);
        top.transform.SetParent(transform);
        top.transform.position = transform.position;
    }

    public void loadWorld(){
        newWorldButton.interactable = false;
        loadButton.interactable = false;

        WorldLoadWindow window = Instantiate(worldLoadWindowPrefab).GetComponent<WorldLoadWindow>();
        window.setState(WorldCreator.getInstance().getWorldPasses(),this);
        window.transform.SetParent(transform);
    }

    public void loadWorldSelected(int id){
		foreach (var compornent in titleCompornents) {
			compornent.SetActive(false);
		}

        WorldCreator.getInstance().setIsLoad(true);
        WorldCreator.getInstance().setLoadWorldId(id);
		WorldTop top = Instantiate(worldTopPrefab).GetComponent<WorldTop>();
        top.setState(id,this);
        top.transform.SetParent(CanvasGetter.getCanvas().transform);
        top.transform.position = new Vector3(Screen.width / 2, Screen.height / 2);

    }

    public void loadTitle(){
        WorldCreator.getInstance().setIsLoad(false);

		foreach (var compornent in titleCompornents) {
            compornent.SetActive(true);
		}

		newWorldButton.interactable = true;
		loadButton.interactable = (WorldCreator.getInstance().getWorldPasses().Count > 0);
    }
}
