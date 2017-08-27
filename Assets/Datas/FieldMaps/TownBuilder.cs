using UnityEngine;
using System.Collections.Generic;
using System;

using Character;
using FieldMap;

using ItemAttribute = Item.ItemParameters.ItemAttribute;
using Item;

public class TownBuilder {
    int id;
    Vector3 position;
    Quaternion quaternion;
    int level;
    int size;
    List<Citizen> citizens;
    List<Merchant> merchants;
    List<Client> clients;
    float priseMag;
    Dictionary<ItemAttribute, float> attributeMag;
    List<BuildingSaveData> buildingDatas = new List<BuildingSaveData>();
    bool[,] grid = new bool[10,10];
    int townAttributeId;
    Town.RoadDirection direction;

    public int Id{
        get { return id; }
        set { id = value; }
    }


    public int Level {
        get { return level; }

        set { level = value; }
    }

    public int Size {
        get { return size; }

        set { size = value; }
    }

    public List<Citizen> Citizens {
        get { return new List<Citizen>(citizens); }

        set { citizens = new List<Citizen>(value); }
    }

    public List<Merchant> Merchants {
        get { return new List<Merchant>(merchants); }

        set { merchants = new List<Merchant>(value); }
    }

    public List<Client> Clients {
        get { return new List<Client>(clients); }

        set { clients = new List<Client>(value); }
    }

    public float PriseMag {
        get { return priseMag; }

        set { priseMag = value; }
    }

    public List<BuildingSaveData> BuildingDatas {
        get { return new List<BuildingSaveData>(buildingDatas); }

        set { buildingDatas = new List<BuildingSaveData>(value); }
    }

    public Dictionary<ItemAttribute, float> AttributeMag {
        get { return new Dictionary<ItemAttribute, float>(attributeMag); }

        set { attributeMag = new Dictionary<ItemAttribute, float>(value); }
    }

    public bool[,] Grid{
        get { 
            bool[,] returnGrid = new bool[10,10]; 
            Array.Copy(grid,returnGrid,grid.Length);
            return returnGrid;
        }

        set{ Array.Copy(value,grid,value.Length); }
    }

    public int TownAttributeId{
        get { return townAttributeId; }
        set { townAttributeId = value; }
    }

    public Town.RoadDirection Direction{
        get { return this.direction; }
        set { this.direction = value; }
    }

    public Vector3 Position {
        get { return position; }

        set { position = value; }
    }

    public Quaternion Quaternion {
        get { return quaternion; }

        set { quaternion = value; }
    }
}
