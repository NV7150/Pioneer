using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using FieldMap;

public class BuildingSaveData{
	string buildingModelName;
	Transform buildingTransfrom;

    public string BuildingModelName {
        get { return buildingModelName; }
        set { buildingModelName = value; }
    }

    public Transform BuildingTransfrom {
        get { return buildingTransfrom; }
        set { buildingTransfrom = value; }
    }


    public BuildingSaveData(string modelName, Transform transform){
        buildingModelName = modelName;
        BuildingTransfrom = transform;
    }

    public Building restore(){
        //Debug.Log("into restore");
        var building = MonoBehaviour.Instantiate((GameObject)Resources.Load("Models/TestRoom"));
        building.transform.position = BuildingTransfrom.position;
        building.transform.rotation = BuildingTransfrom.rotation;
        MonoBehaviour.Destroy(buildingTransfrom.gameObject);
        return building.GetComponent<Building>();
    }
}
