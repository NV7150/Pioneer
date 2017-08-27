using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleLoader : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameObject.Find("TitleManager").GetComponent<TitleManager>().loadTitle();
	}
}
