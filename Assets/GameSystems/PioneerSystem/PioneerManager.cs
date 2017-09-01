using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PioneerManager{
    private readonly static PioneerManager INSTANCE = new PioneerManager();

    private List<IObserver> observers = new List<IObserver>();

    private List<IObserver> waitForRemoves = new List<IObserver>();

    private GameObject resultViewPrefab;

    private PioneerManager(){
        resultViewPrefab = (GameObject)Resources.Load("Prefabs/ResultView");
    }

    public static PioneerManager getInstance(){
        return INSTANCE;
    }

    public void missionClearPrint(){
        var view = inputResultView();
        view.setText("使命達成!");
    }

	public void deathPrint() {
		var view = inputResultView();
		view.setText("ゲームオーバー!");
    }

	private ResultView inputResultView() {
		var resultView = MonoBehaviour.Instantiate(resultViewPrefab);
		resultView.transform.SetParent(CanvasGetter.getCanvasElement().transform);
		resultView.transform.position = new Vector3(Screen.width / 2, Screen.height / 2);
        return resultView.GetComponent<ResultView>();
    }

	public void finished() {
        foreach(IObserver observer in observers){
            observer.report(WorldCreator.getInstance().getLoadWorldId());
            observer.reset();
        }
        foreach(IObserver observer in waitForRemoves){
            observers.Remove(observer);
        }
        waitForRemoves.Clear();

        SceneKeeper.deleteScene();
    }

	public void retire() {
		foreach (IObserver observer in observers) {
			observer.reset();
		}
		SceneKeeper.deleteScene();
        UnityEngine.SceneManagement.SceneManager.LoadScene("Title");
    }

    public void setObserver(IObserver observer){
        observers.Add(observer);
    }

    public void removeObserver(IObserver observer){
        waitForRemoves.Add(observer);
    }
}
