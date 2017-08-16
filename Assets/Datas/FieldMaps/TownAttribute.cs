using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ItemAttribute = Item.ItemParameters.ItemAttribute;

namespace FieldMap {
    public class TownAttribute {
        private int ID;
        private string NAME;
        private float PRISE_MAG;
        private Dictionary<ItemAttribute, float> attributeMag = new Dictionary<ItemAttribute, float>();

        public TownAttribute(string[] datas){
            ID = int.Parse(datas[0]);
            NAME = datas[1];
            PRISE_MAG = float.Parse(datas[2]);

            var attributes = System.Enum.GetValues(typeof(ItemAttribute));
            int index = 3;
            foreach(ItemAttribute attribute in attributes){
                attributeMag.Add(attribute,float.Parse(datas[index]));
                index++;
            }
        }

        public int getId(){
            return ID;
        }

        public string getName(){
            return NAME;
        }

        public float getPriseMag(){
            return PRISE_MAG;
        }

        public float getAttributeMag(ItemAttribute attribute){
            return attributeMag[attribute];
        }

        public Dictionary<ItemAttribute, float> getAttributeMags(){
            return new Dictionary<ItemAttribute, float>(attributeMag);
        }
    }
}
