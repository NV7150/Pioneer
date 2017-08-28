using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PioneerManager{
    private readonly static PioneerManager INSTANCE = new PioneerManager();

    private List<IObserver> observers = new List<IObserver>();

    private GameObject resultViewPrefab;

    private PioneerManager(){
        resultViewPrefab = (GameObject)Resources.Load("Prefabs/ResultView");
    }

    public static PioneerManager getInstance(){
        return INSTANCE;
    }

    public void resultPrint(){
        var resultView = MonoBehaviour.Instantiate(resultViewPrefab);
        resultView.transform.SetParent(CanvasGetter.getCanvas().transform);
        resultView.transform.position = new Vector3(Screen.width / 2, Screen.height / 2);
    }

	public void finished() {
		ES2.DeleteDefaultFolder();
        foreach(IObserver observer in observers){
            observer.report(WorldCreator.getInstance().getLoadWorldId());
            observer.reset();
        }
        SceneKeeper.deleteScene();
    }

    public void setObserver(IObserver observer){
        observers.Add(observer);
    }
}
