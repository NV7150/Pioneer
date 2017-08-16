using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldData{
	private Dictionary<int, Vector3> save = new Dictionary<int, Vector3>();
	public Dictionary<int, Vector3> Save {
		get { return new Dictionary<int, Vector3>(save); }
        set { this.save = value; }
	}
}
