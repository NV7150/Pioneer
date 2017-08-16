using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FieldMap;

public class ES2UserType_FieldMapBuilding : ES2Type
{
	public override void Write(object obj, ES2Writer writer)
	{
		FieldMap.Building data = (FieldMap.Building)obj;
		// Add your writer.Write calls here.
		writer.Write(data.buildingtransfrom);

	}
	
	public override object Read(ES2Reader reader)
	{
		FieldMap.Building data = GetOrCreate<FieldMap.Building>();
		Read(reader, data);
		return data;
	}

	public override void Read(ES2Reader reader, object c)
	{
		FieldMap.Building data = (FieldMap.Building)c;
		// Add your reader.Read calls here to read the data into the object.
		data.buildingtransfrom = reader.Read<UnityEngine.Transform>();

	}
	
	/* ! Don't modify anything below this line ! */
	public ES2UserType_FieldMapBuilding():base(typeof(FieldMap.Building)){}
}