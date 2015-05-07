using UnityEngine;
using System.Collections;

public class GamePlay : MonoBehaviour
{

    // Use this for initialization
    public GameObject frefabItem;//item binh thuong go, nen hoac item ko di chuyen
	//public GameObject frefabItemMove;//item di chuyen khac nhau la the m rigibody2d

    GameEngine gameEngine;

    public static int MAX_COL = 7;
    public static int MAX_ROW = 8;
    public static int MAX_ITEM = 4;
    public static float ITEM_WIDTH = 60;
    public static float ITEM_HEIGHT = 60;
    //SpriteRenderer a;
    public UnityEngine.Sprite WallSprite;
    public UnityEngine.Sprite[] LineSprite;
    public UnityEngine.Sprite[] ItemSpriteEnable;
    public UnityEngine.Sprite[] ItemSpriteDisable;
    public ActorItem[][] mapArray;//luu nhung gia tri co the di chuyen + ban do
	public ActorItem[][] mapArrayDisable;//mang luu nhung gia tri dung yen
    public Transform tranformObjSelect;

    public static bool isIGM;


    public static bool isCompleted;

    public static bool isHint = true;
    public static float mHintx = 0;
    public static float mHinty = 0;
    public static float mHintBeginx = 0;
    public static float mHintBeginy = 0;
    public static float mHintEndx = 0;
    public static float mHintEndy = 0;
    public static int mHintCount = 0;

    public static float BEGIN_X = 0;
    public static float BEGIN_Y = 0;

    static bool isFistInit = false;
    public static GamePlay instance;


    public static int COL_SELECT = -1;
    public static int ROW_SELECT = -1;
    public static Vector3 beginInput;//input touch
    public static Vector3 endInput;//touch input
    public static bool isSelectItem = false;

    public static ArrayList arrayList = new ArrayList();


    public static ArrayList arrayListUndo = new ArrayList();

    public static ActorItem currentActor;
    void Start()
    {
        instance = this;
        DEF.init();
        //gameEngine = new GameEngine();
        // GameEngine.targetScore = getTargetScore();


        //  GameEngine.mcurrentlevel = 1;
        GameEngine.mcurrentlevel = 3;
        InitCommondBegin();
        mapArray = new ActorItem[MAX_ROW][];
		mapArrayDisable =  new ActorItem[MAX_ROW][];
        for (int i = 0; i < MAX_ROW; i++)
        {
            mapArray[i] = new ActorItem[MAX_COL];
			mapArrayDisable[i] = new ActorItem[MAX_COL];

        }

        //  for (int n = 0; n < arrayList.Count; n++)
        //  {

        //     ((ActorItem)(arrayList[n])).destroy();
        //     arrayList[n] = null;
        //  }
        //  arrayList.Clear();

        //FOR UNDO
        //  for (int n = 0; n < arrayListUndo.Count; n++)
        //  {
        //      arrayListUndo[n] = null;
        //  }
        //  arrayListUndo.Clear();

        for (int i = 0; i < MAX_ROW; i++)
        {
            for (int j = 0; j < MAX_COL; j++)
            {
                //    Debug.Log(GameEngine.mcurrentlevel);
                int value = ScriptDataLevel.levels[GameEngine.mcurrentlevel][i][j];
                //mapArrayTarget[i][j] = ScriptDataLevel.levels[GameEngine.mcurrentlevel][i][j];
                // if (mapArray[i][j] > 0 && mapArray[i][j] <= 5)
                {
                    GameObject obj;
                    ActorItem actor;
                    //arrayList.Add(new 

					if ( value < 6)
					{
	                    obj = (GameObject)(Instantiate(frefabItem));
	                    actor = obj.GetComponent<ActorItem>();
	                    float x = BEGIN_X + j * ITEM_WIDTH + ITEM_WIDTH / 2;
	                    float y = BEGIN_Y + i * ITEM_HEIGHT + ITEM_HEIGHT / 2;
	                    actor.setActorItem(value, i, j, x, y, ActorItem.STATE_IDE);
	                    actor.gameObject.SetActive(true);                    
	                    actor.spriteRenderer.sortingOrder = 2;
	                    //  arrayList.Add(actor);
	                    mapArray[i][j] = actor;
					}
					else
					{
						//add -1
						obj = (GameObject)(Instantiate(frefabItem));
						actor = obj.GetComponent<ActorItem>();
						float x = BEGIN_X + j * ITEM_WIDTH + ITEM_WIDTH / 2;
						float y = BEGIN_Y + i * ITEM_HEIGHT + ITEM_HEIGHT / 2;
						actor.setActorItem(-1, i, j, x, y, ActorItem.STATE_IDE);
						actor.gameObject.SetActive(true);                    
						actor.spriteRenderer.sortingOrder = 2;

						mapArray[i][j] = actor;
						//add an

						obj = (GameObject)(Instantiate(frefabItem));
						actor = obj.GetComponent<ActorItem>();
						x = BEGIN_X + j * ITEM_WIDTH + ITEM_WIDTH / 2;
						y = BEGIN_Y + i * ITEM_HEIGHT + ITEM_HEIGHT / 2;
						actor.setActorItem(value, i, j, x, y, ActorItem.STATE_IDE);
						actor.gameObject.SetActive(true);                    
						actor.spriteRenderer.sortingOrder = 1;
						//  arrayList.Add(actor);
						mapArrayDisable[i][j] = actor;
					}
					
				}
			}
		}
		
		
	}
	public void InitCommondBegin()
	{
		MAX_ROW = ScriptDataLevel.levels[GameEngine.mcurrentlevel].Length;
        MAX_COL = ScriptDataLevel.levels[GameEngine.mcurrentlevel][0].Length;

        Camera cam = Camera.main;
        float height = 2f * cam.orthographicSize;
        float width = height * cam.aspect;
        ITEM_WIDTH = width/MAX_COL;
        Debug.Log(MAX_COL);
        float  scale = ITEM_WIDTH/ ((Collider2D)(frefabItem.GetComponent<Collider2D>())).bounds.size.x + 0.01f;
        Debug.Log(scale);
        frefabItem.transform.localScale = new Vector3(frefabItem.transform.localScale.x*scale, frefabItem.transform.localScale.y* scale, 1);
        //frefabItemMove.transform.localScale = new Vector3(frefabItemMove.transform.localScale.x * scale, frefabItemMove.transform.localScale.y * scale, 1);

        if (!isFistInit)
        {
            isFistInit = true;
            ITEM_WIDTH = ((Collider2D)(frefabItem.GetComponent<Collider2D>())).bounds.size.x + 0.01f;
            ITEM_HEIGHT = ((Collider2D)(frefabItem.GetComponent<Collider2D>())).bounds.size.y + 0.01f;
            frefabItem.SetActive(false);
          //  frefabItemMove.SetActive(false);
            float x = MAX_COL *ITEM_WIDTH ;
            float y = MAX_ROW * ITEM_HEIGHT - ITEM_WIDTH;
            BEGIN_X = -x / 2;
             
            BEGIN_Y = -y / 2;//3 la offset
        }
    }

    void PlayGame()
    {
        gameEngine.Start();
        isIGM = false;
        isCompleted = false;
        if (GameEngine.mcurrentlevel == 0)
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
    public static int getTargetScore()
    {
        return (GameEngine.mcurrentlevel + 1) * 1000 + 500;
    }
    // Update is called once per frame
    void Update()
    {
        if (!isIGM && !isCompleted)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                isIGM = true;
            }
        }
        else if (isIGM)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                isIGM = false;
            }
        }
        else if (isCompleted)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.LoadLevel("SceneMainMenu");
            }
        }

        if (isSelectItem)
        {
            moveRowColSelect();
        }
        else
        {
            getRowColSelect();
        }
       
        // gameEngine.Update();
    }
    public void getRowColSelect()
    {
        ROW_SELECT = -1;
        COL_SELECT = -1;
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            //Vector3 beginInput = Camera.main.WorldToScreenPoint(Input.mousePosition);
            beginInput = Camera.main.ScreenToWorldPoint(Input.touches[0].position );

            //float x = BEGIN_X + j * ITEM_WIDTH + ITEM_WIDTH / 2;
            //float y = BEGIN_Y + i * ITEM_HEIGHT + ITEM_HEIGHT / 2;
           // Debug.Log("1111:" + beginInput.x);
            ROW_SELECT = (int)((beginInput.y - BEGIN_Y) / ITEM_HEIGHT);
            COL_SELECT = (int)((beginInput.x - BEGIN_X) / ITEM_WIDTH);
            Debug.Log(ROW_SELECT + " :" + COL_SELECT);

        }
        else if (Input.GetMouseButtonDown(0))
        {
            //Vector3 beginInput = Camera.main.WorldToScreenPoint(Input.mousePosition);
            beginInput = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //float x = BEGIN_X + j * ITEM_WIDTH + ITEM_WIDTH / 2;
            //float y = BEGIN_Y + i * ITEM_HEIGHT + ITEM_HEIGHT / 2;
          //  Debug.Log("222 :" + beginInput.x);
            ROW_SELECT = (int)((beginInput.y - BEGIN_Y ) / ITEM_HEIGHT);
            COL_SELECT = (int)((beginInput.x - BEGIN_X ) / ITEM_WIDTH);
            Debug.Log(ROW_SELECT + " :" + COL_SELECT);


        }
		if (ROW_SELECT >= 0 && ROW_SELECT < MAX_ROW && COL_SELECT >= 0 && COL_SELECT < MAX_COL) {
						currentActor = mapArray [ROW_SELECT] [COL_SELECT];
			if (currentActor.value < 5 && currentActor.value >0) {
								isSelectItem = true;
								//Debug.Log("Ten Ten Ten" + ROW_SELECT + " ," + COL_SELECT + "," + currentActor.value);
								//        break;
						}
				}
        for (int i = 0; i < arrayList.Count; i++)
        {
            currentActor = (ActorItem)(arrayList[i]);
            if (currentActor.currentCol == COL_SELECT && currentActor.currentRow == ROW_SELECT)
            {
                if (currentActor.value < 5)
                {
                    isSelectItem = true;
                    //Debug.Log("Ten Ten Ten" + ROW_SELECT + " ," + COL_SELECT + "," + currentActor.value);
                    break;
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
            Debug.Log(detalx + ",,,," + detaly);
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
                DEF.playSounEffect(0);
                //stateInGamePlay = STATE_MOVE;
                currentActor.state = ActorItem.STATE_MOVE;

				//chuyen doi  o gia tri tai day
				int tempvalue1  = 	mapArray[currentActor.targetRow][currentActor.targetCol].value;
				int tempvalue2  = 	mapArray[currentActor.currentRow][currentActor.currentCol].value;
				mapArray[currentActor.targetRow][currentActor.targetCol].setActorItem(tempvalue2);//, currentActor.currentRow, currentActor.currentCol, float _currentX, float _currentY, int _state//)
				mapArray[currentActor.currentRow][currentActor.currentCol].setActorItem(tempvalue1);

				// lay toa do luc dau
				float x = BEGIN_X + currentActor.currentCol * ITEM_WIDTH + ITEM_WIDTH / 2;;
				float y = BEGIN_Y + currentActor.currentRow * ITEM_WIDTH + ITEM_HEIGHT / 2;;
				//
				//mapArray[currentActor.currentRow][currentActor.currentCol].spriteRenderer.sortingOrder = 0;
				currentActor = mapArray[currentActor.targetRow][currentActor.targetCol];
				//currentActor.spriteRenderer.sortingOrder = 1;
				//xong doi gia tri
				// chuyen tu toa do dau sang toa do moi
				iTween.MoveFrom(currentActor.gameObject, iTween.Hash("x", x,"y", y, "speed", 5, "EaseType", "linear", "oncomplete", "Movecompleted"));


            }
            Debug.Log("MOVE HERE");
        }

    }

    public bool checkNewTarget(int movex, int movey)
    {


		currentActor.targetRow = currentActor.currentRow;
		currentActor.targetCol = currentActor.currentCol;
        //Debug.Log(targetRow + ",," + targetCol);
        //Debug.Log(movey + ",,," + movex);
        bool valuereturn = false;
		while (mapArray[currentActor.targetRow + movey][currentActor.targetCol + movex].value == -1 || mapArray[currentActor.targetRow + movey][currentActor.targetCol + movex].value >= 06)
        {
			currentActor.targetCol += movex;
			currentActor.targetRow += movey;
            valuereturn = true;
        }

    //    if (valuereturn)//here can set lai cai map cho dung. khong dung thi toi
    //    {
    //       mapArray[targetRow][targetCol] = mapArray[currentActor.currentRow][currentActor.currentCol];
     //       mapArray[currentActor.currentRow][currentActor.currentCol].value = -1;
    //    }
    //    if (currentActor.currentRow != targetRow || currentActor.currentCol != targetCol)
    //    {
    //        Debug.Log(("hehe : " + currentActor.currentRow + " ," + currentActor.currentCol + "," + targetRow + "," + targetCol));
      //      arrayListUndo.Add(new Rect(currentActor.currentRow, currentActor.currentCol, targetRow, targetCol));
      //  }
    //    currentActor.currentRow = targetRow;
    //    currentActor.currentCol = targetCol;

    //    currentActor.targetX = BEGIN_X + targetCol * ITEM_WIDTH + ITEM_WIDTH / 2;
    //    currentActor.targetY = BEGIN_Y + targetRow * ITEM_HEIGHT + ITEM_HEIGHT / 2;
        //change value in mapraay

        // Debug.Log(targetRow + ",," + targetCol);

        return valuereturn;

    }
    void OnGUI()
    {
      

        float x = 0;
        float y = 0;
        for (int i = 0; i < GameEngine.MAX_ROW; i++)
        {
            for (int j = 0; j < GameEngine.MAX_COL; j++)
            {
                x = GameEngine.BEGIN_X + j * GameEngine.ITEM_WIDTH;
                y = GameEngine.BEGIN_Y + i * GameEngine.ITEM_HEIGHT;
              
            }
        }

        if (GameEngine.mcurrentlevel > GameEngine.mUnlocklevel)
        {
      //      GUI.DrawTexture(new Rect(0 * DEF.scaleX, 230 * DEF.scaleY, Screen.width, 1100 * DEF.scaleY), Blocked_BackGround);
       //     GUI.DrawTexture(new Rect(0 * DEF.scaleX, 230 * DEF.scaleY, 400 * DEF.scaleX, 400 * DEF.scaleY), Blocked);
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

    void DrawQuad(Rect position, Color color)
    {
        Texture2D texture = new Texture2D(1, 1);
        texture.SetPixel(0, 0, color);
        texture.Apply();
        GUI.skin.box.normal.background = texture;
        GUI.Box(position, GUIContent.none);
    }
}

