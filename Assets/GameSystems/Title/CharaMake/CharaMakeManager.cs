using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Parameter;
using Character;
using Quest;
using SelectView;
using MasterData;

namespace CharaMake {
    public class CharaMakeManager : MonoBehaviour {
        /// <summary> 選択された職業 </summary>
        private Job choseJob;
        /// <summary> 選択された人間性 </summary>
        private Humanity choseHumanity;
        /// <summary> 選択された特徴のリスト </summary>
        private List<Identity> choseIdentities = new List<Identity>();
        private IMissionBuilder choseMission;
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
        private GameObject missionNodePrefab;

        /// <summary> アクティブなセレクトビューコンテナ </summary>
        private SelectViewContainer selectView;
        /// <summary> アクティブな職業のセレクトビュー </summary>
        private SelectView<JobNode, Job> jobSelectView;
        /// <summary> アクティブな人間性のセレクトビュー </summary>
        private SelectView<HumanityNode, Humanity> humanitySelectView;
        /// <summary> アクティブな特徴のセレクトビュー </summary>
        private SelectView<IdentityNode, Identity> identitySelectView;
        private SelectView<MissionNode, IMissionBuilder> missionSelectView;
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
            missionNodePrefab = (GameObject)Resources.Load("Prefabs/missionNode");
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
                case CharaMakeState.MISSION:
                    missionChose();
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
                inputMission();
            }
        }

        public void missionChose(){
            this.choseMission = missionSelectView.getElement();
            missionSelectView.delete();
			selectView.detach();
			Destroy(parameterView.gameObject);
			inputResult();
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
                        int index = jobSelectView.getIndex() + axis;
                        Job job = jobSelectView.moveTo(index);
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

                    case CharaMakeState.MISSION:
                        IMissionBuilder mission = missionSelectView.moveTo(missionSelectView.getIndex() + axis);
                        parameterView.printText(mission.getName(),mission.getDescription(),mission.getFlavorText());
                        break;
                }
            }
        }

        /// <summary>
        /// データを設定します
        /// </summary>
        /// <param name="level">世界のレベル</param>
        public void setDatas(int level) {
            this.jobs = JobMasterManager.getJobsFromLevel(level);
            this.humanities = HumanityMasterManager.getHumanitiesFromLevel(level);
            this.identities = IdentityMasterManager.getIdentitiesFromLevel(level);
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

            Vector3 viewPos = new Vector3(712f, Screen.height / 2);
			this.jobView = Instantiate(jobViewPrefab, viewPos, new Quaternion(0, 0, 0, 0)).GetComponent<CharaMakeJobView>();

            jobView.printText(jobSelectView.getElement());

            Debug.Log(jobSelectView + " " + jobSelectView.getIndex());

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

            Vector3 viewPos = new Vector3(712f, Screen.height / 2);
            this.parameterView = Instantiate(parameterViewPrefab,viewPos,new Quaternion(0,0,0,0)).GetComponent<CharaMakeParameterView>();

            parameterView.transform.SetParent(CanvasGetter.getCanvas().transform);

            var printHumanity = humanitySelectView.getElement();
			parameterView.printText(printHumanity.getName(), printHumanity.getDescription(), printHumanity.getFlavorText());

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

            var printIdentity = identitySelectView.getElement();
            parameterView.printText(printIdentity.getName(),printIdentity.getDescription(),printIdentity.getFlavorText());

            state = CharaMakeState.IDENTITY;
        }

        private void inputMission(){
            int charamakeLevel = 0;
            foreach(Identity identity in choseIdentities){
                charamakeLevel += identity.getLevel();
            }
            charamakeLevel += choseJob.getLevel();
            charamakeLevel += choseHumanity.getLevel();

            List<MissionNode> missionNodes = new List<MissionNode>();
            for (int i = 0; i < 5;i++){
                Debug.Log("rooped");
                IMissionBuilder builder = QuestHelper.getRandomMission(charamakeLevel);
                var missionNode = Instantiate(missionNodePrefab).GetComponent<MissionNode>();
                missionNode.setQuest(builder);
                missionNodes.Add(missionNode);
            }
            missionSelectView = selectView.creatSelectView<MissionNode, IMissionBuilder>(missionNodes);

            var printMission = missionSelectView.getElement();
            parameterView.printText(printMission.getName(),printMission.getDescription(),printMission.getFlavorText());

            state = CharaMakeState.MISSION;
        }

        /// <summary>
        /// 結果画面を表示させます
        /// </summary>
        private void inputResult() {
            Vector3 viewPos = new Vector3(Screen.width / 2, Screen.height / 2);

            CharaMakeResultView resultView = Instantiate(resultViewPrefab, viewPos, new Quaternion(0, 0, 0, 0)).GetComponent<CharaMakeResultView>();
            Destroy(selectView.gameObject);
            resultView.transform.SetParent(CanvasGetter.getCanvas().transform);
            resultView.setParameters(choseJob, choseHumanity, choseIdentities ,choseMission, this);

            state = CharaMakeState.RESULT;
        }

        /// <summary>
        /// 名前が入力された時の処理
        /// </summary>
        /// <param name="name">Name.</param>
        public void nameInputed(string name) {
            this.name = name;

            WorldCreator.getInstance().setPlayer(makeCharacter());

            finishCharaMake();
        }

        /// <summary>
        /// キャラクターを生成します
        /// </summary>
        /// <returns>生成されたキャラクター</returns>
        public Player makeCharacter() {
            return new Player(choseJob, choseHumanity, choseIdentities,choseMission, name);
        }

        public void finishCharaMake(){
            SceneManager.LoadScene("FieldScene");
        }

        private enum CharaMakeState {
            JOB, HUMANITY, IDENTITY, MISSION, RESULT
        }
    }
}