using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObserver{
    void report(int worldId);
    void reset();
}
