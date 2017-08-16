using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Character;

public class ES2UserType_CharacterMerchant : ES2Type
{
	public override void Write(object obj, ES2Writer writer)
	{
		Character.Merchant data = (Character.Merchant)obj;
		// Add your writer.Write calls here.
		writer.Write(data.Id);
		writer.Write(data.NumberOfGoods);
		writer.Write(data.GoodsLevel);
		writer.Write(data.Abilities);
		writer.Write(data.containerTransform);

	}
	
	public override object Read(ES2Reader reader)
	{
		var id = reader.Read<System.Int32>();
		var numberOfGoods = reader.Read<System.Int32>();
		var goodsLevel = reader.Read<System.Int32>();
		var abilities = reader.ReadDictionary<Parameter.CharacterParameters.FriendlyAbility, System.Int32>();
        var containerTransfrom = reader.Read<Transform>();
        Merchant data = new Merchant(id,goodsLevel,numberOfGoods,abilities,containerTransfrom);
		return data;
	}

	public override void Read(ES2Reader reader, object c)
	{
		var id = reader.Read<System.Int32>();
		var numberOfGoods = reader.Read<System.Int32>();
		var goodsLevel = reader.Read<System.Int32>();
		var abilities = reader.ReadDictionary<Parameter.CharacterParameters.FriendlyAbility, System.Int32>();
		var containerTransfrom = reader.Read<Transform>();
		Merchant dataSource = new Merchant(id, goodsLevel, numberOfGoods, abilities, containerTransfrom);

		Merchant data = (Character.Merchant)c;
        data = dataSource;
	}
	
	/* ! Don't modify anything below this line ! */
	public ES2UserType_CharacterMerchant():base(typeof(Character.Merchant)){}
}