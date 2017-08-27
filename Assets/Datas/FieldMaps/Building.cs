using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Character;
using MasterData;

namespace FieldMap {
    [System.SerializableAttribute]
    public class Building : MonoBehaviour {
        bool isPositionEnabled = false;
        bool isRotationEnabled = false;
        long id;

        public Vector3 buildingPosition{
            get { return this.transform.position; }
            set{
                if (!isPositionEnabled) {
                    this.transform.position = value;
                    isPositionEnabled = true;
                }else{
                    throw new System.ArgumentException("building position has already been enabled");
                }
            }
        }

        public Quaternion buildingRotaion{
            get { return this.transform.rotation; }
            set{
                if(!isRotationEnabled){
                    this.transform.rotation = value;
                    isRotationEnabled = true;
                }else{
                    throw new System.ArgumentException("building rotation has already been enabled");
                }
            }
        }

        public long Id{
            get { return id; }
        }

        public void setState(Vector3 pos,long id) {
            pos.y = Terrain.activeTerrain.terrainData.GetInterpolatedHeight(
                pos.x / Terrain.activeTerrain.terrainData.size.x,
                pos.z / Terrain.activeTerrain.terrainData.size.z
            );

            transform.position = pos;
            this.id = id;
        }
    }
}
