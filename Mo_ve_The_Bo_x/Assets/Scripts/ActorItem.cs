using UnityEngine;
using System.Collections;

public class ActorItem   {
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
    public float targetX;
    public float targetY;

	
	public int state;

    public float move_x = 0;
    public float move_y = 0;    
    public static float SPEED_MOVE = 30*DEF.scaleX;
    
    public GameObject ItemAnim;



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
    public void setActorItem(int _value, int _row, int _col, int _state)
    {
        value = _value;
        currentRow = _row;
        currentCol = _col;
        state = _state;
    }
    public ActorItem(int _value, int _row, int _col, float _currentX, float _currentY, int _state,GameObject _ItemAnim)
	{
		value = _value;
		currentRow = _row;
		currentCol = _col;
	
		currentX = _currentX;
		currentY = _currentY;
		state = _state;
        
        ItemAnim = _ItemAnim;
        ItemAnim.GetComponent<PackedSprite>().DoAnim(value);
        ItemAnim.SetActive(true);
        ItemAnim.transform.localScale = new Vector3(GameEngine.ITEM_WIDTH/110f, GameEngine.ITEM_HEIGHT/110f);//DEF.scaleX, DEF.scaleY, 1);
        
        ItemAnim.transform.position = DEF.Vec3(currentX, currentY, 0);
  
	}

    public void setActorItem(int _value, int _row, int _col, float _currentX, float _currentY, float _targetX, float _targetY, int _state)
    {
        value = _value;
        currentRow = _row;
        currentCol = _col;
     
        currentX = _currentX;
        currentY = _currentY;
        state = _state;
   
       
        ItemAnim.GetComponent<PackedSprite>().DoAnim(value);
        //p.DoAnim(value);
        ItemAnim.transform.localScale = new Vector3(GameEngine.ITEM_WIDTH / 110f, GameEngine.ITEM_HEIGHT / 110f);//DEF.scaleX, DEF.scaleY, 1);
        //ItemAnim.transform.localScale = new Vector3(DEF.scaleX, DEF.scaleY, 1);
        ItemAnim.transform.position = DEF.Vec3(currentX, currentY, ItemAnim.transform.position.z);
      
      
    }


	public void update()
	{
		switch (state)
		{
		case STATE_IDE:
			break;
		case STATE_MOVE:

            currentX += move_x * SPEED_MOVE;
            currentY += move_y * SPEED_MOVE;
            if (Mathf.Abs(currentX - targetX) <= SPEED_MOVE && Mathf.Abs(currentY - targetY) <= SPEED_MOVE)
            {
                currentX = targetX;
                currentY = targetY;
                state = STATE_IDE;
            }

            ItemAnim.transform.position = DEF.Vec3(currentX, currentY, ItemAnim.transform.position.z);
			break;
         case STATE_WAITTING_DROP:

            ItemAnim.transform.position = DEF.Vec3(currentX, currentY, ItemAnim.transform.position.z);
			
            break;
		}
	}
   

	public void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void destroy()
    {
        Object.Destroy(ItemAnim);
    }
}
