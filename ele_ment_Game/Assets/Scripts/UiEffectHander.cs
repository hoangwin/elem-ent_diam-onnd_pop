using UnityEngine;
using System.Collections;

public class UiEffectHander : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void hideCompleted()
	{
		UIEffect.instance.uiCompleted.gameObject.SetActive(false);

	}
	public void selectLevelClickCompleted()
	{
		Application.LoadLevel("SceneGamePlay");
	}
	public void selectLevelChangePageCompleted()
	{
		//Debug.Log ("aaa");
		ScriptSelectLevel.instance.Init ();
	}


    //mainmenu

    public void MainMenuMoveEndCompleted()
    {
        Debug.Log("tententen");
        Application.LoadLevel("SceneSelectLevel");
    }
}
