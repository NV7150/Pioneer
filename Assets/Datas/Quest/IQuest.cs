﻿using Character;

using QuestType = Quest.QuestParameters.QuestType;

namespace Quest{
    public interface IQuest {
        bool isCleared();

        string getName();

        string getDescription();

        string getFlavorText();

        void activateCompensation(Hero player);

        QuestType getQuestType();

        Client getQuester();
    }
}