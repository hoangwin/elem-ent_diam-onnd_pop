using UnityEngine;
using System.Collections;

public class ScriptHighScore : MonoBehaviour
{

    public GUISkin guiSkinButtonNormal;    
    public GUISkin guiSkinSelectLevelBack;
    public GUISkin guiSkinLabel;
    public Texture Dialog;
    public static int MAX_COL = 4;
    public static int MAX_ROW = 5;
    string[] strarray;

    public static string[] rankNameLabel = new string[10];
    public static string[] scoreLabel= new string[10];

    public static string myName = "User";
    public static string myScore = "0";
    public static string myPos = "----";



	// Use this for initialization
    public static string strUserName = "InputName";
	void Start () {
        DEF.init();

        guiSkinButtonNormal.button.fontSize = (int)(60 * DEF.scaleY);        
        
        guiSkinLabel.label.fontSize = (int)(60 * DEF.scaleY);
        //Update: http://gamethuanviet.com/candypophd/SetGetData.php?type=update&username=%s&Score=%d&Level=0&Played=0&country=NA 
        // http://gamethuanviet.com/candypophd/SetGetData.php?type=select&username=%s
        Debug.Log(GameEngine.username);
        if (GameEngine.username.Length >= 6)
        {
            PostHightScore();
            getHightScore();
        }
      //  GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(DEF.scaleX, DEF.scaleY, 1));
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.LoadLevel("SceneMainMenu");
        }
	}
    void OnGUI()
    {

        GUIStyle myStyle;
        myStyle =  guiSkinLabel.GetStyle("Label");
        myStyle.alignment = TextAnchor.MiddleCenter;
        myStyle.fontSize = (int)(60 * DEF.scaleY);
        myStyle.normal.textColor = Color.yellow;
        GUI.skin = guiSkinLabel; GUI.skin = guiSkinSelectLevelBack;
        GUI.Label(new Rect(Screen.width / 2 - 800 / 2 * DEF.scaleX, 40 * DEF.scaleY, 800 * DEF.scaleX, 60 * DEF.scaleY), "HIGH SOCRE", myStyle);
      //  GUI.DrawTexture(new Rect(0 * DEF.scaleX, 300 * DEF.scaleY, Screen.width, 600 * DEF.scaleY), Dialog);

        myStyle.normal.textColor = Color.white;
        if (GameEngine.username.Length < 6)
        {
            Debug.Log(GameEngine.username);
            string str = "Must more than 6 charracter";
            GUI.Label(new Rect(70 * DEF.scaleX, 100 * DEF.scaleY, 640 * DEF.scaleX, 400 * DEF.scaleY), str, myStyle);
            strUserName = GUI.TextField(new Rect(100 * DEF.scaleX, 400 * DEF.scaleY, 600 * DEF.scaleX, 80 * DEF.scaleY), strUserName, 12);
            strUserName = strUserName.Replace(' ','_');
            GUI.skin = guiSkinButtonNormal;
            if (strUserName.Length > 6)
            {


                if (GUI.Button(new Rect(Screen.width / 2 - 240 * DEF.scaleX, 500 * DEF.scaleY, 480 * DEF.scaleX, 120 * DEF.scaleY), "OK"))
                {
                    // Application.LoadLevel("SceneGamePlay");             
                    GameEngine.username = strUserName;
                    ScriptMainMenu.saveGame();
                    PostHightScore();
                    getHightScore();
                    DEF.playSounBack(this);
                }
            }
        }
        else
        {
            myStyle.alignment = TextAnchor.MiddleLeft;
            myStyle.fontSize = (int)(40 * DEF.scaleY);
            GUI.Label(new Rect(20 * DEF.scaleX, (100 + 50 * (-2)) * DEF.scaleY, 640 * DEF.scaleX, 400 * DEF.scaleY),"Pos", myStyle);
            GUI.Label(new Rect(150 * DEF.scaleX, (100 + 50 * (-2)) * DEF.scaleY, 640 * DEF.scaleX, 400 * DEF.scaleY),"Name", myStyle);
            GUI.Label(new Rect(600 * DEF.scaleX, (100 + 50 * (-2)) * DEF.scaleY, 640 * DEF.scaleX, 400 * DEF.scaleY), "Score", myStyle);
            GUI.Label(new Rect(100 * DEF.scaleX, (100 + 50 * (-1))* DEF.scaleY, 640 * DEF.scaleX, 400 * DEF.scaleY), "-----------------------------------", myStyle);
            for (int i = 0; i < 10; i++)
            {
                GUI.Label(new Rect(20 * DEF.scaleX, (100 + 50 * i) * DEF.scaleY, 640 * DEF.scaleX, 400 * DEF.scaleY), (i + 1) + ".", myStyle);
                GUI.Label(new Rect(150 * DEF.scaleX, (100 + 50 * i) * DEF.scaleY, 640 * DEF.scaleX, 400 * DEF.scaleY), rankNameLabel[i], myStyle);
                GUI.Label(new Rect(600 * DEF.scaleX, (100 + 50 * i) * DEF.scaleY, 640 * DEF.scaleX, 400 * DEF.scaleY), scoreLabel[i], myStyle);
            }
            GUI.Label(new Rect(100 * DEF.scaleX, (100 + 500) * DEF.scaleY, 640 * DEF.scaleX, 400 * DEF.scaleY), "-----------------------------------", myStyle);

            GUI.Label(new Rect(20 * DEF.scaleX, (100 + 50 * 11) * DEF.scaleY, 640 * DEF.scaleX, 400 * DEF.scaleY), myPos, myStyle);
            GUI.Label(new Rect(150 * DEF.scaleX, (100 + 50 * 11) * DEF.scaleY, 640 * DEF.scaleX, 400 * DEF.scaleY), myName, myStyle);
            GUI.Label(new Rect(600 * DEF.scaleX, (100 + 50 * 11) * DEF.scaleY, 640 * DEF.scaleX, 400 * DEF.scaleY), myScore, myStyle);
        }
        
        GUI.skin = guiSkinSelectLevelBack;

        if (GUI.Button(new Rect(Screen.width - 140 * DEF.scaleX, 0, 140 * DEF.scaleX, 140 * DEF.scaleY), " "))
        {
            DEF.playSounBack(this);
            Application.LoadLevel("SceneMainmenu");
        }

        GUI.skin = guiSkinButtonNormal;
       

    }
    public void  getHightScore()
    {
        WWW www = new WWW("http://gamethuanviet.com/candypophd/SetGetData.php?type=select&username="+ GameEngine.username);
        Debug.Log("http://gamethuanviet.com/candypophd/SetGetData.php?type=select&username=" + GameEngine.username);
        while (!www.isDone)
        {


        }
        Debug.Log(www.text);
        strarray = www.text.Split('|');
        for (int i = 0; i < 10; i++)
        {

            rankNameLabel[i] = strarray[i * 3 + 1];
            scoreLabel[i] = strarray[i * 3 + 2];
        }
        if (strarray.Length > 33)
        {
            myPos = strarray[10 * 3 + 1];
            myName = strarray[10 * 3 + 2];
            myScore = strarray[10 * 3 + 3];
        }
    }
    public static void PostHightScore()
    {
     
      //  string strPost = "http://gamethuanviet.com/candypophd/SetGetData.php?type=update&username=" + GameEngine.username + "&Score=" + GameEngine.mTopScore +"&Level=0&Played=0&country=NA";
      //  Debug.Log(strPost);
      //  WWW www = new WWW(strPost);
    }
}
