using UnityEngine;
using System.Collections;

public class ActorItem   {
	// Use this for initialization
    public const int STATE_IDE = 0;
    public const int STATE_WAITTING_DROP = 1;
    public const int STATE_DROP = 2;
    public const int STATE_WAIT_DETROY = 3;
    public const int STATE_DETROY = 4;

	public int value;
	public int targetRow;
	public int targetCol;
	public int currentRow;
	public int currentCol;
	public float currentX;
	public float currentY;
	public float targetX;
	public float targetY;
	public int state;
	public int specialType = -1;
	public static float SPEED_SLOW = 1;
    public static float SPEED_MEDIUM = 1;
    public static float SPEED_FAST = 1;

    public GameObject ItemAnim;
    public GameObject EffectAnim;


    public ActorItem()
    {
        value = 0;
        currentX = 0;
        currentY = 0;
        currentRow = 0;
        currentCol = 0;
        targetX = 0;
        targetY = 0;
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
    public ActorItem(int _value, int _row, int _col, float _currentX, float _currentY, float _targetX, float _targetY, int _state,GameObject _ItemAnim,GameObject _effectAnim)
	{
		value = _value;
		currentRow = _row;
		currentCol = _col;
		targetX = _targetX;
		targetY = _targetY;
		currentX = _currentX;
		currentY = _currentY;
		state = _state;
		specialType = -1;
        
        ItemAnim = _ItemAnim;
		EffectAnim = _effectAnim;
        ItemAnim.GetComponent<PackedSprite>().DoAnim(value);
        //p.DoAnim(value);
        ItemAnim.transform.localScale = new Vector3(DEF.scaleX, DEF.scaleY, 1);
        ItemAnim.transform.position = DEF.Vec3(currentX, currentY, 0);

        EffectAnim.transform.localScale = new Vector3(DEF.scaleX, DEF.scaleY, 1);
        EffectAnim.transform.position = DEF.Vec3(_targetX, _targetY, 5);
		EffectAnim.GetComponent<PackedSprite>().DoAnim(value);
        EffectAnim.SetActive(false);
       
	}

    public void setActorItem(int _value, int _row, int _col, float _currentX, float _currentY, float _targetX, float _targetY, int _state)
    {
        value = _value;
        currentRow = _row;
        currentCol = _col;
        targetX = _targetX;
        targetY = _targetY;
        currentX = _currentX;
        currentY = _currentY;
        state = _state;
        specialType = -1;
        
     //   ItemAnim = _ItemAnim;
       
        ItemAnim.GetComponent<PackedSprite>().DoAnim(value);
        //p.DoAnim(value);
        ItemAnim.transform.localScale = new Vector3(DEF.scaleX, DEF.scaleY, 1);
        ItemAnim.transform.position = DEF.Vec3(currentX, currentY, 0);
      
        EffectAnim.transform.localScale = new Vector3(DEF.scaleX, DEF.scaleY, 1);
        EffectAnim.transform.position = DEF.Vec3(_targetX, _targetY, 5);
        EffectAnim.GetComponent<PackedSprite>().DoAnim(value);
        EffectAnim.SetActive(false);
    }


	public void update()
	{
		switch (state)
		{
		case STATE_IDE:
			break;
		case STATE_DROP:
			
          //  if( currentRow ==0&& currentCol==0)
          //          Debug.Log(targetY + " " + currentY); 
            
            if (currentY >= targetY)//&& sprite.hasAnimationFinished(_currentAnimation, _currentFrame, _waitDelay));
            {
            //    state = STATE_WAITTING_DROP;
            //    currentY = targetY - SPEED_SLOW*2;
                state = STATE_IDE;
                currentY = targetY;
				EffectAnim.transform.localScale = new Vector3(DEF.scaleX , DEF.scaleY , 1);
				 EffectAnim.SetActive(true);
              	EffectAnim.GetComponent<PackedSprite>().DoAnim(value);               
                EffectAnim.SetActive(false);
				
            }
			float deltaScaleX =  DEF.scaleX / 30;
			float deltaScaleY =  DEF.scaleY / 30;
			if (currentY < targetY)
			{
				if ((EffectAnim.transform.localScale.x -deltaScaleX >0) && (EffectAnim.transform.localScale.y - deltaScaleY>0))
                	 EffectAnim.transform.localScale = new Vector3(EffectAnim.transform.localScale.x - deltaScaleX,EffectAnim.transform.localScale.y - deltaScaleY, 1);
				else
					EffectAnim.SetActive(false);
                if (GameEngine.isStartGame)
                    currentY += SPEED_MEDIUM;    
                else
				    currentY += SPEED_FAST;    

			}
                
			
            ItemAnim.transform.position = DEF.Vec3(currentX, currentY, 0);
			break;
         case STATE_WAITTING_DROP:
            if (currentY < targetY)
				currentY += SPEED_SLOW;            
			if (currentY >= targetY)//&& sprite.hasAnimationFinished(_currentAnimation, _currentFrame, _waitDelay));
			{
				currentY = targetY;
				state = STATE_IDE;
			}
            ItemAnim.transform.position = DEF.Vec3(currentX, currentY, 0);
			
            break;
		}
	}
    /*
    public void paint(Canvas c)
    {

        switch (state)
        {
            case STATE_IDE:
                if (value >= 0)
                {
                    StateGameplay.spriteFruit.drawAFrame(c, value, currentX, currentY);
                    if (specialType >= 0)
                    {
                        //Log.d("specialType " ,"specialType id " +specialType+",x = "+ currentX +",y="+ currentY);
                        int temp = 0;// GameLib.frameCountCurrentState%2;
                        if (specialType == 0)
                            StateGameplay.spriteFruit.drawAFrame(c, 6 + temp, currentX, currentY);
                        else if (specialType == 1)

                            StateGameplay.spriteFruit.drawAFrame(c, 8 + temp, currentX, currentY);
                        else
                            StateGameplay.spriteFruit.drawAFrame(c, 10 + temp, currentX, currentY);
                    }
                }
                sprite.drawAnim(c, this);
                break;
            case STATE_DROP:
                if (value >= 0)
                {
                    StateGameplay.spriteFruit.drawAFrame(c, value, currentX, currentY);
                    if (specialType >= 0)
                    {
                        int temp = 0;// GameLib.frameCountCurrentState%2;
                        if (specialType == 0)
                            StateGameplay.spriteFruit.drawAFrame(c, 6 + temp, currentX, currentY);
                        else if (specialType == 1)

                            StateGameplay.spriteFruit.drawAFrame(c, 8 + temp, currentX, currentY);
                        else
                            StateGameplay.spriteFruit.drawAFrame(c, 10 + temp, currentX, currentY);
                    }
                }
                sprite.drawAnim(c, this);
                //	c.drawText("+10", targetX, targetY, StateGameplay.android_FontSmall);
                break;
        }

    }
     */

	public void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
