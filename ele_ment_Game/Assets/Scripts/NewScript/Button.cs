using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void ButtonLeft()
    {
       // isCompleted = false;
       // DEF.playSounBack(this);
        GamePlay.mcurrentlevel--;
        // Application.LoadLevel("SceneHint");
       GamePlay.instance.PlayGame();
    }
    public void ButtonRight()
    {
        // isCompleted = false;
        // DEF.playSounBack(this);
        GamePlay.mcurrentlevel++;
        // Application.LoadLevel("SceneHint");
        GamePlay.instance.PlayGame();
    }
    public void ButtonReset()
    {
        // isCompleted = false;
        // DEF.playSounBack(this);
        //GameEngine.mcurrentlevel++;
        // Application.LoadLevel("SceneHint");
        GamePlay.instance.PlayGame();
    }
    public void ButtonUndo()
    {
        Undo.instance.UndoAStep();
        // isCompleted = false;
        // DEF.playSounBack(this);
        //GameEngine.mcurrentlevel++;
        // Application.LoadLevel("SceneHint");
      //  GamePlay.instance.PlayGame();
    }
}
