using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Character;

public class ES2UserType_CharacterCitizen : ES2Type
{
	public override void Write(object obj, ES2Writer writer)
	{
		Citizen data = (Citizen)obj;
		// Add your writer.Write calls here.
		writer.Write(data.Id);
        writer.Write(data.ContainerTransfrom);

	}
	
	public override object Read(ES2Reader reader)
	{
		var id = reader.Read<System.Int32>();
        var containerTransform = reader.Read<Transform>();
        Citizen data = new Citizen(id, containerTransform);
		return data;
	}

	public override void Read(ES2Reader reader, object c)
	{

		// Add your reader.Read calls here to read the data into the object.
		var id = reader.Read<System.Int32>();
		var containerTransform = reader.Read<Transform>();
		Citizen data = new Citizen(id, containerTransform);
        Citizen citizen = (Citizen)c;
        citizen = data;
	}
	
	/* ! Don't modify anything below this line ! */
	public ES2UserType_CharacterCitizen():base(typeof(Citizen)){}
}