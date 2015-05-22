using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Button : MonoBehaviour {

	// Use this for initialization
    public Image buttonSound;
    public Sprite buttonOn;
    public Sprite buttonOff;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void ButtonLeft()
    {
       // isCompleted = false;
        SoundEngine.instance.PlayOneShot(SoundEngine.instance._soundclick);
        GamePlay.mcurrentlevel--;
        if (GamePlay.mcurrentlevel < 0)
            GamePlay.mcurrentlevel = 99;
         
       GamePlay.instance.PlayGame();
    }
    public void ButtonRight()
    {
        // isCompleted = false;
        SoundEngine.instance.PlayOneShot(SoundEngine.instance._soundclick);
        GamePlay.mcurrentlevel++;
        if (GamePlay.mcurrentlevel >= 100)
            GamePlay.mcurrentlevel = 0;       
        // Application.LoadLevel("SceneHint");
        GamePlay.instance.PlayGame();
    }
    public void ButtonReset()
    {
        SoundEngine.instance.PlayOneShot(SoundEngine.instance._soundclick);
        GamePlay.instance.PlayGame();
    }
    public void ButtonUndo()
    {
        SoundEngine.instance.PlayOneShot(SoundEngine.instance._soundclick);
        Undo.instance.UndoAStep();
        
    }
	public void ButtonPlayClassic()
    {
        ScriptMainMenu.instance.effectEnd();
        SoundEngine.instance.PlayOneShot(SoundEngine.instance._soundclick);
		GamePlay.GameMode = 0;
		//Application.LoadLevel("SceneSelectLevel");

	}
	public void ButtonPlayExtra()
	{
        ScriptMainMenu.instance.effectEnd();
        SoundEngine.instance.PlayOneShot(SoundEngine.instance._soundclick);
		GamePlay.GameMode = 1;
		//Application.LoadLevel("SceneSelectLevel");

	}

    public void ButtonMainmenu()
    {
        SoundEngine.instance.PlayOneShot(SoundEngine.instance._soundclick);
        Application.LoadLevel("SceneMainMenu");
    }
    public void ButtonSound()
    {
        SoundEngine.isSoundSFX = !SoundEngine.isSoundSFX;
        if(SoundEngine.isSoundSFX)
        {
            buttonSound.sprite = buttonOn;
        }
        else
        {
            buttonSound.sprite = buttonOff;
        }
        SoundEngine.instance.PlayOneShot(SoundEngine.instance._soundclick);
        
    }
    public void ButtonRate()
    {
        SoundEngine.instance.PlayOneShot(SoundEngine.instance._soundclick);
#if UNITY_ANDROID
            Application.OpenURL("market://details?id=com.unblock.flow.game");	
            
#elif UNITY_WP8
        WP8Statics.RateApp("");
#elif UNITY_IOS
        Application.OpenURL("https://itunes.apple.com/us/app/bubble-shoot-free/id914220826?ls=1&mt=8");	
        
         //   IOsStatic.ShowAds(" ", " ");#endif
        	
#endif	
    }
    public void ButtonQuit()
    {
        Application.Quit();
    //    Application.LoadLevel("SceneMainMenu");
    }
}
