using UnityEngine;
using System.Collections;

public class DEF_ : MonoBehaviour
{

	// Use this for initialization
	public static float scaleX =1f;
	public static float scaleY =1f;

    public static float m_VerSize = 1;
    public static float m_horSize =1;

    public static string STRING_USER_NAME = "USER_NAME";
    public static string STRING_TOP_SCORE = "TOP_SCORE";
    public static string STRING_UNLOCK_LEVEL = "UNLOCK_LEVEL";

   // public static bool isNeedSoundQuestion = true;
    public static bool isSoundEnable = true;
    	

	
    public static void FillFullImage(SpriteRenderer sr)
    {
        float worldScreenHeight = Camera.main.orthographicSize * 2;
        float worldScreenWidth = worldScreenHeight * Screen.width/Screen.height;
        sr.gameObject.transform.localScale = new Vector3(worldScreenWidth / sr.sprite.bounds.size.x,0.02f +worldScreenHeight / sr.sprite.bounds.size.y, 1);
        Debug.Log(worldScreenHeight + "," + Screen.width + "," + Screen.height +"," +(worldScreenWidth));
        Debug.Log((sr.sprite.bounds.size.y));
    }

    public static void playSounBack(MonoBehaviour scene)
    {
        if (!isSoundEnable)
            return;
        GameObject SounBack = GameObject.Find("EffectMusic");
        if (SounBack != null)
        {
           // Debug.Log("Play Sound");
            SounBack.GetComponent<AudioSource>().Play();
        }
    }
    
    public static void playSounEffect(int index)
    {
        if (!isSoundEnable)
            return;
        string str = "";
        switch(index)
        {
            case 0:
                str = "Sound_Move";
                break;            
            case 1:
                str = "Sound_Win";
                break;
        }
         
        GameObject SounBack = GameObject.Find(str);
        if (SounBack != null)
        {
            // Debug.Log("Play Sound");
            SounBack.GetComponent<AudioSource>().Play();
        }
    }
    public static void playSound(string str)
    {
        if (!isSoundEnable)
            return;
        GameObject SounBack = GameObject.Find(str);
        if (SounBack != null)
        {
            // Debug.Log("Play Sound");
            SounBack.GetComponent<AudioSource>().Play();
        }
    }
}

