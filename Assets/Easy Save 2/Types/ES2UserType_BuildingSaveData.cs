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
		writer.Write(data.BuildingTransfrom);

	}
	
	public override object Read(ES2Reader reader)
	{

        var buildingModelName = reader.Read<System.String>();
        var buildingTransfrom = reader.Read<UnityEngine.Transform>();
        BuildingSaveData data = new BuildingSaveData(buildingModelName,buildingTransfrom);
		return data;
	}

	public override void Read(ES2Reader reader, object c)
	{
		BuildingSaveData data = (BuildingSaveData)c;
		// Add your reader.Read calls here to read the data into the object.
		var buildingModelName = reader.Read<System.String>();
		var buildingTransfrom = reader.Read<UnityEngine.Transform>();
		BuildingSaveData dataSource = new BuildingSaveData(buildingModelName, buildingTransfrom);
        data = dataSource;
	}
	
	/* ! Don't modify anything below this line ! */
	public ES2UserType_BuildingSaveData():base(typeof(BuildingSaveData)){}
}