using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using FieldMap;
using System;

public class WorldObserver : IObserver {
    World world;

    public WorldObserver(World world){
        this.world = world;
        PioneerManager.getInstance().setObserver(this);
    }

    public void report(int worldId) {
        Debug.Log("called report world");
        world.levelUpWorld();
        world.saveWorld();
    }

    public void reset() {
        world.resetWorld();
        WorldCreatFlugHelper.getInstance().setWorldLoaded(false);
        PioneerManager.getInstance().removeObserver(this);
    }
}
