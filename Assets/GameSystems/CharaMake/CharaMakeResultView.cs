using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Parameter;

namespace CharaMake{
    public class CharaMakeResultView : MonoBehaviour {
        /// <summary> 名前 </summary>
        private string name;
        /// <summary> 元となるマネージャ </summary>
        private CharaMakeManager manager;

        /// <summary> 決定ボタン </summary>
        public Button decideButton;
        /// <summary> 名前入力欄 </summary>
        public InputField nameField;
        /// <summary> 入力された名前のテキスト </summary>
        public Text nameText;

        /// <summary> 職業を表示させるテキスト </summary>
        public Text jobText;
        /// <summary> 人間性を表示させるテキスト </summary>
        public Text humanityText;
        /// <summary> 特徴を表示させるテキストのリスト </summary>
        public List<Text> identityTexts;

        // Use this for initialization
        void Start() {
            decideButton.interactable = true;
            nameField.interactable = true;
        }

        /// <summary>
        /// パラメータを表示させます
        /// </summary>
        /// <param name="job">J職業</param>
        /// <param name="humanity">人間性</param>
        /// <param name="identities">特徴のリスト</param>
        /// <param name="manager">元となるマネージャ</param>
        public void setParameters(Job job, Humanity humanity, List<Identity> identities, CharaMakeManager manager) {
            nameField.interactable = false;
            jobText.text = job.getName();
            humanityText.text = humanity.getName();
            int i = 0;
            foreach (Text text in identityTexts) {
                text.text = identities[i].getName();
                i++;
            }
            this.manager = manager;
        }

        /// <summary> 
        /// 名前が設定された時の処理 
        /// </summary>
        public void setName() {
            this.name = nameText.text;
            decideButton.interactable = true;
        }

        /// <summary>
        /// 決定ボタンが押された時の処理
        /// </summary>
        public void decide() {
            manager.nameInputed(name);
            Destroy(gameObject);
        }
    }
}
