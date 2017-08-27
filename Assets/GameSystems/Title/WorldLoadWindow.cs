using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SelectView;

public class WorldLoadWindow : MonoBehaviour {
    private SelectViewContainer selectviewContainer;
    private SelectView<WorldLoadNode, int> selectView;
    private TitleManager title;

    private void Update() {
        if(Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.UpArrow)){
            int axis = getAxis();

            selectView.moveTo(selectView.getIndex() + axis);
        }

        if(Input.GetKey(KeyCode.Return)){
            title.loadWorldSelected(selectView.getElement());
            Destroy(gameObject);
        }
    }

    private int getAxis(){
		int axis = 0;
		float rawAxis = Input.GetAxisRaw("Vertical");
		if (rawAxis > 0) {
			axis = -1;
		} else if (rawAxis < 0) {
			axis = 1;
		}
        return axis;
    }

	public void setState(List<int> ids, TitleManager manager) {
		this.transform.position = new Vector3(Screen.width / 2, Screen.height / 2);

		selectviewContainer = Instantiate((GameObject)Resources.Load("Prefabs/SelectView")).GetComponent<SelectViewContainer>();
		selectviewContainer.transform.position = this.transform.position;
		selectviewContainer.transform.SetParent(transform);

        var worldNodePrefab = (GameObject)Resources.Load("Prefabs/WorldLoadNode");
        var worldLoadNodes = new List<WorldLoadNode>();

        foreach(int id in ids){
            var node = Instantiate(worldNodePrefab).GetComponent<WorldLoadNode>();
            node.setId(id);
            worldLoadNodes.Add(node);
        }

        selectView = selectviewContainer.creatSelectView<WorldLoadNode, int>(worldLoadNodes);

        title = manager;
    }
}
