using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Character;
using System;
using MasterData;

public class WorldCreatFlugHelper : IObserver {
    private readonly static WorldCreatFlugHelper INSTANCE = new WorldCreatFlugHelper();

    private int loadWorldId;
    /// <summary>
    /// ワールドをセーブデータからロードするかを示すフラグ
    /// </summary>
    private bool isLoad = false;
    /// <summary>
    /// ワールドが読み込まれたかを表すフラグ
    /// </summary>
    private bool worldLoaded = false;
    private int worldIdDefault = 0;
    /// <summary>
    /// ワールドの初期化が必要かを示すフラグ
    /// </summary>
    private bool isNeedToLoadWorld = false;

    private List<int> worldPasses = new List<int>();

    private Player player;

    private string inputingWorldName;

    private bool isNeedToBackToTop;

    public static WorldCreatFlugHelper getInstance() {
        return INSTANCE;
    }

    private WorldCreatFlugHelper() {
        PioneerManager.getInstance().setObserver(this);
        if (ES2.Exists("BasicData")) {
            ES2Reader reader = ES2Reader.Create("BasicData");
            this.worldPasses = reader.ReadList<Int32>("WorldPass");
            this.worldIdDefault = reader.Read<Int32>("WorldIdDefault");
        }
    }

    public bool getIsLoad() {
        return isLoad;
    }

    public int getLoadWorldId() {
        return loadWorldId;
    }

    public bool getWorldLoaded() {
        return worldLoaded;
    }

    public void setIsLoad(bool flag) {
        isLoad = flag;
    }

    public void setLoadWorldId(int id) {
        loadWorldId = id;
    }

    public void setWorldLoaded(bool flag) {
        worldLoaded = flag;
    }

    public int getWorldIdDefault() {
        return worldIdDefault;
    }

    public void setWorldIdDefault(int id) {
        worldIdDefault = id;
    }

    public void activetePlayer(Transform transfrom) {
        var client = new Client(player.getMisssion(), transfrom);
        player.activateContainer();
        transfrom.position += new Vector3(0, 0, 20);
        player.getContainer().transform.position = transfrom.position;
    }

    public void setPlayer(Player player) {
        this.player = player;
    }

    public void setWorldPass(int id) {
        worldPasses.Add(id);
    }

    public void resetPlayerPos() {
        player.resetPos();
    }

    public List<int> getWorldPasses() {
        return new List<int>(worldPasses);
    }

    public void report(int worldId) {
        ES2Writer writer = ES2Writer.Create("BasicData");
        writer.Write(worldPasses, "WorldPass");
        writer.Write(worldIdDefault, "WorldIdDefault");
        writer.Save();
    }

    public void reset() { }

    public void loadProgresses(int id) {
        EnemyMasterManager.getInstance().addProgress(id);
        AttackSkillMasterManager.getInstance().addProgress(id);
        HealSkillMasterManager.getInstance().addProgress(id);
        BufSkillMasterManager.getInstance().addProgress(id);
        DebufSkillMasterManager.getInstance().addProgress(id);
        HealItemMasterManager.getInstance().addProgress(id);
        ItemMaterialMasterManager.getInstance().addProgress(id);
    }

    public Player getPlayer() {
        return player;
    }

    public bool getIsNeedToLoadWorld() {
        return isNeedToLoadWorld;
    }

    public void setIsNeedToLoadWorld(bool flag) {
        isNeedToLoadWorld = flag;
    }

    public void changedName(string name) {
        inputingWorldName = name;
    }

    public string getInputingWorldName() {
        return inputingWorldName;
    }

    public bool getIsNeedToBackToTop(){
        return isNeedToBackToTop;
    }

    public void setIsNeedToBackToTop(bool flag){
        isNeedToBackToTop = flag;
    }

}
