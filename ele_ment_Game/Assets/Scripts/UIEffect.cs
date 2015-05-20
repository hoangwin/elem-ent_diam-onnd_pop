using UnityEngine;
using System.Collections;

public class UIEffect : MonoBehaviour {

    public Transform uiGamePlay;

    public Transform uiCompleted;
    public Transform uiCompletedSub;
    public Transform uiStarEffect;
	// Use this for initialization
    public static UIEffect instance;
	void Start () {
        instance = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void showCOmpleted()
    {
        UIEffect.instance.uiCompleted.gameObject.SetActive(true);
        UIEffect.instance.uiCompletedSub.position = new Vector3(Screen.width / 2, Screen.height / 2, UIEffect.instance.uiCompleted.position.z);
        iTween.MoveFrom(UIEffect.instance.uiCompletedSub.gameObject, iTween.Hash("y", 3 * Screen.height / 2, "time", 1, "EaseType", "linear"));//"oncomplete", "Movecompleted"
        iTween.RotateTo(UIEffect.instance.uiStarEffect.gameObject, iTween.Hash("rotation", new Vector3(0, 00, 900), "easetype", iTween.EaseType.linear, "time", 7.0f));
		GamePlay.timer = 0;
    }
    public void movehideCompleted()
    {

        UIEffect.instance.uiCompletedSub.position = new Vector3(Screen.width / 2, Screen.height / 2, UIEffect.instance.uiCompleted.position.z);
        iTween.MoveTo(UIEffect.instance.uiCompletedSub.gameObject, iTween.Hash("x", 2 * Screen.width, "time", 1, "EaseType", "linear", "oncomplete", "hideCompleted"));//"oncomplete", "Movecompleted"
		GamePlay.mcurrentlevel++;
		GamePlay.instance.PlayGame();
     //   iTween.RotateTo(UIEffect.instance.uiStarEffect.gameObject, iTween.Hash("rotation", new Vector3(0, 00, 500), "easetype", iTween.EaseType.linear, "time", 3.3f));
    }
   
}
