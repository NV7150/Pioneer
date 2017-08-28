using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Character;
using System;

public class WorldCreator : IObserver {
    private readonly static WorldCreator INSTANCE = new WorldCreator();

    private int loadWorldId;
    private bool isLoad = false;
    private bool worldLoaded = false;
    private int worldIdDefault = 0;

    private List<int> worldPasses = new List<int>();

    private Player player;

    public static WorldCreator getInstance(){
        return INSTANCE;
    }

    private WorldCreator(){
        PioneerManager.getInstance().setObserver(this);
		if (ES2.Exists("BasicData")) {
            ES2Reader reader = ES2Reader.Create("BasicData");
            this.worldPasses = reader.ReadList<Int32>("WorldPass");
            this.worldIdDefault = reader.Read<Int32>("WorldIdDefault");
		}
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

    public void setIsLoad(bool flag){
        isLoad = flag;
    }

    public void setLoadWorldId(int id){
        loadWorldId = id;
    }

    public void setWorldLoaded(bool flag){
        worldLoaded = flag;
    }

    public int getWorldIdDefault(){
        return worldIdDefault;
    }

    public void setWorldIdDefault(int id){
        worldIdDefault = id;
    }

    public void activetePlayer(Transform transfrom){
        player.activateContainer();
        player.getContainer().transform.position = transfrom.position;
        var client = new Client(player.getMisssion(),transfrom);
    }

    public void setPlayer(Player player){
        this.player = player;
    }

    public void setWorldPass(int id){
        worldPasses.Add(id);
    }

    public void resetPlayerPos(){
        player.resetPos();
    }

    public List<int> getWorldPasses(){
        return new List<int>(worldPasses);
    }

	public void report() {
		ES2Writer writer = ES2Writer.Create("BasicData");
		writer.Write(worldPasses, "WorldPass");
		writer.Write(worldIdDefault, "WorldIdDefault");
		writer.Save();
    }

    public void reset() {}
}
