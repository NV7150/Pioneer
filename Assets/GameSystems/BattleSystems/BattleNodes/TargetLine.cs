using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Character;

namespace BattleSystem {
    public class TargetLine : MonoBehaviour {
        private GameObject attackerModel;
        private GameObject targetModel;

        public List<Material> friendlyMaterials = new List<Material>();
        public List<Material> enemyMaterials = new List<Material>();
        private List<Material> useMaterials = new List<Material>();
        private int materialIndex = 0;

        public GameObject textObject;
        private TextMesh lineText;
        public LineRenderer line;

        private readonly Vector3 TEXT_PADDING = new Vector3(0f, 5f, 0f);

        private float limit = 0f;

        private GameObject battleCamera;

        // Use this for initialization
        void Start() {
            battleCamera = GameObject.Find("BattleCamera");
        }

        // Update is called once per frame
        void Update() {
            if (battleCamera == null) {
                battleCamera = GameObject.Find("BattleCamera");
            } else {
                transform.LookAt(battleCamera.transform);
            }

            if (attackerModel != null) {
                updateLinePos();
                if (limit <= 0) {
                    updateMaterial();
                    limit = 0.005f;
                }else{
                    limit -= Time.deltaTime;
                }
            }
        }

        private void updateLinePos(){
            Vector3 attackerPos = attackerModel.transform.position;
            Vector3 targetPos = targetModel.transform.position;

            line.SetPosition(0,attackerPos);
            line.SetPosition(1,targetPos);

            float distance = Vector3.Distance(attackerPos, targetPos);

            textObject.transform.position = (attackerPos + targetPos) / 2 + TEXT_PADDING;
        }

        private void updateMaterial(){
            line.material = useMaterials[materialIndex];
            materialIndex = (materialIndex >= useMaterials.Count - 1) ? 0 : materialIndex + 1;
        }

		public void setState(GameObject attackerModel, GameObject targetModel, string skillName, bool isFriendly) {
			useMaterials = (isFriendly) ? friendlyMaterials : enemyMaterials;

            this.attackerModel = attackerModel;
			this.targetModel = targetModel;
			lineText = textObject.GetComponent<TextMesh>();
            lineText.text = skillName;
        }
    }
}
