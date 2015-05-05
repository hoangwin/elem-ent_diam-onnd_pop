using UnityEngine;
using System.Collections;

public class ScriptGameplay : MonoBehaviour {

	// Use this for initialization
    public GameObject frefabItem;    
    GameEngine gameEngine;
    public Texture topBar;
    public Texture fullProgressBar;
    public Texture emptyProgressBar;

    public Texture ItemEffect1;
    public Texture ItemEffect2;
    public Texture ItemEffect3;
    

    public Texture Dialog;

    public GUISkin IGMSkin;
    public GUISkin ButtonNormalSkin;

    public GUISkin ButtonMainmenu;
    public GUISkin ButtonReplay;
    public GUISkin ButtonNext;
    public GUISkin ButtonHighScore;



    public static bool isIGM;
    public static bool isCompleted;
   
	void Start () {
        DEF.init();
        //gameEngine = new GameEngine();
        GameEngine.targetScore = getTargetScore();
        gameObject.AddComponent<GameEngine>();
        gameEngine = (GameEngine)gameObject.GetComponent("GameEngine");
        GameEngine.frefabItem = frefabItem;
        isIGM = false;
        isCompleted = false;
        GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(DEF.scaleX, DEF.scaleY, 1));
    }

    void PlayGame()
    {
        gameEngine.Start();
        isIGM = false;
        isCompleted = false;
    }
    public static int getTargetScore()
    {
        return (GameEngine.mcurrentlevel +1)* 1000 + 500;
    }
	// Update is called once per frame
	void Update () {
        if (!isIGM && !isCompleted)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                isIGM = true;
            }
        }
        else if (isIGM)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                isIGM = false;
            }
        }
        else if (isCompleted)
        {
             if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.LoadLevel("SceneMainMenu");
            }
        }

       // gameEngine.Update();
	}
    
	void OnGUI()
	{
        GUI.skin = IGMSkin;
        GUI.skin.label.fontSize = (int)(50 * DEF.scaleY);
        GUI.skin.label.normal.textColor = Color.yellow;

        GUI.DrawTexture(new Rect(0, 0, Screen.width+10,140 * DEF.scaleY), topBar);

        GUI.Label(new Rect(0 * DEF.scaleX, 0, 200 * DEF.scaleY, 120 * DEF.scaleY), "Score\n" + GameEngine.score);
        if(GameEngine.gameMode ==0)
            GUI.Label(new Rect(600 * DEF.scaleX, 0, 200 * DEF.scaleY, 120 * DEF.scaleY), "Target\n" + GameEngine.targetScore);

        if(GameEngine.stateInGamePlay == GameEngine.STATE_DROP ||  GameEngine.stateInGamePlay == GameEngine.STATE_WAITING_START)
        {

            if(GameEngine.strEffect.Length > 0)
                GUI.Label(new Rect(GameEngine.effectScore_X - 60, GameEngine.effectScore_Y- 60, 120, 120), "+" + GameEngine.strEffect);
            GameEngine.effectScore_Y = GameEngine.effectScore_Y-1*DEF.scaleY;
            
        }
        //if (GameEngine.stateInGamePlay == GameEngine.STATE_IDE || GameEngine.stateInGamePlay == GameEngine.STATE_DROP)
        {
            for (int i = 0; i < GameEngine.MAX_ROW; i++)
            {
                for (int j = 0; j < GameEngine.MAX_COL; j++)
                {
                    if (GameEngine.tableArray[i][j].specialType == 0)
                    {
                        GUI.DrawTexture(new Rect(GameEngine.tableArray[i][j].currentX - GameEngine.ITEM_WIDTH/2,
                            GameEngine.tableArray[i][j].currentY - GameEngine.ITEM_HEIGHT/2, GameEngine.ITEM_WIDTH, GameEngine.ITEM_HEIGHT), ItemEffect1);
                    }
                    else if (GameEngine.tableArray[i][j].specialType == 1)
                    {
                        GUI.DrawTexture(new Rect(GameEngine.tableArray[i][j].currentX - GameEngine.ITEM_WIDTH / 2,
                            GameEngine.tableArray[i][j].currentY - GameEngine.ITEM_HEIGHT / 2, GameEngine.ITEM_WIDTH, GameEngine.ITEM_HEIGHT), ItemEffect2);
                    }
                    else if (GameEngine.tableArray[i][j].specialType == 2)
                    {
                        GUI.DrawTexture(new Rect(GameEngine.tableArray[i][j].currentX - GameEngine.ITEM_WIDTH / 2,
                            GameEngine.tableArray[i][j].currentY - GameEngine.ITEM_HEIGHT / 2, GameEngine.ITEM_WIDTH, GameEngine.ITEM_HEIGHT), ItemEffect3);
                    }
                }
            }
        }

        if (GameEngine.gameMode == 0)
        {
            int percent = (GameEngine.score * 100 / GameEngine.targetScore);
            if (percent > 100)
                percent = 100;
           
            float width = 700 * DEF.scaleX;
            width = width - width * (100.0f - percent) / 100;
            GUI.DrawTexture(new Rect(50 * DEF.scaleX, 1100 * DEF.scaleY, 700 * DEF.scaleX, 70 * DEF.scaleY), fullProgressBar);
            GUI.DrawTexture(new Rect(50 * DEF.scaleX, 1100f * DEF.scaleY, width, 70f * DEF.scaleY), emptyProgressBar);
            GUI.skin.label.normal.textColor = Color.white;
            GUI.Label(new Rect(0, 1100 * DEF.scaleY, Screen.width, 60f * DEF.scaleY), percent + "%");
        }
        else if (GameEngine.gameMode == 1)
        {

            float percent = (GameEngine.timer * 100 / GameEngine.MAX_TIMER);
            if (percent > 100)
                percent = 100;
            float width = 700 * DEF.scaleX;
            width = width - width * (100.0f - percent) / 100;
            GUI.DrawTexture(new Rect(50 * DEF.scaleX, 1100 * DEF.scaleY, 700 * DEF.scaleX, 70 * DEF.scaleY), fullProgressBar);
            GUI.DrawTexture(new Rect(50 * DEF.scaleX, 1100f * DEF.scaleY, width, 70f * DEF.scaleY), emptyProgressBar);
            GUI.skin.label.normal.textColor = Color.white;
            GUI.Label(new Rect(0, 1100 * DEF.scaleY, Screen.width, 60f * DEF.scaleY), "Time");
            
        }

        if (isCompleted)
        {
            //GUIStyle myStyle;
            //myStyle = ButtonNormalSkin.GetStyle("Label");
            //myStyle.fontSize = (int)(60 * DEF.scaleY);
            GUI.skin.label.fontSize = (int)(80 * DEF.scaleY);
            GUI.skin.label.normal.textColor = Color.yellow;
            string str = "COMPLETED";

            GUI.DrawTexture(new Rect(0 * DEF.scaleX, 300 * DEF.scaleY, Screen.width, 600 * DEF.scaleY), Dialog);

            GUI.Label(new Rect(0, 330 * DEF.scaleY, 800 * DEF.scaleX, 120 * DEF.scaleY), str);

            GUI.skin.label.normal.textColor = Color.white;
            if(GameEngine.gameMode ==0)
                GUI.Label(new Rect(0, 500 * DEF.scaleY, 800 * DEF.scaleX, 120 * DEF.scaleY), "SCORE " + GameEngine.score);
            else
                GUI.Label(new Rect(0, 500 * DEF.scaleY, 800 * DEF.scaleX, 120 * DEF.scaleY), "SCORE " + GameEngine.score);

            GUI.skin = ButtonMainmenu;

            if (GUI.Button(new Rect(120 * DEF.scaleX, 700 * DEF.scaleY, 160 * DEF.scaleX, 160 * DEF.scaleY), " "))
            {//main menu
                DEF.playSounBack(this);
                Application.LoadLevel("SceneMainMenu");
            }

            GUI.skin = ButtonReplay;

            if (GUI.Button(new Rect(320 * DEF.scaleX, 700 * DEF.scaleY, 160 * DEF.scaleX, 160 * DEF.scaleY), " "))
            {//replay
                DEF.playSounBack(this);
                PlayGame();
            }
         
            if (GameEngine.gameMode == 0)
            {

                GUI.skin = ButtonNext;
                if (GUI.Button(new Rect(520 * DEF.scaleX, 700 * DEF.scaleY, 160 * DEF.scaleX, 160 * DEF.scaleY), " "))
                {
                    //next
                    DEF.playSounBack(this);
                    GameEngine.mcurrentlevel++;
                    Application.LoadLevel("SceneHint");
                    //PlayGame();
                }
            }
            else
            {
                GUI.skin = ButtonHighScore;
                if (GUI.Button(new Rect(520 * DEF.scaleX, 700 * DEF.scaleY, 160 * DEF.scaleX, 160 * DEF.scaleY), " "))
                {
                    //next
                    DEF.playSounBack(this);                   
                    Application.LoadLevel("SceneHighScore");
                    //PlayGame();
                }
            }


        }


      //  GUI.skin.label.alignment = TextAnchor.MiddleCenter;
      //  GUI.Label(Rect(0, 0, 100, 50), string.Format("{0:N0}%", async.progress * 100f));

        if (!isIGM && !isCompleted)
        {
            GUI.skin = IGMSkin;
            if (GUI.Button(new Rect(Screen.width / 2 - 70 * DEF.scaleX, 0 * DEF.scaleY, 140 * DEF.scaleX, 140 * DEF.scaleY), " "))
            {
                DEF.playSounBack(this);
                isIGM = true;
            }
        }
        else if (isIGM)
        {
          //  DrawQuad(new Rect(0,0,Screen.width,Screen.height),new Color(0,0,0,20));

            GUI.skin = ButtonNormalSkin;
            if (GUI.Button(new Rect(Screen.width / 2 - 240 * DEF.scaleX, (200 + 1 * 140) * DEF.scaleY, 480 * DEF.scaleX, 140 * DEF.scaleY), "Resume"))
            {
                DEF.playSounBack(this);
                isIGM = false;
                //Application.LoadLevel("SceneSelectLevel");
            }

            if (GUI.Button(new Rect(Screen.width / 2 - 240 * DEF.scaleX, (200 + 2 * 140) * DEF.scaleY, 480 * DEF.scaleX, 140 * DEF.scaleY), "Restart Game"))
            {
                DEF.playSounBack(this);
                PlayGame();
            }
            if(GUI.Button(new Rect(Screen.width / 2 - 240 * DEF.scaleX, (200 + 3 * 140) * DEF.scaleY, 480 * DEF.scaleX, 140 * DEF.scaleY), "Main Menu"))
            {
                DEF.playSounBack(this);
                Application.LoadLevel("SceneMainMenu");
            }
            if (GUI.Button(new Rect(Screen.width / 2 - 240 * DEF.scaleX, (200 + 4 * 140) * DEF.scaleY, 480 * DEF.scaleX, 140 * DEF.scaleY), "Exit Game"))
            {
                Debug.Log("quit");
                Application.Quit();
            }
        }

       
	}

    void DrawQuad(Rect position, Color color)
    {
        Texture2D texture = new Texture2D(1, 1);
        texture.SetPixel(0, 0, color);
        texture.Apply();
        GUI.skin.box.normal.background = texture;
        GUI.Box(position, GUIContent.none);
    }
}
