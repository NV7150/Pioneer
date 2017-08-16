using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ES2UserType_TownBuilder : ES2Type
{
	public override void Write(object obj, ES2Writer writer)
	{
		TownBuilder data = (TownBuilder)obj;
		// Add your writer.Write calls here.
		writer.Write(data.Id);
		writer.Write(data.Transfrom);
		writer.Write(data.Level);
		writer.Write(data.Size);
		writer.Write(data.Citizens);
		writer.Write(data.Merchants);
		writer.Write(data.Clients);
		writer.Write(data.PriseMag);
		writer.Write(data.BuildingDatas);
		writer.Write(data.AttributeMag);
		writer.Write(data.Grid);
		writer.Write(data.TownAttributeId);
		writer.Write(data.Direction);

	}
	
	public override object Read(ES2Reader reader)
	{
		TownBuilder data = new TownBuilder();
		Read(reader, data);
		return data;
	}

	public override void Read(ES2Reader reader, object c)
	{
		TownBuilder data = (TownBuilder)c;
		// Add your reader.Read calls here to read the data into the object.
		data.Id = reader.Read<System.Int32>();
		data.Transfrom = reader.Read<UnityEngine.Transform>();
		data.Level = reader.Read<System.Int32>();
		data.Size = reader.Read<System.Int32>();
		data.Citizens = reader.ReadList<Character.Citizen>();
		data.Merchants = reader.ReadList<Character.Merchant>();
		data.Clients = reader.ReadList<Character.Client>();
		data.PriseMag = reader.Read<System.Single>();
		data.BuildingDatas = reader.ReadList<BuildingSaveData>();
		data.AttributeMag = reader.ReadDictionary<Item.ItemParameters.ItemAttribute,System.Single>();
		data.Grid = reader.Read2DArray<System.Boolean>();
		data.TownAttributeId = reader.Read<System.Int32>();
		data.Direction = reader.Read<FieldMap.Town.RoadDirection>();

	}
	
	/* ! Don't modify anything below this line ! */
	public ES2UserType_TownBuilder():base(typeof(TownBuilder)){}
}