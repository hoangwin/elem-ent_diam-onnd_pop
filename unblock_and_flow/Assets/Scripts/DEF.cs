using UnityEngine;
using System.Collections;

public class DEF_ : MonoBehaviour
{

	// Use this for initialization
	public static float scaleX =1f;
	public static float scaleY =1f;

    public static float m_VerSize = 1;
    public static float m_horSize =1;


   // public static bool isNeedSoundQuestion = true;
    
	public static SuperInt ScoreModeClassic;
	public static SuperInt ScoreModeExtra;

	public static void initAndLoadScore()
	{
		if(DEF_.ScoreModeClassic == null)
		{
			DEF_.ScoreModeClassic = new SuperInt(0,"SCORE_CLASSIC");
			DEF_.ScoreModeClassic.Load();
			DEF_.ScoreModeExtra = new SuperInt(0,"SCORE_MODE_EXTRA");
			DEF_.ScoreModeExtra.Load();
            //test
          //  DEF_.ScoreModeClassic.NUM = 0;
          //  DEF_.ScoreModeClassic.Save();
         //   DEF_.ScoreModeExtra.NUM = 0;
          //  DEF_.ScoreModeExtra.Save();
		}
	}
    public static void FillFullImage(SpriteRenderer sr)
    {
        float worldScreenHeight = Camera.main.orthographicSize * 2;
        float worldScreenWidth = worldScreenHeight * Screen.width/Screen.height;
        sr.gameObject.transform.localScale = new Vector3(worldScreenWidth / sr.sprite.bounds.size.x,0.02f +worldScreenHeight / sr.sprite.bounds.size.y, 1);
//        Debug.Log(worldScreenHeight + "," + Screen.width + "," + Screen.height +"," +(worldScreenWidth));
 //       Debug.Log((sr.sprite.bounds.size.y));
    }

}

