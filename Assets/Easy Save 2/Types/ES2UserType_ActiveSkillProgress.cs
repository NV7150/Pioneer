using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ES2UserType_ActiveSkillProgress : ES2Type
{
	public override void Write(object obj, ES2Writer writer)
	{
		ActiveSkillProgress data = (ActiveSkillProgress)obj;
		// Add your writer.Write calls here.
		writer.Write(data.Effect);
		writer.Write(data.Delay);
		writer.Write(data.Cost);

	}
	
	public override object Read(ES2Reader reader)
	{
		ActiveSkillProgress data = new ActiveSkillProgress();
		Read(reader, data);
		return data;
	}

	public override void Read(ES2Reader reader, object c)
	{
		ActiveSkillProgress data = (ActiveSkillProgress)c;
		// Add your reader.Read calls here to read the data into the object.
		data.Effect = reader.Read<System.Int32>();
		data.Delay = reader.Read<System.Single>();
		data.Cost = reader.Read<System.Int32>();

	}
	
	/* ! Don't modify anything below this line ! */
	public ES2UserType_ActiveSkillProgress():base(typeof(ActiveSkillProgress)){}
}