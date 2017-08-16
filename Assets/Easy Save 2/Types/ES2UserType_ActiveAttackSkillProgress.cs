using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ES2UserType_ActiveAttackSkillProgress : ES2Type
{
	public override void Write(object obj, ES2Writer writer)
	{
		ActiveAttackSkillProgress data = (ActiveAttackSkillProgress)obj;
		// Add your writer.Write calls here.
		writer.Write(data.Hit);
		writer.Write(data.Effect);
		writer.Write(data.Delay);
		writer.Write(data.Cost);

	}
	
	public override object Read(ES2Reader reader)
	{
		ActiveAttackSkillProgress data = new ActiveAttackSkillProgress();
		Read(reader, data);
		return data;
	}

	public override void Read(ES2Reader reader, object c)
	{
		ActiveAttackSkillProgress data = (ActiveAttackSkillProgress)c;
		// Add your reader.Read calls here to read the data into the object.
		data.Hit = reader.Read<System.Int32>();
		data.Effect = reader.Read<System.Int32>();
		data.Delay = reader.Read<System.Single>();
		data.Cost = reader.Read<System.Int32>();

	}
	
	/* ! Don't modify anything below this line ! */
	public ES2UserType_ActiveAttackSkillProgress():base(typeof(ActiveAttackSkillProgress)){}
}