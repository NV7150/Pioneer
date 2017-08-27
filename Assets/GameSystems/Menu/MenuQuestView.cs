using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Quest;

public class MenuQuestView : MonoBehaviour {
    public Text nameText;
    public Text descriptionText;
    public Text flavorText;

    public void printQuest(IQuest quest){
        nameText.text = quest.getName();
        descriptionText.text = quest.getDescription();
        flavorText.text = quest.getFlavorText();
    }
}
