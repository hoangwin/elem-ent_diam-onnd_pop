using UnityEngine;
using System.Collections;

public class ScriptHint : MonoBehaviour {
    
	// Use this for initialization
    public static ScriptHint instance;
	void Start () {
        instance = this;
       
	}

    public void init()
    {
        if (GamePlay.mcurrentlevel == 0)
        {
            gameObject.SetActive(true);
            gameObject.transform.position = GamePlay.instance.mapArray[4][4].gameObject.transform.position;
            Vector3 temp = GamePlay.instance.mapArray[4][1].gameObject.transform.position;
            iTween.MoveFrom(gameObject, iTween.Hash("x", temp.x, "time", 1.5, "EaseType", "linear","oncomplete", "Movecompleted"));

        }
        else
        {
            gameObject.SetActive(false);
        }
    }
    public void Movecompleted()
    {
        gameObject.transform.position = GamePlay.instance.mapArray[4][4].gameObject.transform.position;
        Vector3 temp = GamePlay.instance.mapArray[4][1].gameObject.transform.position;
        iTween.MoveFrom(gameObject, iTween.Hash("x", temp.x, "time", 1.5, "EaseType", "linear", "oncomplete", "Movecompleted"));
    }
	// Update is called once per frame
	void Update () {
	
	}
   
}
