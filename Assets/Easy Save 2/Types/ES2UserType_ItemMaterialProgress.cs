using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ES2UserType_ItemMaterialProgress : ES2Type
{
	public override void Write(object obj, ES2Writer writer)
	{
		ItemMaterialProgress data = (ItemMaterialProgress)obj;
		// Add your writer.Write calls here.
		writer.Write(data.Level);
		writer.Write(data.Quality);
		writer.Write(data.ItemValue);

	}
	
	public override object Read(ES2Reader reader)
	{
		ItemMaterialProgress data = new ItemMaterialProgress();
		Read(reader, data);
		return data;
	}

	public override void Read(ES2Reader reader, object c)
	{
		ItemMaterialProgress data = (ItemMaterialProgress)c;
		// Add your reader.Read calls here to read the data into the object.
		data.Level = reader.Read<System.Int32>();
		data.Quality = reader.Read<System.Single>();
		data.ItemValue = reader.Read<System.Int32>();

	}
	
	/* ! Don't modify anything below this line ! */
	public ES2UserType_ItemMaterialProgress():base(typeof(ItemMaterialProgress)){}
}