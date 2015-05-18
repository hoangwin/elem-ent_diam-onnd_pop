using UnityEngine;
using System.Collections;

public class ScriptMainMenu : MonoBehaviour
{
    static public float timeShowAds = 0;
    static public bool firstShowAds = false;
    public GameObject line1;
    public GameObject line2;
    public GameObject line3;
    public GameObject line4;
    public static ScriptMainMenu instance;
    void Start()
    {
		DEF_.initAndLoadScore ();
        effectBegin();
        instance = this;
        ShowADS();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    

    public static void ShowADS()
    {
        if (timeShowAds > 90 || !firstShowAds)
        {
            firstShowAds = true;
            timeShowAds = 0;
#if UNITY_ANDROID
            using (AndroidJavaClass jc = new AndroidJavaClass("com.bubbleshoot2.free.UnityPlayerNativeActivity"))
            {
                jc.CallStatic<int>("ShowAds");
            }
            
#elif UNITY_WP8
            WP8Statics.ShowAds("");
#elif UNITY_IOS
            IOsStatic.ShowAds(" ", " ");
#endif
        }
    }

    public void effectBegin()
    {
        Debug.Log("move");
        iTween.MoveFrom(line1, iTween.Hash("x", - Screen.width/2, "time", 0.5, "EaseType", "linear"));
        iTween.MoveFrom(line2, iTween.Hash("x", - Screen.width/2, "time", 0.6, "EaseType", "linear"));
        iTween.MoveFrom(line3, iTween.Hash("x", - Screen.width/2, "time", 0.7, "EaseType", "linear"));
        iTween.MoveFrom(line4, iTween.Hash("x", - Screen.width/2, "time", 0.8, "EaseType", "linear"));
        //iTween.MoveTo(line5, iTween.Hash("x", line5.transform.position.x - Screen.width, "time", 0.5, "EaseType", "linear", "oncomplete", "selectLevelChangePageCompleted"));

    }

    public void effectEnd()
    {
        Debug.Log("move");
        iTween.MoveTo(line1, iTween.Hash("x", -Screen.width / 2, "time", 0.5, "EaseType", "linear"));
        iTween.MoveTo(line2, iTween.Hash("x", -Screen.width / 2, "time", 0.5, "EaseType", "linear"));
        iTween.MoveTo(line3, iTween.Hash("x", -Screen.width / 2, "time", 0.5, "EaseType", "linear"));
        iTween.MoveTo(line4, iTween.Hash("x", -Screen.width / 2, "time", 0.5, "EaseType", "linear", "oncomplete", "MainMenuMoveEndCompleted"));
        //iTween.MoveTo(line5, iTween.Hash("x", line5.transform.position.x - Screen.width, "time", 0.5, "EaseType", "linear", "oncomplete", "selectLevelChangePageCompleted"));

    }

}
