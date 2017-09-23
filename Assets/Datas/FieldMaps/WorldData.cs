using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldData{
    private Dictionary<int, Vector3> towns = new Dictionary<int, Vector3>();
	public Dictionary<int, Vector3> Towns {
		get { return new Dictionary<int, Vector3>(towns); }
        set { this.towns = value; }
	}

	private int worldLevel;
    public int WorldLevel {
        get { return worldLevel; }
        set { worldLevel = value; }
    }

    private string worldName;
    public string WorldName{
        get { return worldName; }
        set { worldName = value; }
    }

}
