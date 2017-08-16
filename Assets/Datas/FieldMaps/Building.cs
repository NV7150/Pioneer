using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Character;
using MasterData;

namespace FieldMap {
    [System.SerializableAttribute]
    public class Building : MonoBehaviour {
        bool isTransformEnabled = false;
        long id;

        public Transform buildingtransfrom{
            get { return this.transform; }
            set{
                if (!isTransformEnabled) {
                    this.transform.position = value.position;
                    this.transform.rotation = value.rotation;
                    isTransformEnabled = true;
                }else{
                    throw new System.ArgumentException("building has already been enabled");
                }
            }
        }

        public long Id{
            get { return id; }
        }

        public void setState(Vector3 pos,long id) {
            transform.position = pos;
            this.id = id;
        }
    }
}
