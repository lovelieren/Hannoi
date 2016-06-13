using UnityEngine;
using System.Collections;

public class DiskMoveAnimation : MonoBehaviour {

	// Use this for initialization

    private Vector3 pos1;
    private Vector3 pos2;

    public Stack sourceStack;
    public Stack targetStack;
    

    MoveDir currentDir = MoveDir.Up;
    public void SetPositon(Vector3 pos1, Vector3 pos2,Stack source,Stack target)
    {
        this.pos1 = pos1;
        this.pos2 = pos2;
        this.sourceStack = source;
        this.targetStack = target;
        currentDir = MoveDir.Up;
    }
    float time;

	
	void Update () {
        if (StrategyManagement.animationMoving)
        {
            switch (currentDir)
            {
                case MoveDir.Up:
                    if (Vector3.Distance(transform.position,pos1)<0.1f)
                        currentDir = MoveDir.Sler;
                    //transform.position += Vector3.up * StrategyManagement.moveSpeed * Time.deltaTime;
                    transform.position = Vector3.Lerp(transform.position, pos1, StrategyManagement.moveSpeed * Time.deltaTime);
                    break;
                case MoveDir.Sler:
                    if (Vector3.Distance(transform.position, pos2) < 0.1)
                        currentDir = MoveDir.Down;
                    transform.position = Vector3.Slerp(transform.position, pos2, StrategyManagement.moveSpeed * Time.deltaTime);time = Time.time;
                    break;
                case MoveDir.Down:
                    //查询目标柱目前堆放的高度
                    
                    int _count = targetStack.Count;
                    //transform.position += Vector3.down * StrategyManagement.moveSpeed * Time.deltaTime;
                    transform.position = Vector3.Lerp(transform.position, new Vector3(pos2.x, _count * 0.4f + 0.3f, pos2.z), StrategyManagement.moveSpeed * Time.deltaTime);
                    //Debug.Log((Time.time-time).ToString()+"        "+Mathf.Abs(transform.position.y- (_count) * 0.4f - 0.3f));
                    if (Mathf.Abs(transform.position.y - (_count) * 0.4f - 0.3f)<0.01)
                    {
                        currentDir = MoveDir.None;
                        StrategyManagement.animationMoving = false;
                        StrategyManagement.strategyMoving = true;
                        StrategyManagement.currentDiskOrder++;
                    }
                    break;
                default:
                    break;
            }
        }

    }
}
