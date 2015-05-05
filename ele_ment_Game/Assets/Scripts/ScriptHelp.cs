using UnityEngine;
using System.Collections;

public class ScriptHelp : MonoBehaviour {

    public GUISkin guiSkinButtonNormal;    
    public GUISkin guiSkinSelectLevelBack;
    public GUISkin guiSkinLabel;
    public Texture Dialog;
   

    
    public static int MAX_COL = 4;
    public static int MAX_ROW = 5;
    
	// Use this for initialization
	void Start () {
        DEF.init();

        guiSkinButtonNormal.button.fontSize = (int)(60 * DEF.scaleY);        
        
        guiSkinLabel.label.fontSize = (int)(60 * DEF.scaleY);

    

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
        myStyle.normal.textColor = Color.green;
        GUI.skin = guiSkinLabel; GUI.skin = guiSkinSelectLevelBack;
    //    GUI.Label(new Rect(Screen.width / 2 - 800 / 2 * DEF.scaleX, 40 * DEF.scaleY, 800 * DEF.scaleX, 60 * DEF.scaleY), "HINT", myStyle);
      
        
      //  myStyle.normal.textColor = Color.blue;
       

        GUI.skin = guiSkinSelectLevelBack;

        if (GUI.Button(new Rect(Screen.width - 140 * DEF.scaleX, 0, 140 * DEF.scaleX, 140 * DEF.scaleY), " "))
        {
            DEF.playSounBack(this);
           // if(GameEngine.gameMode == 0)
           //     Application.LoadLevel("SceneSelectLevel");
           // else
                Application.LoadLevel("SceneMainmenu");
        }
        /*
        GUI.skin = guiSkinButtonNormal;
        if (GUI.Button(new Rect(Screen.width / 2 - 240 * DEF.scaleX, 900 * DEF.scaleY, 480 * DEF.scaleX, 120 * DEF.scaleY), "Play"))
        {
            DEF.playSounBack(this);
            Application.LoadLevel("SceneGamePlay");
        }
        */
    }
}
