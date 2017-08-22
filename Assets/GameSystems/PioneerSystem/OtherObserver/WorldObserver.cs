﻿using System.Collections;
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

    public void report() {
        world.saveWorld();
    }

    public void reset() {
        WorldCreator.getInstance().setWorldLoaded(false);
    }
}
