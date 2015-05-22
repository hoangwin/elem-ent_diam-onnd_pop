using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GamePlay : MonoBehaviour
{
    // Use this for initialization
    public GameObject frefabItem;//item binh thuong go, nen hoac item ko di chuyen
	//public GameObject frefabItemMove;//item di chuyen khac nhau la the m rigibody2d
    

    public static int MAX_COL = 7;
    public static int MAX_ROW = 8;
    
    public static float ITEM_WIDTH = 60;
    public static float ITEM_HEIGHT = 60;
    //SpriteRenderer a;
    public UnityEngine.SpriteRenderer BGroundSpriteRenderer;
    public UnityEngine.Sprite WallSprite;
    public UnityEngine.Sprite FlagSprite;
    public UnityEngine.Sprite[] LineSprite;
    public UnityEngine.Sprite[] ItemSpriteEnable;
    public UnityEngine.Sprite[] ItemSpriteDisable;

    public ActorItem[][] mapArray;//luu nhung gia tri co the di chuyen + ban do
	public ActorItem[][] mapArrayDisable;//mang luu nhung gia tri dung yen
    public Transform tranformObjSelect;
    public Transform tranformPos1;
    public Transform tranformPos2;

    public static bool isCompleted;
	public static float timer;

    public static float BEGIN_X = 0;
    public static float BEGIN_Y = 0;

    static bool isFistInit = false;
    public static GamePlay instance;
    public static int mcurrentlevel= 0;
    public static int munblocklevel = 0;
    public static int COL_SELECT = -1;
    public static int ROW_SELECT = -1;

    public static Vector3 beginInput;//input touch
    public static Vector3 endInput;//touch input
    public static bool isSelectItem = false;

    public static ArrayList arrayList = new ArrayList();
    public static ArrayList arrayListUndo = new ArrayList();

    public static ActorItem currentActor;
	public static int GameMode = 0;//0 -> ko co duong di//1 -> co duong chay theo
    public GameObject ImageBlock;
	public Image colorFadeEfect;
    public Text textLevelIngame;
    public Text textLevelCompleted;
	public Color color;

    void Start()
    {
        color = new Color(colorFadeEfect.color.r, colorFadeEfect.color.g, colorFadeEfect.color.b, colorFadeEfect.color.a);

        isCompleted = false;
        instance = this;
        timer = -1;
        //    DEF.init();
        //GameEngine.mcurrentlevel = 3;

        DEF_.FillFullImage(BGroundSpriteRenderer);
        destroyAll();
        InitCommondBegin();
        mapArray = new ActorItem[MAX_ROW][];
        mapArrayDisable = new ActorItem[MAX_ROW][];
        for (int i = 0; i < MAX_ROW; i++)
        {
            mapArray[i] = new ActorItem[MAX_COL];
            mapArrayDisable[i] = new ActorItem[MAX_COL];
        }
        if (Undo.instance != null)
            Undo.instance.clearAllUndo();
        if (GamePlay.mcurrentlevel < 0)
            GamePlay.mcurrentlevel = 100;
        else if (GamePlay.mcurrentlevel >= 100)
            GamePlay.mcurrentlevel = 0;
        for (int i = 0; i < MAX_ROW; i++)
        {
            for (int j = 0; j < MAX_COL; j++)
            {

                int value = 0;
                if (GameMode == 0)
                    value = ScriptDataLevel.levels[GamePlay.mcurrentlevel][i][j];
                else
                    value = ScriptDataLevelExtra.levels[GamePlay.mcurrentlevel][i][j];

                GameObject obj;
                ActorItem actor;


                //	if ( value < 6)
                //{
                obj = (GameObject)(Instantiate(frefabItem));
                actor = obj.GetComponent<ActorItem>();
                float x = BEGIN_X + j * ITEM_WIDTH + ITEM_WIDTH / 2;
                float y = BEGIN_Y + i * ITEM_HEIGHT + ITEM_HEIGHT / 2;
                if (value < 11)
                    actor.setActorItem(value, i, j, x, y, ActorItem.STATE_IDE);
                else
                    actor.setActorItem(-1, i, j, x, y, ActorItem.STATE_IDE);
                actor.gameObject.SetActive(true);
                mapArray[i][j] = actor;
                //create background
                obj = (GameObject)(Instantiate(frefabItem));
                actor = obj.GetComponent<ActorItem>();
                x = BEGIN_X + j * ITEM_WIDTH + ITEM_WIDTH / 2;
                y = BEGIN_Y + i * ITEM_HEIGHT + ITEM_HEIGHT / 2;
                if (value < 11)
                    actor.setActorItem(-1, i, j, x, y, ActorItem.STATE_IDE);
                else
                    actor.setActorItem(value, i, j, x, y, ActorItem.STATE_IDE);
                actor.gameObject.SetActive(true);
                //actor.spriteRenderer.sortingOrder = 1;
                //  arrayList.Add(actor);
                mapArrayDisable[i][j] = actor;

                //}
                {

                    //flag
                    if (value > 10)
                    {
                        mapArrayDisable[i][j].gameObjectFlag = (GameObject)(Instantiate(frefabItem));
                        actor = mapArrayDisable[i][j].gameObjectFlag.GetComponent<ActorItem>();
                        actor.setActorItem(100, i, j, x, y, ActorItem.STATE_IDE);//flag
                        actor.gameObject.SetActive(false);
                        actor.spriteRenderer.sortingOrder = 3;
                        //  arrayList.Add(actor);
                    }

                }


            }
        }

        iTween.ValueTo(this.gameObject, iTween.Hash("from", 1, "to", 0, "time", 1f, "onupdate", "onUpdateValue"));
        textLevelIngame.text = "Level " + (mcurrentlevel + 1).ToString();
        ScriptHint.instance.init();


        if (GamePlay.GameMode == 0)
            munblocklevel = DEF_.ScoreModeClassic.NUM;
        else
            munblocklevel = DEF_.ScoreModeExtra.NUM;
        if (munblocklevel < mcurrentlevel)
            ImageBlock.SetActive(true);
        else
            ImageBlock.SetActive(false);
    }

public void onUpdateValue(float a)
{
	color.a = a;
	colorFadeEfect.color = color;
	if(a == 0)
	colorFadeEfect.enabled = false;
//	Debug.Log (a);
}
    void destroyAll()
    {
        if (mapArray != null)
            for (int i = 0; i < MAX_ROW; i++)
            {
                for (int j = 0; j < MAX_COL; j++)
                {
                    if (mapArray[i][j] != null)
                        ((ActorItem)(mapArray[i][j])).destroy();
                    if (mapArrayDisable[i][j] != null)
                        ((ActorItem)(mapArrayDisable[i][j])).destroy();
                }
            }
    }
	public void InitCommondBegin()
	{
		if (GameMode == 0) {
						MAX_ROW = ScriptDataLevel.levels [GamePlay.mcurrentlevel].Length;
						MAX_COL = ScriptDataLevel.levels [GamePlay.mcurrentlevel] [0].Length;
				} else {
						MAX_ROW = ScriptDataLevelExtra.levels [GamePlay.mcurrentlevel].Length;
						MAX_COL = ScriptDataLevelExtra.levels [GamePlay.mcurrentlevel] [0].Length;
				}

        //Camera cam = Camera.main;
        //float height = 2f * cam.orthographicSize;
       // float width = height * cam.aspect;

        BEGIN_X = tranformPos1.position.x;
        BEGIN_Y = tranformPos2.position.y;

        float height = tranformPos1.position.y - tranformPos2.position.y;
        float width = tranformPos2.position.x - tranformPos1.position.x;
        ITEM_WIDTH = width / (MAX_COL-2);
        ITEM_HEIGHT = height / (MAX_ROW - 2);
        BEGIN_X = BEGIN_X - ITEM_WIDTH;
        BEGIN_Y = BEGIN_Y - ITEM_HEIGHT;
        //ITEM_WIDTH = (width-0.2f)/MAX_COL;
        //ITEM_HEIGHT = (height - 4) / MAX_ROW;
        //if (ITEM_WIDTH > ITEM_HEIGHT)
        //    ITEM_WIDTH = ITEM_HEIGHT;
    //    Debug.Log(MAX_COL);
        frefabItem.SetActive(true);
        float  scalex = ITEM_WIDTH/ ((Collider2D)(frefabItem.GetComponent<Collider2D>())).bounds.size.x;
        float  scaley = ITEM_HEIGHT/ ((Collider2D)(frefabItem.GetComponent<Collider2D>())).bounds.size.y;
        //Debug.Log(scale);
        frefabItem.transform.localScale = new Vector3(frefabItem.transform.localScale.x*scalex, frefabItem.transform.localScale.y* scaley, 1);
    
            isFistInit = true;
         //   ITEM_WIDTH = ((Collider2D)(frefabItem.GetComponent<Collider2D>())).bounds.size.x;// +0.001f;
       //     ITEM_HEIGHT = ((Collider2D)(frefabItem.GetComponent<Collider2D>())).bounds.size.y;// +0.001f;
            frefabItem.SetActive(false);
        
           // float x = MAX_COL *ITEM_WIDTH ;
           // float y = MAX_ROW * ITEM_HEIGHT - ITEM_WIDTH;
          //  BEGIN_X = -x / 2;
          //  BEGIN_Y = -y / 2 - 1.5f;//3 la offset
           
         
             
          
       
    }

    public void PlayGame()
    {
        Start();

       
    }
    public static int getTargetScore()
    {
        return (GamePlay.mcurrentlevel + 1) * 1000 + 500;
    }
    // Update is called once per frame
    void Update()
    {
        ScriptMainMenu.timeShowAds += Time.deltaTime;
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Application.LoadLevel("SceneMainMenu");
		}
        if (isCompleted)
        {
        
			if(timer!=-1)
			{
				timer+=Time.deltaTime;
				if(timer >2)
				{
					UIEffect.instance.movehideCompleted();
				}
			}
			return;
        }
        if (munblocklevel < mcurrentlevel)
            return;
        if (isSelectItem)
        {
            moveRowColSelect();
        }
        else
        {
            getRowColSelect();
        }
		if (currentActor != null && currentActor.state == ActorItem.STATE_MOVE) 
		{
			if (GameMode == 1) 
			{
				ROW_SELECT = (int)((currentActor.transform.position.y - BEGIN_Y) / ITEM_HEIGHT);
				COL_SELECT = (int)((currentActor.transform.position.x - BEGIN_X) / ITEM_WIDTH);



                if (currentActor.currentRow != ROW_SELECT || currentActor.currentCol != COL_SELECT)
                {
                   // Debug.Log(ROW_SELECT + "," + COL_SELECT);
                    mapArray[ROW_SELECT][COL_SELECT].setActorItem(currentActor.value + 20);
                }
			}
		}
		// gameEngine.Update();
    }
    public void getRowColSelect()
    {
        ROW_SELECT = -1;
        COL_SELECT = -1;
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            
            beginInput = Camera.main.ScreenToWorldPoint(Input.touches[0].position );

            ROW_SELECT = (int)((beginInput.y - BEGIN_Y) / ITEM_HEIGHT);
            COL_SELECT = (int)((beginInput.x - BEGIN_X) / ITEM_WIDTH);
            Debug.Log(ROW_SELECT + " :" + COL_SELECT);

        }
        else if (Input.GetMouseButtonDown(0))
        {
            
            beginInput = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            ROW_SELECT = (int)((beginInput.y - BEGIN_Y ) / ITEM_HEIGHT);
            COL_SELECT = (int)((beginInput.x - BEGIN_X ) / ITEM_WIDTH);
        }
        if (ROW_SELECT >= 0 && ROW_SELECT < MAX_ROW && COL_SELECT >= 0 && COL_SELECT < MAX_COL)
        {
            currentActor = mapArray[ROW_SELECT][COL_SELECT];
            if (GameMode == 0)
            {
                if (currentActor.value <= 10 && currentActor.value > 0)
                {
                    isSelectItem = true;
                    //Debug.Log("Ten Ten Ten" + ROW_SELECT + " ," + COL_SELECT + "," + currentActor.value);
                    //        break;
                }
            }
            else//mode block se ko di chuye khi da khop
            {
                if (mapArrayDisable[ROW_SELECT][COL_SELECT] == null || (mapArrayDisable[ROW_SELECT][COL_SELECT] != null && mapArrayDisable[ROW_SELECT][COL_SELECT].value != currentActor.value + 10))// neu da khop thi khong di chuyen
                {
                    if (currentActor.value <= 10 && currentActor.value > 0)
                    {
                        isSelectItem = true;                        
                    }
                }
            }
        }
    }

    public void moveRowColSelect()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {

            isSelectItem = false;
            //Debug.Log("Up");
             endInput = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
        }
        else if (Input.GetMouseButtonUp(0))
        {
          //  Debug.Log("Up");
            isSelectItem = false;
             endInput = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        else
        {
            return;
        }

        float detalx = endInput.x - beginInput.x;
        float detaly = endInput.y - beginInput.y;
        if (detalx > 0.2f || detaly > 0.2f || detalx < -0.2f || detaly < -0.2f)
        {
           // Debug.Log(detalx + ",,,," + detaly);
            if (Mathf.Abs(detalx) > Mathf.Abs(detaly))
            {
                currentActor.move_x = detalx / Mathf.Abs(detalx);
                currentActor.move_y = 0;
            //    Debug.Log("11111 " + currentActor.move_x);
            }
            else
            {
                currentActor.move_x = 0;
                currentActor.move_y = detaly / Mathf.Abs(detaly);
            //    Debug.Log("2222 :" + currentActor.move_y);
            }
            currentActor.state = ActorItem.STATE_MOVE;
            if (checkNewTarget((int)currentActor.move_x, (int)currentActor.move_y))
            {
               // DEF_.playSounEffect(0);
                //stateInGamePlay = STATE_MOVE;
                currentActor.state = ActorItem.STATE_MOVE;

                Undo.instance.AddUndo(currentActor.currentCol, currentActor.currentRow, currentActor.targetCol, currentActor.targetRow);
                //Debug.Log("Add Undo:" +currentActor.currentCol +"," +currentActor.currentRow+","+ currentActor.targetCol+","+ currentActor.targetRow);
				//chuyen doi  o gia tri tai day
                if (GamePlay.instance.mapArrayDisable[currentActor.currentRow][currentActor.currentCol] != null && GamePlay.instance.mapArray[currentActor.currentRow][currentActor.currentCol].value == GamePlay.instance.mapArrayDisable[currentActor.currentRow][currentActor.currentCol].value - 10)
                {
                    GamePlay.instance.mapArrayDisable[currentActor.currentRow][currentActor.currentCol].gameObjectFlag.SetActive(false);
                }
				int tempvalue1  = 	mapArray[currentActor.targetRow][currentActor.targetCol].value;
				int tempvalue2  = 	mapArray[currentActor.currentRow][currentActor.currentCol].value;




				mapArray[currentActor.targetRow][currentActor.targetCol].setActorItem(tempvalue2);//, currentActor.currentRow, currentActor.currentCol, float _currentX, float _currentY, int _state//)
				mapArray[currentActor.currentRow][currentActor.currentCol].setActorItem(tempvalue1);

				// lay toa do luc dau
				float x = BEGIN_X + currentActor.currentCol * ITEM_WIDTH + ITEM_WIDTH / 2;;
				float y = BEGIN_Y + currentActor.currentRow * ITEM_HEIGHT + ITEM_HEIGHT / 2;;

				currentActor = mapArray[currentActor.targetRow][currentActor.targetCol];
				currentActor.state =ActorItem.STATE_MOVE;
                
				//xong doi gia tri
				// chuyen tu toa do dau sang toa do moi
				iTween.MoveFrom(currentActor.gameObject, iTween.Hash("x", x,"y", y, "speed", 10, "EaseType", "linear", "oncomplete", "Movecompleted"));
                SoundEngine.instance.PlayOneShot(SoundEngine.instance._soundMove);
			}
           // Debug.Log("MOVE HERE");
        }
    }

    public bool checkNewTarget(int movex, int movey)
    {
		currentActor.targetRow = currentActor.currentRow;
		currentActor.targetCol = currentActor.currentCol;
        //Debug.Log(targetRow + ",," + targetCol);
        //Debug.Log(movey + ",,," + movex);
        bool valuereturn = false;
        if (GameMode == 0)
        {
            while (mapArray[currentActor.targetRow + movey][currentActor.targetCol + movex].value == -1)
            {
                currentActor.targetCol += movex;
                currentActor.targetRow += movey;
                valuereturn = true;
            }
        }
        else if (GameMode == 1)
        {
            int valueTarget = mapArray[currentActor.targetRow + movey][currentActor.targetCol + movex].value;
            ActorItem actoreItemTarget = mapArrayDisable[currentActor.targetRow + movey][currentActor.targetCol + movex];
         bool isBreak = false;
            while (valueTarget == -1)
            {
                if((actoreItemTarget != null && (actoreItemTarget.value == currentActor.value + 10)))
                {
                    isBreak = true;
                }
				else if(actoreItemTarget != null && actoreItemTarget.value != -1&&(actoreItemTarget.value != currentActor.value + 10))
				{
					currentActor.targetCol -= movex;
					currentActor.targetRow -= movey;
					isBreak = true;
				}

                currentActor.targetCol += movex;
                currentActor.targetRow += movey;

                valueTarget = mapArray[currentActor.targetRow + movey][currentActor.targetCol + movex].value;                
                actoreItemTarget = mapArrayDisable[currentActor.targetRow + movey][currentActor.targetCol + movex];

                valuereturn = true;
                if(isBreak)
                    break;
            }
        }

        return valuereturn;

    }

    public bool checkWin()
    {        
        //Debug.Log(targetRow + ",," + targetCol);
        //Debug.Log(movey + ",,," + movex);
        bool valuereturn = true;

        for (int i = 0; i < MAX_ROW; i++)
        {
            for (int j = 0; j < MAX_COL; j++)
            {
				if (mapArray[i][j].value > 0 &&mapArray[i][j].value <=10 && mapArray[i][j].value != mapArrayDisable[i][j].value - 10)
                {
                   // Debug.Log("-----------\n"+mapArray[i][j].value);
                   // Debug.Log(mapArrayDisable[i][j].value);
                    valuereturn = false;
                    return valuereturn;
                }
            }
        }
        return valuereturn;
    }
    
}

