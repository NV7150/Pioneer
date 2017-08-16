using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ES2UserType_EnemyProgress : ES2Type
{
	public override void Write(object obj, ES2Writer writer)
	{
		EnemyProgress data = (EnemyProgress)obj;
		// Add your writer.Write calls here.
		writer.Write(data.Abilities);
		writer.Write(data.AttributeResistances);
		writer.Write(data.Level);
		writer.Write(data.WeponLevel);

	}
	
	public override object Read(ES2Reader reader)
	{
		EnemyProgress data = new EnemyProgress();
		Read(reader, data);
		return data;
	}

	public override void Read(ES2Reader reader, object c)
	{
		EnemyProgress data = (EnemyProgress)c;
		// Add your reader.Read calls here to read the data into the object.
		data.Abilities = reader.ReadDictionary<Parameter.CharacterParameters.BattleAbility,System.Int32>();
		data.AttributeResistances = reader.ReadDictionary<Skill.ActiveSkillParameters.AttackSkillAttribute,System.Single>();
		data.Level = reader.Read<System.Int32>();
		data.WeponLevel = reader.Read<System.Int32>();

	}
	
	/* ! Don't modify anything below this line ! */
	public ES2UserType_EnemyProgress():base(typeof(EnemyProgress)){}
}