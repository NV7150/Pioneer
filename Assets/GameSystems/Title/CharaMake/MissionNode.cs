using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

using SelectView;
using Quest;

namespace CharaMake {
    public class MissionNode : MonoBehaviour, INode<IMissionBuilder> {
        public Text nameText;

        IMissionBuilder quest;

        public void setQuest(IMissionBuilder quest){
            this.quest = quest;
            nameText.text = quest.getName();
        }

        public IMissionBuilder getElement() {
            return quest;
        }
    }
}