using UnityEngine;
using System.Collections;

public class ScriptHint : MonoBehaviour {

    public GUISkin guiSkinButtonNormal;    
    public GUISkin guiSkinSelectLevelBack;
    public GUISkin guiSkinLabel;
    public Texture Dialog;
   
	public static bool isHint = true;
	public static float mHintx = 0;
	public static float mHinty = 0;
	public static float mHintBeginx = 0;
	public static float mHintBeginy = 0;
	public static float mHintEndx = 0;
	public static float mHintEndy = 0;
	public static int mHintCount = 0;
    
    public static int MAX_COL = 4;
    public static int MAX_ROW = 5;
    
	// Use this for initialization
	void Start () {
        DEF.init();

        guiSkinButtonNormal.button.fontSize = (int)(60 * DEF.scaleY);        
        
        guiSkinLabel.label.fontSize = (int)(60 * DEF.scaleY);

    

      //  GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(DEF.scaleX, DEF.scaleY, 1));
	}
	public void initHint()
	{
		if (GamePlay.mcurrentlevel == 0)
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
	public void drawHint()
	{
		if (isHint)
		{
			//     GUI.DrawTexture(new Rect(mHintx, mHinty, 100 * DEF.scaleY, 110 * DEF.scaleY), Finger);
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
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Application.LoadLevel("SceneMainMenu");
        }
	}
   
}
