using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using CharaMake;
using MasterData;
using FieldMap;

public class WorldTop : MonoBehaviour {
    private int id;

    private TitleManager title;

    public Text levelText;
    public InputField nameText;

    private WorldData data;

    public void setState(TitleManager title){
        this.title = title;
        nameText.text = "新しい世界";
        levelText.text = "Lv." + 1;
        WorldCreatFlugHelper.getInstance().changedName("新しい世界");
    }

    public void setState(int id ,TitleManager title){
		this.id = id;
		data = MasterDataManagerBase.loadSaveData<WorldData>(id, id, "WorldData");
        nameText.text = data.WorldName;
		levelText.text = "Lv." + data.WorldLevel;
		WorldCreatFlugHelper.getInstance().changedName(data.WorldName);
        this.title = title;
    }

	public void startCharaMake() {
		CharaMakeManager manager = Instantiate((GameObject)Resources.Load("Prefabs/CharaMakeManager")).GetComponent<CharaMakeManager>();
        if (WorldCreatFlugHelper.getInstance().getIsLoad()) {
            manager.setDatas(data.WorldLevel);
        }else{
            manager.setDatas(1);
        }
        Destroy(gameObject);
    }

    public void back(){
        title.loadTitle();
        Destroy(gameObject);
    }

    public void nameInputed(){
        if (nameText.text.Length > 0) {
            nameText = TextInputHelper.getText(nameText);
            WorldCreatFlugHelper.getInstance().changedName(nameText.text);
        }
    }
}
