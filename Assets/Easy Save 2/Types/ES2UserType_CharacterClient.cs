using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Character;

using FriendlyAbility = Parameter.CharacterParameters.FriendlyAbility;

public class ES2UserType_CharacterClient : ES2Type
{
	public override void Write(object obj, ES2Writer writer)
	{
		Client data = (Client)obj;
		// Add your writer.Write calls here.
		writer.Write(data.Id);
		writer.Write(data.Abilities);
		writer.Write(data.Level);
        writer.Write(data.ContainerTransform);

	}
	
	public override object Read(ES2Reader reader)
	{
		var id = reader.Read<System.Int32>();
		var abilities = reader.ReadDictionary<FriendlyAbility, System.Int32>();
		var level = reader.Read<System.Int32>();
        var containerTransform = reader.Read<Transform>();

        Client data = new Client(id,abilities,level,containerTransform);
		return data;
	}

	public override void Read(ES2Reader reader, object c)
	{
		Client client = (Client)c;
		// Add your reader.Read calls here to read the data into the object.
		var id = reader.Read<System.Int32>();
		var abilities = reader.ReadDictionary<FriendlyAbility, System.Int32>();
		var level = reader.Read<System.Int32>();
		var containerTransform = reader.Read<Transform>();

		Client data = new Client(id, abilities, level, containerTransform);
        client = data;
	}
	
	/* ! Don't modify anything below this line ! */
	public ES2UserType_CharacterClient():base(typeof(Client)){}
}