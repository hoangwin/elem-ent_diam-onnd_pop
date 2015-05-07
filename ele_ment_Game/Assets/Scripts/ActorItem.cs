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
    public static float SPEED_MOVE = 30*DEF.scaleX;

    public SpriteRenderer spriteRenderer;

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
        if (value == -1)//o trong
        {
            boxCollider2D.enabled = false;
            spriteRenderer.sprite = GamePlay.instance.LineSprite[0];
			
        }
        else if (value > 0 && value < 6)//0 = buc tuong amc dinh
        {
            spriteRenderer.sprite = GamePlay.instance.ItemSpriteEnable[value - 1];
			
        }
        else if (value > 0 && value <= 10)
        {
            Debug.Log(value - 5 - 1);
			spriteRenderer.sprite = GamePlay.instance.ItemSpriteDisable[value - 5 - 1];
			
        }
    }
   


    public void Update()
	{
		switch (state)
		{
		case STATE_IDE:
			break;
		case STATE_MOVE:
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
		Debug.Log ("Completed");
	}
    void OnCollisionEnter2D(Collision2D other)
    {
	//	if (this == GamePlay.currentActor) {
	//					if ((other.gameObject.GetComponent<ActorItem> ()).value >= 0)
	//							Debug.Log ("aaa" + (other.gameObject.GetComponent<ActorItem> ()).currentRow + "," + (other.gameObject.GetComponent<ActorItem> ()).currentCol);//characterInQuicksand = true;
	//			}
	}
	public void Start () {
	
	}
	

    public void destroy()
    {
        
    }
}
