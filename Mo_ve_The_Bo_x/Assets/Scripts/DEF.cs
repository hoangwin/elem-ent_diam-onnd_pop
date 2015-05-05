using UnityEngine;
using System.Collections;

public class DEF : MonoBehaviour
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

    

	void Start ()
	{
		DEF.init();
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
	public static void  init()
	{
		scaleX = Screen.width/800f;
		scaleY = Screen.height/1280f;

        m_VerSize = Camera.main.orthographicSize * 2.0f;        
        m_horSize = m_VerSize * Screen.width / Screen.height;

        Debug.Log(m_VerSize);
	}
    public static Vector3 Vec3(float PixelX, float PixelY, float z = 0)
    {
        return new Vector3(((PixelX * m_horSize / Screen.width) - m_horSize / 2), (m_VerSize / 2 - PixelY * m_VerSize / Screen.height), z);
    }
    public static void playSounBack(MonoBehaviour scene)
    {
        if (!DEF.isSoundEnable)
            return;
        GameObject SounBack = GameObject.Find("EffectMusic");
        if (SounBack != null)
        {
           // Debug.Log("Play Sound");
            SounBack.audio.Play();
        }
    }
    
    public static void playSounEffect(int index)
    {
        if (!DEF.isSoundEnable)
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
            SounBack.audio.Play();
        }
    }
    public static void playSound(string str)
    {
        if (!DEF.isSoundEnable)
            return;
        GameObject SounBack = GameObject.Find(str);
        if (SounBack != null)
        {
            // Debug.Log("Play Sound");
            SounBack.audio.Play();
        }
    }
}

