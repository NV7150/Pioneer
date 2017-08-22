using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Character;

public class WorldCreator{
    private readonly static WorldCreator INSTANCE = new WorldCreator();

    private int loadWorldId;
    private bool isLoad = false;
    private bool worldLoaded = false;
    private int worldIdDefault = 0;

    private Player player;

    public static WorldCreator getInstance(){
        return INSTANCE;
    }

    public bool getIsLoad(){
        return isLoad;
    }

    public int getLoadWorldId(){
        return loadWorldId;
    }

    public bool getWorldLoaded(){
        return worldLoaded;
    }

    public int getWorldIdDefault(){
        return worldIdDefault;
    }

    public void setIsLoad(bool flag){
        isLoad = flag;
    }

    public void setLoadWorldId(int id){
        loadWorldId = id;
    }

    public void setWorldLoaded(bool flag){
        worldLoaded = flag;
    }

    public void setWorldIdDefault(int id){
        worldIdDefault = id;
    }

    public void activetePlayer(){
        player.activateContainer();
    }

    public void setPlayer(Player player){
        this.player = player;
    }
}
