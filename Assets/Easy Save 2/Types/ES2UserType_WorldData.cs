using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ES2UserType_WorldData : ES2Type
{
	public override void Write(object obj, ES2Writer writer)
	{
		WorldData data = (WorldData)obj;
		// Add your writer.Write calls here.
		writer.Write(data.Towns);
		writer.Write(data.WorldLevel);
		writer.Write(data.WorldName);

	}
	
	public override object Read(ES2Reader reader)
	{
		WorldData data = new WorldData();
		Read(reader, data);
		return data;
	}

	public override void Read(ES2Reader reader, object c)
	{
		WorldData data = (WorldData)c;
		// Add your reader.Read calls here to read the data into the object.
		data.Towns = reader.ReadDictionary<System.Int32,UnityEngine.Vector3>();
		data.WorldLevel = reader.Read<System.Int32>();
		data.WorldName = reader.Read<System.String>();

	}
	
	/* ! Don't modify anything below this line ! */
	public ES2UserType_WorldData():base(typeof(WorldData)){}
}