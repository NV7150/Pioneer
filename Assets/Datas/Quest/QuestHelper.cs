using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

using MasterData;
using Character;

using QuestType = Quest.QuestParameters.QuestType;
using static Quest.QuestParameters.QuestType;

namespace Quest {
    public static class QuestHelper {
        public static IQuest getTypeQuest(QuestType type, FlagList flags, Client client){
            switch(type){
                case EXTERMINATION: return new ExterminationQuest(flags, client);
            }
            throw new System.ArgumentException("unkown QuestType");
        }

        public static IMissionBuilder getRandomMission(int baseLevel){
            var types = Enum.GetValues(typeof(QuestType));
            int typeRand = UnityEngine.Random.Range(0, types.Length);
            QuestType type = (QuestType)types.GetValue(typeRand);

            switch(type){
                case EXTERMINATION:return new ExterminationMissonBuilder(baseLevel);
            }
            throw new NotSupportedException("unkown QuestType");
        }
    }
}
