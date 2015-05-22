using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScriptSelectLevel : MonoBehaviour {

	public GameObject ButtonTempleted;

    

    public static int MAX_PAGE = 5;
    public static int mcurrentPage = 0;
	public Text TextPage;
    public Text textMode;
	public UnityEngine.Sprite imageOn;
	public UnityEngine.Sprite imageOFF;
	public LevelButton[] arrayButton;
	public GameObject line1;
	public GameObject line2;
	public GameObject line3;
	public GameObject line4;
	public GameObject line5;

	public Vector3 vline1;
	public Vector3 vline2;
	public Vector3 vline3;
	public Vector3 vline4;
	public Vector3 vline5;
	public static ScriptSelectLevel instance;
	// Use this for initialization
	public Image colorFadeEfect;
	public Color color;
	void Start () {
		DEF_.initAndLoadScore ();		
		color = new Color(colorFadeEfect.color.r,colorFadeEfect.color.g,colorFadeEfect.color.b,colorFadeEfect.color.a);
		colorFadeEfect.enabled = false;
		Debug.Log (colorFadeEfect.color.a);
		float worldScreenHeight = Camera.main.orthographicSize * 2;
		float worldScreenWidth = worldScreenHeight * Screen.width/Screen.height;
		
		if(GamePlay.GameMode ==0)
			mcurrentPage = DEF_.ScoreModeClassic.NUM/20;
		else
			mcurrentPage = DEF_.ScoreModeExtra.NUM/20;
		vline1 = new Vector3(line1.transform.position.x,line1.transform.position.y,line1.transform.position.z);
		vline2 = new Vector3(line2.transform.position.x,line2.transform.position.y,line2.transform.position.z);
		vline3 = new Vector3(line3.transform.position.x,line3.transform.position.y,line3.transform.position.z);
		vline4 = new Vector3(line4.transform.position.x,line4.transform.position.y,line4.transform.position.z);
		vline5 = new Vector3(line5.transform.position.x,line5.transform.position.y,line5.transform.position.z);
		Init ();
		instance = this;
	}
	public void Init()
	{	
		line1.transform.position = vline1;
		line2.transform.position = vline2;
		line3.transform.position = vline3;
		line4.transform.position = vline4;
		line5.transform.position = vline5;
		//xong doi gia tri
		// chuyen tu toa do dau sang toa do moi
		iTween.MoveFrom(line1, iTween.Hash("x", vline1.x + Screen.width,"time", 0.5, "EaseType", "linear"));
		iTween.MoveFrom(line2, iTween.Hash("x", vline2.x + Screen.width,"time", 0.5, "EaseType", "linear"));
		iTween.MoveFrom(line3, iTween.Hash("x", vline3.x + Screen.width,"time", 0.5, "EaseType", "linear"));
		iTween.MoveFrom(line4, iTween.Hash("x", vline4.x + Screen.width,"time", 0.5, "EaseType", "linear"));
		iTween.MoveFrom(line5, iTween.Hash("x", vline5.x + Screen.width,"time", 0.5, "EaseType", "linear"));

        if (GamePlay.GameMode == 0)
            textMode.text = "Classic Mode";
        else
            textMode.text = "Extra Mode";
		if (mcurrentPage < 0)
			mcurrentPage = MAX_PAGE - 1;
		if (mcurrentPage >= MAX_PAGE)
			mcurrentPage = 0;
		
		TextPage.text = (mcurrentPage + 1).ToString () + "/" + MAX_PAGE.ToString();
		int tempUnblock = 0;
		if(GamePlay.GameMode ==0)
			tempUnblock = DEF_.ScoreModeClassic.NUM;
		else
			tempUnblock = DEF_.ScoreModeExtra.NUM;
		for (int i= 0; i<20; i++) 
		{
			int temp = mcurrentPage*20 + i ;
			if(temp > tempUnblock)
			{
				arrayButton[i].index = (temp);
				arrayButton[i].image.sprite = imageOFF;
				arrayButton[i].text.text ="";

			}
			else
			{
				arrayButton[i].index = (temp);
				arrayButton[i].image.sprite = imageOn;
				arrayButton[i].text.text = (temp+ 1).ToString();
			}
		}
	}
	public void efectClick()
	{
		iTween.MoveTo(line1, iTween.Hash("x", line1.transform.position.x - Screen.width,"time", 0.5, "EaseType", "linear"));
		iTween.MoveTo(line2, iTween.Hash("x", line2.transform.position.x + Screen.width,"time", 0.5, "EaseType", "linear"));
		iTween.MoveTo(line3, iTween.Hash("x", line3.transform.position.x - Screen.width,"time", 0.5, "EaseType", "linear"));
		iTween.MoveTo(line4, iTween.Hash("x", line4.transform.position.x + Screen.width,"time", 0.5, "EaseType", "linear"));
        iTween.MoveTo(line5, iTween.Hash("x", line5.transform.position.x - Screen.width, "time", 0.5, "EaseType", "linear", "oncomplete", "selectLevelClickCompleted"));
		iTween.ValueTo(this.gameObject,iTween.Hash("from",0,"to",1,"time",0.5f,"onupdate","onUpdateValue"));
	}
	public void onUpdateValue(float a)
	{
		color.a = a;
		colorFadeEfect.color = color;

	//	Debug.Log (a);
	}
		            
	public void efectChangePage()
	{
		iTween.MoveTo(line1, iTween.Hash("x", line1.transform.position.x - Screen.width,"time", 0.5, "EaseType", "linear"));
		iTween.MoveTo(line2, iTween.Hash("x", line2.transform.position.x + Screen.width,"time", 0.5, "EaseType", "linear"));
		iTween.MoveTo(line3, iTween.Hash("x", line3.transform.position.x - Screen.width,"time", 0.5, "EaseType", "linear"));
		iTween.MoveTo(line4, iTween.Hash("x", line4.transform.position.x + Screen.width,"time", 0.5, "EaseType", "linear"));
		iTween.MoveTo(line5, iTween.Hash("x", line5.transform.position.x - Screen.width,"time", 0.5, "EaseType", "linear","oncomplete","selectLevelChangePageCompleted"));
		
	}

	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.LoadLevel("SceneMainMenu");
        }


	}
	public void ButtonLeftPage()
    {
        SoundEngine.instance.PlayOneShot(SoundEngine.instance._soundclick);
		mcurrentPage -= 1;
		efectChangePage ();
	}
	public void ButtonRightPage()
    {
        SoundEngine.instance.PlayOneShot(SoundEngine.instance._soundclick);
		mcurrentPage += 1;
		efectChangePage ();
	}
    public void ButtonBackMainmenu()
    {
        SoundEngine.instance.PlayOneShot(SoundEngine.instance._soundclick);
        Application.LoadLevel("SceneMainMenu");
    }
}
