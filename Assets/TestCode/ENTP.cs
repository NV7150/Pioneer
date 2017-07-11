using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ENTP : MonoBehaviour {
	public Text text;
	public static int hp;
	public static int mp;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		text.text = "HP " + hp + " MP ";
	}
}
