using UnityEngine;
using System.Collections;

public class ScriptMainMenu : MonoBehaviour {

	// Use this for initialization
	
    public GUISkin skinButtonNormal;
    public GUISkin skinButtonSoundOn;
    public GUISkin skinButtonSoundOff;
    public GUISkin skinButtonHelp;
    public GUISkin skinButtonExit;
    //public AudioClip soundBack;
    //public Texture dialogTextture;
    void Start()
    {
        
        loadGame();
        DEF.init();
        GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity,new  Vector3(DEF.scaleX, DEF.scaleY, 1));
     
	}
	 
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
	}
    public static void saveGame()
    {
        
        PlayerPrefs.SetInt(DEF.STRING_UNLOCK_LEVEL, GameEngine.mUnlocklevel);
      //  PlayerPrefs.SetInt("USER_TOPLEVEL", GameEngine.mTopScore);
        PlayerPrefs.SetString("USER_NAME", GameEngine.username);
        PlayerPrefs.Save();
    }
    public static void loadGame()
    {
        GameEngine.mUnlocklevel = PlayerPrefs.GetInt(DEF.STRING_UNLOCK_LEVEL);
        //if (GameEngine.mUnlocklevel < 95) GameEngine.mUnlocklevel = 95;
      //  GameEngine.mTopScore = PlayerPrefs.GetInt("USER_TOPLEVEL");
        GameEngine.username = PlayerPrefs.GetString("USER_NAME");
    }

	void OnGUI()
    {
        GUI.skin = skinButtonNormal;
        skinButtonNormal.button.fontSize = (int)(60 * DEF.scaleY);
        skinButtonNormal.label.fontSize = (int)(60 * DEF.scaleY);
      /*  if (DEF.isNeedSoundQuestion)
        {
            GUI.DrawTexture(new Rect(0 * DEF.scaleX, 300 * DEF.scaleY, Screen.width, 600 * DEF.scaleY), dialogTextture);
            GUI.Label(new Rect(100 * DEF.scaleX, 400 * DEF.scaleY, 600 * DEF.scaleX, 300 * DEF.scaleY),  "Do You want Play sound in game?");

            if (GUI.Button(new Rect( 100 * DEF.scaleX, 700* DEF.scaleY, 250 * DEF.scaleX, 120 * DEF.scaleY), "Yes"))
            {
                //soundBack.PlayOneShot();
                DEF.isNeedSoundQuestion = true;
              
            }
            if (GUI.Button(new Rect(480* DEF.scaleX, 700 * DEF.scaleY, 250 * DEF.scaleX, 120 * DEF.scaleY), "No"))
            {
                //soundBack.PlayOneShot();
                DEF.isNeedSoundQuestion = false;
               
            }
            return;
        }
       * */
      
        GUI.skin = skinButtonNormal;
        if (GUI.Button(new Rect(Screen.width / 2 - 240 * DEF.scaleX, (400 + 1 * 140) * DEF.scaleY, 480 * DEF.scaleX, 140 * DEF.scaleY), "Play"))
        {
			//soundBack.PlayOneShot();
           
            GameEngine.gameMode = 0;
            Application.LoadLevel("SceneSelectLevel");
            DEF.playSounBack(this);
        }
        //GUI.skin = a;
        /*
        if (GUI.Button(new Rect(Screen.width / 2 - 240 * DEF.scaleX, (360 + 2 * 140) * DEF.scaleY, 480 * DEF.scaleX, 140 * DEF.scaleY), "Challenge"))
        {
            GameEngine.gameMode = 1;
            Application.LoadLevel("SceneHint");
            DEF.playSounBack(this);
        }
         */

        if (DEF.isSoundEnable)
            GUI.skin = skinButtonSoundOn;
        else
            GUI.skin = skinButtonSoundOff;
        if (GUI.Button(new Rect(Screen.width / 2 - 360 * DEF.scaleX, (830) * DEF.scaleY, 180 * DEF.scaleX, 180 * DEF.scaleY), " "))
        {
            DEF.isSoundEnable = !DEF.isSoundEnable;
            GameObject gameMusic = GameObject.Find("BGMusic");
            if(DEF.isSoundEnable)
            {   
                  if (gameMusic) {
                      if (!gameMusic.GetComponent<AudioSource>().isPlaying)
                            gameMusic.GetComponent<AudioSource>().Play();
                  }
            
             }
                  else
                  {

                  if (gameMusic) {
                      if (gameMusic.GetComponent<AudioSource>().isPlaying)
                            gameMusic.GetComponent<AudioSource>().Stop();
                  }
            }

          
        }

        GUI.skin = skinButtonHelp;
        if (GUI.Button(new Rect(Screen.width / 2 - 90 * DEF.scaleX, (830) * DEF.scaleY, 180 * DEF.scaleX, 180 * DEF.scaleY), " "))
        {
            Application.LoadLevel("SceneHelp");
            DEF.playSounBack(this);
        }

        GUI.skin = skinButtonExit;
        if (GUI.Button(new Rect(Screen.width / 2 + 180 * DEF.scaleX, (830) * DEF.scaleY, 180 * DEF.scaleX, 180 * DEF.scaleY), " "))
        {
            Application.Quit();
            //DEF.playSounBack(this);
        }
      //  Debug.Log("SIZE   :" +  DEF.scaleX);
        
	
	}
    void Awake()
    {

      Debug.Log("TOAN STT");
      GameObject gameMusic = GameObject.Find("BGMusic");
        
      if (gameMusic) {
          if (!gameMusic.GetComponent<AudioSource>().isPlaying)
              if (DEF.isSoundEnable)
                gameMusic.GetComponent<AudioSource>().Play();
            // kill game music
      //  Destroy(gameMusic);
       }
       DontDestroyOnLoad(gameMusic);

       GameObject selectSound = GameObject.Find("EffectMusic");
       //if (selectSound)
       //{
       // kill game music
       //  Destroy(selectSound);
       //}
       DontDestroyOnLoad(selectSound);
    }
	
}
