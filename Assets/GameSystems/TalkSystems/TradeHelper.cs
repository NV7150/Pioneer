using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Item;
using Character;

using static Parameter.CharacterParameters.FriendlyAbility;

public static class TradeHelper {

    public static int getBuyValue(IItem product,Player player,Merchant trader){
        int itemValue = (int)(product.getItemValue() * trader.getValueMag(product.getItemAttribute()));
        itemValue = (int)( itemValue * ( 1.5f - ( (player.getFriendlyAbility(SPC) - trader.getFriendlyAbility(SPC)) * 0.1 ) ) );
        return itemValue;
    }

    public static int getSellValue(IItem product,Player player,Merchant trader){
		int itemValue = (int)(product.getItemValue() * trader.getValueMag(product.getItemAttribute()));
        itemValue = (int)(itemValue * (0.5f + ((player.getFriendlyAbility(SPC) - trader.getFriendlyAbility(SPC)) * 0.1)));
		return itemValue;
    }
}
