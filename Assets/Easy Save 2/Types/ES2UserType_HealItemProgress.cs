using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ES2UserType_HealItemProgress : ES2Type
{
	public override void Write(object obj, ES2Writer writer)
	{
		HealItemProgress data = (HealItemProgress)obj;
		// Add your writer.Write calls here.
		writer.Write(data.Level);
		writer.Write(data.Heal);
		writer.Write(data.ItemValue);

	}
	
	public override object Read(ES2Reader reader)
	{
		HealItemProgress data = new HealItemProgress();
		Read(reader, data);
		return data;
	}

	public override void Read(ES2Reader reader, object c)
	{
		HealItemProgress data = (HealItemProgress)c;
		// Add your reader.Read calls here to read the data into the object.
		data.Level = reader.Read<System.Int32>();
		data.Heal = reader.Read<System.Int32>();
		data.ItemValue = reader.Read<System.Int32>();

	}
	
	/* ! Don't modify anything below this line ! */
	public ES2UserType_HealItemProgress():base(typeof(HealItemProgress)){}
}