using UnityEngine;
using System.Collections;

public class ScriptSelectLevel : MonoBehaviour {

    public GUISkin guiSkinSelectLevelNormal;
    public GUISkin guiSkinSelectLevelDIsable;
    public GUISkin guiSkinSelectLevelBack;
    public GUISkin guiSkinLabel;
    public GUISkin guiSkinNext;
    public GUISkin guiSkinPre;

    
    public static int MAX_COL = 4;
    public static int MAX_ROW = 5;
    public static int MAX_PAGE = 5;
    public static int mcurrentPage = 0;
    int START_X;
    int START_Y;
    int SPACING;
    int WIDTH;
    int HEIGHT;
	// Use this for initialization
	void Start () {
        DEF.init();
        
        guiSkinSelectLevelNormal.button.fontSize = (int)(60 * DEF.scaleY);
        guiSkinSelectLevelDIsable.button.fontSize = (int)(60 * DEF.scaleY);
        guiSkinSelectLevelBack.button.fontSize = (int)(60 * DEF.scaleY);
        guiSkinLabel.label.fontSize = (int)(60 * DEF.scaleY);

        START_X = (int)(80 * DEF.scaleX);
        START_Y = (int)( 155 * DEF.scaleX) ;
        SPACING = (int)(30 * DEF.scaleX);
        WIDTH = (int)(150 * DEF.scaleX);
        HEIGHT = (int)(150 * DEF.scaleY);

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
        GUI.skin = guiSkinLabel; 
        
        GUI.Label(new Rect(Screen.width / 2 - 800 / 2 * DEF.scaleX, 40 * DEF.scaleY, 800 * DEF.scaleX, 60 * DEF.scaleY), "SELECT LEVEL", myStyle);

        myStyle.normal.textColor = Color.white;
        GUI.Label(new Rect(0, 950 * DEF.scaleY,Screen.width, 60 * DEF.scaleY),(mcurrentPage+ 1) +" /" + MAX_PAGE, myStyle);
        GUI.skin = guiSkinPre;
        if (GUI.Button(new Rect(150 * DEF.scaleX, 920 * DEF.scaleY, WIDTH, HEIGHT), " "))
        {
            DEF.playSounBack(this);
            mcurrentPage--;
            if (mcurrentPage < 0)
                mcurrentPage = MAX_PAGE - 1;
        }

        GUI.skin = guiSkinNext;
        if (GUI.Button(new Rect(500 * DEF.scaleX, 920 * DEF.scaleY, WIDTH, HEIGHT), " "))
        {
            DEF.playSounBack(this);
            mcurrentPage++;
            if (mcurrentPage >= MAX_PAGE)
                mcurrentPage = 0;
        }


        GUI.skin = guiSkinSelectLevelBack;
        if (GUI.Button(new Rect(Screen.width - 140 * DEF.scaleX, 0, 140 * DEF.scaleX, 140 * DEF.scaleY), ""))
        {
            DEF.playSounBack(this);
            Application.LoadLevel("SceneMainMenu");
        }

       
        for(int i = 0;i<MAX_ROW;i++)
        {
            for(int j = 0;j<MAX_COL;j++)
            {

                if (mcurrentPage * MAX_COL * MAX_ROW + i * MAX_COL + j <= GameEngine.mUnlocklevel)
                {
                    GUI.skin = guiSkinSelectLevelNormal;
                    if (GUI.Button(new Rect(START_X + j * (WIDTH + SPACING), START_Y + i * (HEIGHT + SPACING/2), WIDTH, HEIGHT), "" + (mcurrentPage*MAX_COL*MAX_ROW+ i * MAX_COL + j + 1)))
                    {
                        DEF.playSounBack(this);
                        GameEngine.mcurrentlevel = mcurrentPage * MAX_COL * MAX_ROW + i * MAX_COL + j + 1;
                        Application.LoadLevel("SceneHint");

                    }
                }
                else
                {
                        GUI.skin = guiSkinSelectLevelDIsable;
                        GUI.Button(new Rect(START_X + j * (WIDTH + SPACING), START_Y + i * (HEIGHT + SPACING / 4), WIDTH, HEIGHT), "" + (mcurrentPage * MAX_COL * MAX_ROW + i * MAX_COL + j + 1));
                }
              
          
            }
        }

        
    }
}
