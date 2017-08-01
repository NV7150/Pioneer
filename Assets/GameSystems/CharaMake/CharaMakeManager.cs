using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Parameter;
using Character;
using SelectView;

namespace CharaMake {
    public class CharaMakeManager : MonoBehaviour {
        /// <summary> 選択された職業 </summary>
        private Job choseJob;
        /// <summary> 選択された人間性 </summary>
        private Humanity choseHumanity;
        /// <summary> 選択された特徴のリスト </summary>
        private List<Identity> choseIdentities = new List<Identity>();
        /// <summary> 入力された名前 </summary>
        private string name;

        /// <summary> 選択できる職業のリスト </summary>
        private List<Job> jobs = new List<Job>();
        /// <summary> 選択できる人間性のリスト </summary>
        private List<Humanity> humanities = new List<Humanity>();
        /// <summary> 選択できる特徴のリスト </summary>
        private List<Identity> identities = new List<Identity>();

        //各オブジェクトのプレファブ
        private GameObject jobNodePrefab;
        private GameObject jobViewPrefab;
        private GameObject humanityNodePrefab;
        private GameObject identityNodePrefab;
        private GameObject parameterViewPrefab;
        private GameObject selectViewPrefab;
        private GameObject resultViewPrefab;

        /// <summary> アクティブなセレクトビューコンテナ </summary>
        private SelectViewContainer selectView;
        /// <summary> アクティブな職業のセレクトビュー </summary>
        private SelectView<JobNode, Job> jobSelectView;
        /// <summary> アクティブな人間性のセレクトビュー </summary>
        private SelectView<HumanityNode, Humanity> humanitySelectView;
        /// <summary> アクティブな特徴のセレクトビュー </summary>
        private SelectView<IdentityNode, Identity> identitySelectView;
        /// <summary> アクティブな職業の表示ビュー </summary>
        private CharaMakeJobView jobView;
        /// <summary> アクティブなパラメータの表示ビュー </summary>
        private CharaMakeParameterView parameterView;

        /// <summary> 現在の設定状態 </summary>
        private CharaMakeState state;

        private void Awake() {
            jobNodePrefab = (GameObject)Resources.Load("Prefabs/JobNode");
            humanityNodePrefab = (GameObject)Resources.Load("Prefabs/HumanityNode");
            identityNodePrefab = (GameObject)Resources.Load("Prefabs/IdentityNode");
            selectViewPrefab = (GameObject)Resources.Load("Prefabs/SelectView");
            jobViewPrefab = (GameObject)Resources.Load("Prefabs/CharaMakeJobView");
            parameterViewPrefab = (GameObject)Resources.Load("Prefabs/CharaMakeParameterView");
            resultViewPrefab = (GameObject)Resources.Load("Prefabs/CharaMakeResultView");
        }

        private void Update() {
            if (Input.GetKeyDown(KeyCode.Return)) {
                decide();
            } else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.UpArrow)) {
                moveCursor();
            }
        }

        /// <summary>
        /// 決定処理
        /// </summary>
        private void decide() {
            switch (state) {
                case CharaMakeState.JOB:
                    jobChose();
                    break;
                case CharaMakeState.HUMANITY:
                    humanityChose();
                    break;
                case CharaMakeState.IDENTITY:
                    identityChose();
                    break;
            }
        }

        /// <summary>
        /// 職業の決定処理
        /// </summary>
        private void jobChose() {
            this.choseJob = jobSelectView.getElement();
            jobSelectView.delete();
            selectView.detach();
            inputHumanity();
            Destroy(jobView.gameObject);
        }

        /// <summary>
        /// 人間性の決定処理
        /// </summary>
        private void humanityChose() {
            this.choseHumanity = humanitySelectView.getElement();
            humanitySelectView.delete();
            selectView.detach();
            inputIdentity();
        }

        /// <summary>
        /// 特徴の決定処理
        /// </summary>
        private void identityChose() {
            this.choseIdentities.Add(identitySelectView.getElement());
            if (this.choseIdentities.Count >= 3) {
                identitySelectView.delete();
                selectView.detach();
                Destroy(parameterView.gameObject);
                inputResult();
            }
        }

        /// <summary>
        /// カーソルの移動移動
        /// </summary>
        private void moveCursor() {
            int axis = 0;
            float rawAxis = Input.GetAxisRaw("Vertical");
            if (rawAxis > 0) {
                axis = -1;
            } else if (rawAxis < 0) {
                axis = 1;
            }

            if (axis != 0) {
                switch (state) {
                    case CharaMakeState.JOB:
                        Job job = jobSelectView.moveTo(jobSelectView.getIndex() + axis);
                        jobView.printText(job);
                        break;
                    case CharaMakeState.HUMANITY:
                        Humanity humanity = humanitySelectView.moveTo(humanitySelectView.getIndex() + axis);
                        parameterView.printText(humanity.getName(), humanity.getDescription(), humanity.getFlavorText());
                        break;
                    case CharaMakeState.IDENTITY:
                        Identity identity = identitySelectView.moveTo(identitySelectView.getIndex() + axis);
                        parameterView.printText(identity.getName(), identity.getDescription(), identity.getFlavorText());
                        break;
                }
            }
        }

        /// <summary>
        /// データを設定します
        /// </summary>
        /// <param name="jobs">選択できる職業のリスト</param>
        /// <param name="humanities">選択できる人間性のリスト</param>
        /// <param name="identities">選択できる特徴のリスト</param>
        public void setDatas(List<Job> jobs, List<Humanity> humanities, List<Identity> identities) {
            this.jobs = jobs;
            this.humanities = humanities;
            this.identities = identities;
            Vector3 viewPos = new Vector3(200, Screen.height / 2);
            selectView = Instantiate(selectViewPrefab, viewPos, new Quaternion(0, 0, 0, 0)).GetComponent<SelectViewContainer>();
            selectView.transform.SetParent(CanvasGetter.getCanvas().transform);

            inputJob();
        }

        /// <summary>
        /// 職業選択画面を表示させます
        /// </summary>
        private void inputJob() {
            List<JobNode> jobNodes = new List<JobNode>();
            foreach (Job job in jobs) {
                JobNode jobNode = Instantiate(jobNodePrefab).GetComponent<JobNode>();
                jobNode.setJob(job);
                jobNodes.Add(jobNode);
            }
            jobSelectView = selectView.creatSelectView<JobNode, Job>(jobNodes);
            this.jobView = Instantiate(jobViewPrefab).GetComponent<CharaMakeJobView>();
            jobView.transform.SetParent(CanvasGetter.getCanvas().transform);
            state = CharaMakeState.JOB;
        }

        /// <summary>
        /// 人間性選択画面を表示させます
        /// </summary>
        private void inputHumanity() {
            List<HumanityNode> humanityNodes = new List<HumanityNode>();
            foreach (Humanity humanity in humanities) {
                HumanityNode humanityNode = Instantiate(humanityNodePrefab).GetComponent<HumanityNode>();
                humanityNode.setHumanity(humanity);
                humanityNodes.Add(humanityNode);
            }
            humanitySelectView = selectView.creatSelectView<HumanityNode, Humanity>(humanityNodes);
            this.parameterView = Instantiate(parameterViewPrefab).GetComponent<CharaMakeParameterView>();
            parameterView.transform.SetParent(CanvasGetter.getCanvas().transform);
            state = CharaMakeState.HUMANITY;
        }

        /// <summary>
        /// 特徴選択画面を表示させます
        /// </summary>
        private void inputIdentity() {
            List<IdentityNode> identityNodes = new List<IdentityNode>();
            foreach (Identity identity in identities) {
                IdentityNode identityNode = Instantiate(identityNodePrefab).GetComponent<IdentityNode>();
                identityNode.setIdentity(identity);
                identityNodes.Add(identityNode);
            }
            identitySelectView = selectView.creatSelectView<IdentityNode, Identity>(identityNodes);
            state = CharaMakeState.IDENTITY;
        }

        /// <summary>
        /// 結果画面を表示させます
        /// </summary>
        private void inputResult() {
            Vector3 viewPos = new Vector3(Screen.width / 2, Screen.height / 2);

            CharaMakeResultView resultView = Instantiate(resultViewPrefab, viewPos, new Quaternion(0, 0, 0, 0)).GetComponent<CharaMakeResultView>();
            Destroy(selectView.gameObject);
            resultView.transform.SetParent(CanvasGetter.getCanvas().transform);
            resultView.setParameters(choseJob, choseHumanity, choseIdentities, this);
        }

        /// <summary>
        /// 名前が入力された時の処理
        /// </summary>
        /// <param name="name">Name.</param>
        public void nameInputed(string name) {
            this.name = name;
        }

        /// <summary>
        /// キャラクターを生成します
        /// </summary>
        /// <returns>生成されたキャラクター</returns>
        public Hero makeCharacter() {
            return new Hero(choseJob, choseHumanity, choseIdentities, null);
        }

        private enum CharaMakeState {
            JOB, HUMANITY, IDENTITY, MISSION, RESULT
        }
    }
}