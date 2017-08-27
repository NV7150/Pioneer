using Character;

using QuestType = Quest.QuestParameters.QuestType;
using CompentionType = Quest.QuestParameters.CompentionType;

namespace Quest{
    public interface IQuest {
        bool isCleared();

        string getName();

        string getDescription();

        string getFlavorText();

        void activateCompensation(Player player);

        QuestType getQuestType();

        Client getQuester();

        CompentionType getCompentionType();
    }
}
