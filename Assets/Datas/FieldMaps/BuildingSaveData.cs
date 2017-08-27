using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using FieldMap;

public class BuildingSaveData{
	string buildingModelName;
    Vector3 buildingPos;
    Quaternion buildingRotate;

    public string BuildingModelName {
        get { return buildingModelName; }
        set { buildingModelName = value; }
    }

    public Vector3 BuildingPostion{
        get { return buildingPos; }
        set { buildingPos = value; }
    }

    public Quaternion BuildingRotate{
        get{ return buildingRotate; }
        set { buildingRotate = value; }
    }


    public BuildingSaveData(string modelName, Transform transform){
        buildingModelName = modelName;
        buildingPos = transform.position;
        buildingRotate = transform.rotation;
    }

    public BuildingSaveData(string modelName, Vector3 pos,Quaternion rotate){
        this.buildingModelName = modelName;
        buildingPos = pos;
        buildingRotate = rotate;
    }

    public Building restore(){
        var building = MonoBehaviour.Instantiate((GameObject)Resources.Load("Models/TestRoom"));
        building.transform.position = buildingPos;
        building.transform.rotation = buildingRotate;
        return building.GetComponent<Building>();
    }
}
