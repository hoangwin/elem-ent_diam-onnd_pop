using UnityEngine;
using System.Collections;

public class ActorItem : MonoBehaviour
{
	// Use this for initialization
    public const int STATE_IDE = 0;
    public const int STATE_WAITTING_DROP = 1;
    public const int STATE_MOVE = 2;
    public const int STATE_WAIT_DETROY = 3;
    public const int STATE_DETROY = 4;

	public int value;

	
	public int currentRow;
	public int currentCol;
	public float currentX;
	public float currentY;
    public int  targetCol;
    public int targetRow;
	
	public int state;

    public float move_x = 0;
    public float move_y = 0;    
    

    public SpriteRenderer spriteRenderer;
    public GameObject gameObjectFlag;

    public BoxCollider2D boxCollider2D;
    public ActorItem()
    {
        value = 0;
        currentX = 0;
        currentY = 0;
        currentRow = 0;
        currentCol = 0;

        state = STATE_IDE;
    }
    public ActorItem(int _value, int _row, int _col, int _state)
    {
        value = _value;
        currentRow = _row;
        currentCol = _col;
        state = _state;
    }
    

    public ActorItem(int _value, int _row, int _col, float _currentX, float _currentY, int _state,GameObject obj)
    {

    }

    public void setActorItem(int _value)
    {
        value = _value;
        setAnim();
    }
    public void setActorItem(int _value, int _row, int _col, float _currentX, float _currentY, int _state)
	{
		value = _value;
		currentRow = _row;
		currentCol = _col;
	
		currentX = _currentX;
		currentY = _currentY;
		state = _state;
        gameObject.transform.position = new Vector3(currentX, currentY, 0);
        setAnim();
  
	}
    public void setAnim()
    {
        if (currentCol == GamePlay.MAX_COL - 1 || currentCol == 0 || currentRow == GamePlay.MAX_ROW - 1 || currentRow == 0)//Duong boc o ngoai
        {
            boxCollider2D.enabled = false;
            //spriteRenderer.sprite = GamePlay.instance.LineSprite[0];
         //   Debug.Log("aaaa");
            spriteRenderer.sortingOrder = -2;

        }else   if (value == -1)//o trong
        {
            boxCollider2D.enabled = false;
            spriteRenderer.sprite = GamePlay.instance.LineSprite[0];
            spriteRenderer.sortingOrder = 0;
			
        }
        if(value == 100)//flag
        {
                boxCollider2D.enabled = false;
                spriteRenderer.sprite = GamePlay.instance.FlagSprite;
                spriteRenderer.sortingOrder = 0;
        }
        else if (value > 0 && value < 6)//0 = buc tuong amc dinh
        {
            spriteRenderer.sprite = GamePlay.instance.ItemSpriteEnable[value - 1];
            spriteRenderer.sortingOrder = 2;
        }
        else if (value > 0 && value <= 10)
        {
//            Debug.Log(value - 5 - 1);
			spriteRenderer.sprite = GamePlay.instance.ItemSpriteDisable[value - 5 - 1];
			
        }
        else if (value > 10 && value <= 15)
        {
            //            Debug.Log(value - 5 - 1);
            spriteRenderer.sprite = GamePlay.instance.LineSprite[value - 10];
            spriteRenderer.sortingOrder = 1;
        }
    }
   


    public void Update()
	{
		switch (state)
		{
		case STATE_IDE:
			break;
		case STATE_MOVE:
			if(GamePlay.GameMode == 1)
			{

			}
          //  transform.Translate(3*move_x * Time.deltaTime, 3*move_y * Time.deltaTime, 0);

           // Debug.Log(move_x + "," + move_y);
           // currentX += move_x * SPEED_MOVE;
           // currentY += move_y * SPEED_MOVE;
          //  if (Mathf.Abs(currentX - targetX) <= SPEED_MOVE && Mathf.Abs(currentY - targetY) <= SPEED_MOVE)
            //{
            //    currentX = targetX;
            //    currentY = targetY;
            //    state = STATE_IDE;
           // }
                //can tinhh them o day

        
			break;
         case STATE_WAITTING_DROP:

        
			
            break;
		}
	}
    void Movecompleted()
    {
        
        state = STATE_IDE;
        Debug.Log("Completed MOve");
        if (GamePlay.instance.mapArrayDisable[currentRow][currentCol] != null && value == GamePlay.instance.mapArrayDisable[currentRow][currentCol].value - 5)
        {
            SoundEngine.instance.PlayOneShot(SoundEngine.instance._soundpair);
            GamePlay.instance.mapArrayDisable[currentRow][currentCol].gameObjectFlag.SetActive(true);
        }
        if (GamePlay.instance.checkWin())
        {
            //     Debug.Log("Completed");
            SoundEngine.instance.PlayOneShot(SoundEngine.instance._soundWin);
            GamePlay.instance.textLevelCompleted.text = "Level " + (GamePlay.mcurrentlevel + 1).ToString();
            GamePlay.isCompleted = true;
            UIEffect.instance.showCOmpleted();
            if (GamePlay.GameMode == 0)
            {
                if (GamePlay.mcurrentlevel + 1 > DEF_.ScoreModeClassic.NUM)
                    DEF_.ScoreModeClassic.NUM++;
                DEF_.ScoreModeClassic.Save();
            }
            else
            {
                if (GamePlay.mcurrentlevel + 1 > DEF_.ScoreModeExtra.NUM)
                    DEF_.ScoreModeExtra.NUM++;
                DEF_.ScoreModeExtra.Save();
            }
            ScriptMainMenu.ShowADS();
        }
    }

	public void Start () {
	
	}
	

    public void destroy()
    {
        if (gameObjectFlag!= null)
            GameObject.Destroy(gameObjectFlag);
        GameObject.Destroy(gameObject);
    }
}
