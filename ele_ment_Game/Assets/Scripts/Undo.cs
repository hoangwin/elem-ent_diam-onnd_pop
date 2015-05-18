using UnityEngine;
using System.Collections;

public class Undo : MonoBehaviour {

	// Use this for initialization
    public static ArrayList listNodeUndo = new ArrayList();
    public static Undo instance;
	void Start () {
        instance = this;
        //listNodeUndo.Add(new NodeUndo());
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void clearAllUndo()
    {
        while(listNodeUndo.Count >0)
        {
            
            GameObject.Destroy((Object)(listNodeUndo[0]));
            listNodeUndo.RemoveAt(0);
        }
        listNodeUndo.Clear();

    }
    public void AddUndo(int col1,int row1,int col2,int row2)
    {

        NodeUndo o = new NodeUndo(col1, row1, col2, row2);
        listNodeUndo.Add(o);
         //   GameObject.Destroy((Object)(listNodeUndo[0]));
         //   listNodeUndo.RemoveAt(0);      
         //   listNodeUndo.Clear();
    }

    public void RemoveNode(int i)
    {     
        GameObject.Destroy((Object)(listNodeUndo[i]));
       
        
        listNodeUndo.RemoveAt(i);
    }
    public void UndoAStep()
    {
        if(listNodeUndo.Count>0)
        {
            NodeUndo objNode = (NodeUndo)(listNodeUndo[listNodeUndo.Count - 1]);
            int row1= (int)objNode.posCurent.y;
            int col1= (int)objNode.posCurent.x;
            int row2 = (int)objNode.posPre.y;
            int col2 = (int)objNode.posPre.x;
            int tempvalue1 = GamePlay.instance.mapArray[row1][col1].value;
            int tempvalue2 = GamePlay.instance.mapArray[row2][col2].value;

            if (GamePlay.instance.mapArrayDisable[row1][col1] != null && GamePlay.instance.mapArray[row1][col1].value == GamePlay.instance.mapArrayDisable[row1][col1].value - 5)
            {
                GamePlay.instance.mapArrayDisable[row1][col1].gameObjectFlag.SetActive(false);
            }


            GamePlay.instance.mapArray[row1][col1].setActorItem(tempvalue2);//, currentActor.currentRow, currentActor.currentCol, float _currentX, float _currentY, int _state//)
            GamePlay.instance.mapArray[row2][col2].setActorItem(tempvalue1);

            ActorItem objActorItem = GamePlay.instance.mapArray[row1][col1];

        

            objActorItem.state = ActorItem.STATE_IDE;

        //xong doi gia tri
        // chuyen tu toa do dau sang toa do moi
            iTween.Stop(objActorItem.gameObject);
       // iTween.MoveFrom(currentActor.gameObject, iTween.Hash("x", x, "y", y, "speed", 5, "EaseType", "linear", "oncomplete", "Movecompleted"));
            RemoveNode(listNodeUndo.Count - 1);
            //reset lai tat ca bang -1
            int tempx = col1 - col2;
            int tempy = row1 - row2;
            if (tempx!=0)
                tempx = -tempx / Mathf.Abs(tempx);
            if (tempy != 0)
                tempy = -tempy / Mathf.Abs(tempy);

          
            while(col1 != col2 || row1 != row2)
            {
                Debug.Log("Undo:" + col1 + "," + col2 + "," + row1 + "," + row2);
                GamePlay.instance.mapArray[row1][col1].setActorItem(-1);
                col1 += tempx;
                row1 += tempy;
            }
        }
    }

}
public class NodeUndo : MonoBehaviour
{
    public Vector2 posPre;
    public Vector2 posCurent;
    public NodeUndo(int col1, int row1, int col2, int row2)
    {
        posPre = new Vector2(col1, row1);
        posCurent = new Vector2(col2, row2);
    }
}
