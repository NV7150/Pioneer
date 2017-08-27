using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Character;
using MasterData;
using FieldMap;

using FriendlyCharacterType = Parameter.CharacterParameters.FriendlyCharacterType;
using static Parameter.CharacterParameters.FriendlyCharacterType;

public static class FriendlyCharacterHelper {
    public static IFriendly getFacilityCaracter(int id,FriendlyCharacterType type,Town livingTown){
        switch(type){
            case MERCHANT:
                return MerchantMasterManager.getInstance().getMerchantFromId(id,livingTown);
            case CLIENT:
                return ClientMasterManager.getInstance().getClientFromId(id, livingTown);
        }
        throw new System.ArgumentException("id not found or firendlyCharacter type isn't type of facility owner");
    }
}
