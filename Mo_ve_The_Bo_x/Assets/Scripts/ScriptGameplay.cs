using UnityEngine;
using System.Collections;

public class ScriptGameplay : MonoBehaviour {

	// Use this for initialization
    public GameObject frefabItem;    
	
    GameEngine gameEngine;    


    public Texture WallTextture;
    public Texture Dialog;
    public Texture Blocked_BackGround;
    public Texture Blocked;
    public Texture Finger;
    public Texture Trophy;
    public GUISkin IGMSkin;
    public GUISkin ButtonNormalSkin;

    public GUISkin ButtonMainmenu;
    public GUISkin ButtonReplay;
    public GUISkin ButtonPlayNext;
    
    public GUISkin guiSkinSelectLevelNext;
    public GUISkin guiSkinSelectLevelBack;
    public GUISkin guiSkinLabel;
    public GUISkin ButtonUndo;


    public static bool isIGM;
   
    
    public static bool isCompleted;

    public static bool isHint = true;
    public static float mHintx = 0;
    public static float mHinty = 0;
    public static float mHintBeginx = 0;
    public static float mHintBeginy = 0;
    public static float mHintEndx = 0;
    public static float mHintEndy = 0;
    public static int mHintCount = 0;

	void Start () {
        DEF.init();
        //gameEngine = new GameEngine();
       // GameEngine.targetScore = getTargetScore();
        gameObject.AddComponent<GameEngine>();
        gameEngine = (GameEngine)gameObject.GetComponent("GameEngine");
        GameEngine.frefabItem = frefabItem;
     //   PlayGame();
        GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(DEF.scaleX, DEF.scaleY, 1));
        if (GameEngine.mcurrentlevel == 0)
        {
            isHint = true;
            mHintBeginx = mHintx = 110 * DEF.scaleX;
            mHintBeginy = mHinty = 930 * DEF.scaleY;
            mHintEndx = 600 * DEF.scaleX;
            mHintEndy = 930 * DEF.scaleY;
            mHintCount = 0;
        }
    }

    void PlayGame()
    {
        gameEngine.Start();
        isIGM = false;
        isCompleted = false;
        if (GameEngine.mcurrentlevel == 0)
        {
            isHint = true;
            mHintBeginx = mHintx = 110 * DEF.scaleX;
            mHintBeginy = mHinty = 930 * DEF.scaleY;
            mHintEndx = 600 * DEF.scaleX;
            mHintEndy = 930 * DEF.scaleY;
            mHintCount = 0;
        }
        else
        {
            isHint = false;
        }
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

        if(GameEngine.stateInGamePlay == GameEngine.STATE_MOVE ||  GameEngine.stateInGamePlay == GameEngine.STATE_WAITING_START)
        {

          //  if(GameEngine.strEffect.Length > 0)
          //      GUI.Label(new Rect(GameEngine.effectScore_X - 60, GameEngine.effectScore_Y- 60, 120, 120), "+" + GameEngine.strEffect);
          //  GameEngine.effectScore_Y = GameEngine.effectScore_Y-1*DEF.scaleY;
            
        }

    
        float x = 0;
        float y = 0;        
        for (int i = 0; i < GameEngine.MAX_ROW; i++)
        {
            for (int j = 0; j < GameEngine.MAX_COL; j++)
            {
                x = GameEngine.BEGIN_X + j * GameEngine.ITEM_WIDTH;
                y = GameEngine.BEGIN_Y + i * GameEngine.ITEM_HEIGHT;
                if (GameEngine.mapArray[i][j] == 0)
                {

                    GUI.DrawTexture(new Rect(x, y, GameEngine.ITEM_WIDTH, GameEngine.ITEM_HEIGHT), WallTextture);
                }
            }
        }

        if (GameEngine.mcurrentlevel > GameEngine.mUnlocklevel)
        {
            GUI.DrawTexture(new Rect(0 * DEF.scaleX, 230 * DEF.scaleY, Screen.width, 1100 * DEF.scaleY), Blocked_BackGround);
            GUI.DrawTexture(new Rect(0 * DEF.scaleX, 230 * DEF.scaleY, 400 * DEF.scaleX, 400 * DEF.scaleY), Blocked);
        }

        if (isCompleted)
        {
            //GUIStyle myStyle;
            //myStyle = ButtonNormalSkin.GetStyle("Label");
            //myStyle.fontSize = (int)(60 * DEF.scaleY);
            GUI.skin.label.fontSize = (int)(80 * DEF.scaleY);
            GUI.skin.label.normal.textColor = Color.blue;
            string str = "COMPLETED";

            GUI.DrawTexture(new Rect(0 * DEF.scaleX, 300 * DEF.scaleY, Screen.width, 600 * DEF.scaleY), Dialog);

            GUI.Label(new Rect(0, 330 * DEF.scaleY, 800 * DEF.scaleX, 120 * DEF.scaleY), str);
           // GUI.skin.label.normal.textColor = Color.white;
            //GUI.Label(new Rect(0, 500 * DEF.scaleY, 800 * DEF.scaleX, 120 * DEF.scaleY), "Level " + (GameEngine.mcurrentlevel +1));
            GUI.DrawTexture(new Rect(250 * DEF.scaleX, 450 * DEF.scaleY, 300 * DEF.scaleX, 200 * DEF.scaleY), Trophy);

            GUI.skin.label.normal.textColor = Color.white;

            GUI.skin = ButtonMainmenu;

            if (GUI.Button(new Rect(120 * DEF.scaleX, 700 * DEF.scaleY, 160 * DEF.scaleX, 110 * DEF.scaleY), " "))
            {//main menu
                DEF.playSounBack(this);
                Application.LoadLevel("SceneMainMenu");
            }

            GUI.skin = ButtonReplay;

            if (GUI.Button(new Rect(320 * DEF.scaleX, 700 * DEF.scaleY, 160 * DEF.scaleX, 110 * DEF.scaleY), " "))
            {//replay
                DEF.playSounBack(this);
                PlayGame();
            }

            if (GameEngine.gameMode == 0 && GameEngine.mcurrentlevel < ScriptDataLevel.levels.Length-1)
            {

                GUI.skin = ButtonPlayNext;
                if (GUI.Button(new Rect(520 * DEF.scaleX, 680 * DEF.scaleY, 160 * DEF.scaleX, 160 * DEF.scaleY), " "))
                {
                    //next
                    isCompleted = false;
                    DEF.playSounBack(this);
                    GameEngine.mcurrentlevel++;
                   // Application.LoadLevel("SceneHint");
                    PlayGame();
                }
            }
          /*  else
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
            */

        }

        
        GUIStyle myStyle;
        myStyle = guiSkinLabel.GetStyle("Label");
        GUI.skin = guiSkinLabel; 
        GUI.Label(new Rect(Screen.width / 2 - 800 / 2 * DEF.scaleX, 120 * DEF.scaleY, 800 * DEF.scaleX, 60 * DEF.scaleY), "LEVEL " + (GameEngine.mcurrentlevel+1));

      //  GUI.skin.label.alignment = TextAnchor.MiddleCenter;
      //  GUI.Label(Rect(0, 0, 100, 50), string.Format("{0:N0}%", async.progress * 100f));

        if (!isIGM && !isCompleted)
        {
            GUI.skin = ButtonMainmenu;
            if (GUI.Button(new Rect(120 * DEF.scaleX, 0 * DEF.scaleY, 140 * DEF.scaleX, 100 * DEF.scaleY), " "))
            {//main menu
                DEF.playSounBack(this);
                Application.LoadLevel("SceneMainMenu");
            }
            GUI.skin = ButtonUndo;          
            if (GUI.Button(new Rect(Screen.width / 2 - 70 * DEF.scaleX, 0 * DEF.scaleY, 140 * DEF.scaleX, 100 * DEF.scaleY), " "))
            {
                DEF.playSounBack(this);
                gameEngine.undo();
                //isIGM = true;
            }
            GUI.skin = ButtonReplay;
           if (GUI.Button(new Rect(600 * DEF.scaleX, 0 * DEF.scaleY, 140 * DEF.scaleX, 100 * DEF.scaleY), " "))
            {//replay
                DEF.playSounBack(this);
                PlayGame();
            }
            GUI.skin = guiSkinSelectLevelBack;
        if (GUI.Button(new Rect(50 * DEF.scaleX, 90 * DEF.scaleY,  130 * DEF.scaleX,  130 * DEF.scaleY), " "))
        {
          //  DEF.playSounBack(this);
            GameEngine.mcurrentlevel--;
            if (GameEngine.mcurrentlevel < 0)
                GameEngine.mcurrentlevel = 99;
            PlayGame();
        }


        GUI.skin = guiSkinSelectLevelNext;
        if (GUI.Button(new Rect(600 * DEF.scaleX, 90 * DEF.scaleY, 130 * DEF.scaleX, 130 * DEF.scaleY), ""))
        {
          //  DEF.playSounBack(this);
            GameEngine.mcurrentlevel++;
            if (GameEngine.mcurrentlevel > 99)
                GameEngine.mcurrentlevel = 0;
            PlayGame();
          
        }
           drawHint();
           

          
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
    public void drawHint()
    {
        if (isHint)
        {
            GUI.DrawTexture(new Rect(mHintx,mHinty, 100 * DEF.scaleY, 110 * DEF.scaleY), Finger);
            mHintx += 3 * DEF.scaleX;
            if (mHintx > mHintEndx)
            {
                mHintx = mHintBeginx;
                mHintCount++;
                if (mHintCount > 2)
                    isHint = false;
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

