using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PioneerManager{
    private readonly static PioneerManager INSTANCE = new PioneerManager();

    private List<IObserver> observers = new List<IObserver>();

    private PioneerManager(){}

    public void finished(){
        foreach(IObserver observer in observers){
            observer.report();
            observer.reset();
        }
    }
}
