using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quest {
    public interface IMissionBuilder {
        IQuest build(FlagList flags);

        string getName();

        string getDescription();

        string getFlavorText();
    }
}
