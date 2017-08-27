using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ES2UserType_BuildingSaveData : ES2Type
{
	public override void Write(object obj, ES2Writer writer)
	{
		BuildingSaveData data = (BuildingSaveData)obj;
		// Add your writer.Write calls here.
		writer.Write(data.BuildingModelName);
		writer.Write(data.BuildingPostion);
		writer.Write(data.BuildingRotate);

	}
	
	public override object Read(ES2Reader reader)
	{
		var buildingModelName = reader.Read<System.String>();
		var buildingPostion = reader.Read<UnityEngine.Vector3>();
		var buildingRotate = reader.Read<UnityEngine.Quaternion>();
        BuildingSaveData data = new BuildingSaveData(buildingModelName,buildingPostion,buildingRotate);
		return data;
	}

	public override void Read(ES2Reader reader, object c)
	{
		BuildingSaveData saveData = (BuildingSaveData)c;
		// Add your reader.Read calls here to read the data into the object.
		var buildingModelName = reader.Read<System.String>();
		var buildingPostion = reader.Read<UnityEngine.Vector3>();
		var buildingRotate = reader.Read<UnityEngine.Quaternion>();
		BuildingSaveData data = new BuildingSaveData(buildingModelName, buildingPostion, buildingRotate);
        saveData = data;

	}
	
	/* ! Don't modify anything below this line ! */
	public ES2UserType_BuildingSaveData():base(typeof(BuildingSaveData)){}
}