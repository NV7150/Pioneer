﻿using System.Collections;
            GameObject massageWindow = MonoBehaviour.Instantiate(massageWindowPrefab);
            massageWindow.transform.SetParent(CanvasGetter.getCanvas().transform);
            massageWindow.GetComponent<MassageWindowNode>().setMassageList(massages);
            isTalking = true;
        }
        isTalking = false;
    }