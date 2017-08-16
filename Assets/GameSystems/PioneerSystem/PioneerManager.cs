using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PioneerManager{
    private readonly static PioneerManager INSTANCE = new PioneerManager();

    private List<IObserver> observers = new List<IObserver>();

    private PioneerManager(){}

    public static PioneerManager getInstance(){
        return INSTANCE;
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
