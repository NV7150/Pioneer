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
        MonoBehaviour.Instantiate(resultViewPrefab);
    }

	public void finished() {
		ES2.DeleteDefaultFolder();
        foreach(IObserver observer in observers){
            observer.report();
            observer.reset();
        }

    }

    public void setObserver(IObserver observer){
        observers.Add(observer);
    }
}
