using System.Collections;
using System.Collections.Generic;
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
    }
}
