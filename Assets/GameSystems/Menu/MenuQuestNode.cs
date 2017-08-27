using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using SelectView;
using Quest;

public class MenuQuestNode : MonoBehaviour,INode<IQuest> {
    IQuest quest;

    public Text nameText;

    public IQuest getElement() {
        return quest;
    }

    public void setQuest(IQuest quest){
        this.quest = quest;
        nameText.text = quest.getName();
    }
}
