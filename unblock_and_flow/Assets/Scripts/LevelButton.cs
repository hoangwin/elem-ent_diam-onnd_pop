using UnityEngine;
using System.Collections;

public class LevelButton : MonoBehaviour {

	// Use this for initialization
	public UnityEngine.UI.Image image;
	public UnityEngine.UI.Text text;
	public int index;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void click()
	{
		int tempUnblock = 0;
		if(GamePlay.GameMode ==0)
			tempUnblock = DEF_.ScoreModeClassic.NUM;
		else
			tempUnblock = DEF_.ScoreModeExtra.NUM;
		if (index <= tempUnblock) {
            SoundEngine.instance.PlayOneShot(SoundEngine.instance._soundclick);
			
						ScriptSelectLevel.instance.colorFadeEfect.enabled = true;
						ScriptSelectLevel.instance.color.a = (1);
						GamePlay.mcurrentlevel = index;
						ScriptSelectLevel.instance.efectClick ();
				}
		//Application.LoadLevel("SceneGamePlay");
	}
}
