using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
public enum MoveDir
{
    None,
    Up,
    Sler,
    Down
}
public class StrategyManagement : MonoBehaviour {

    public static int capacity;
    public static Stack stackA = new Stack(capacity);
    public static Stack stackB = new Stack(capacity);
    public static Stack stackC = new Stack(capacity);
    public static float moveSpeed = 2f;

    public static int lastDiskOrder = 0, currentDiskOrder = 0;
    public static GameObject lastMovingDisk;

    public static List<string> moveStrategyList;

    static int moveCount;

    /// <summary>
    /// A、B、C三根柱子上圆盘的数目   !!!可以使用stack.count替代
    /// </summary>
    public static Vector3 diskAPos, diskBPos, diskCPos;

    //DiskAnimation--Update函数执行的开关变量
    //StrategyManagement--Update函数执行的开关变量
    public static bool animationMoving,strategyMoving = false;

    void Awake()
    {
        moveStrategyList = new List<string>();
    }

    public static void InitializeData()
    {
        moveCount = 0;
        moveStrategyList.Clear();
        MoveHannoi(capacity, 'A', 'B', 'C');
        strategyMoving = true;
        Debug.Log(moveCount);
    }

    /// <summary>
    /// 汉诺塔移动算法
    /// </summary>
    public static void MoveHannoi(int n,char A,char B,char C)
    {
        if (n == 0) return;
        MoveHannoi(n - 1, A, C, B);
        
        moveStrategyList.Add(A.ToString() + "|" + C.ToString());

        moveCount++;

        MoveHannoi(n - 1, B, A, C);

    }

    void DiskMoving()
    {
        //
        if (!strategyMoving)
            return;
        if (currentDiskOrder == moveStrategyList.Count)
        {
            currentDiskOrder = lastDiskOrder = 0;
            strategyMoving = false;
            return;
        }
        if (lastDiskOrder != currentDiskOrder||currentDiskOrder==0)
        {
            //currentDiskOrder++;
            if (lastMovingDisk != null)
                lastMovingDisk.GetComponent<DiskMoveAnimation>().enabled = false;

            string str = moveStrategyList[currentDiskOrder];
            string[] array = str.Split('|');
            switch (array[0])
            {
                case "A":
                    {
                        GameObject _disk = stackA.Pop() as GameObject;
                        lastMovingDisk = _disk;
                        if (array[1] == "B")
                        {
                            if (_disk.GetComponent<DiskMoveAnimation>() != null)
                                _disk.GetComponent<DiskMoveAnimation>().enabled = true;
                            else
                                _disk.AddComponent<DiskMoveAnimation>();
                            _disk.GetComponent<DiskMoveAnimation>().SetPositon(diskAPos, diskBPos, stackA, stackB);
                            animationMoving = true;
                            stackB.Push(_disk);
                        }
                        if (array[1] == "C")
                        {
                            if (_disk.GetComponent<DiskMoveAnimation>() != null)
                                _disk.GetComponent<DiskMoveAnimation>().enabled = true;
                            else
                                _disk.AddComponent<DiskMoveAnimation>();
                            _disk.GetComponent<DiskMoveAnimation>().SetPositon(diskAPos, diskCPos, stackA, stackC);
                            animationMoving = true;
                            stackC.Push(_disk);
                        }
                        break;
                    }
                case "B":
                    {
                        GameObject _disk = stackB.Pop() as GameObject;
                        if (array[1] == "A")
                        {
                            if (_disk.GetComponent<DiskMoveAnimation>() != null)
                                _disk.GetComponent<DiskMoveAnimation>().enabled = true;
                            else
                                _disk.AddComponent<DiskMoveAnimation>();
                            _disk.GetComponent<DiskMoveAnimation>().SetPositon(diskBPos, diskAPos, stackB, stackA);
                            animationMoving = true;
                            stackA.Push(_disk);
                        }
                        if (array[1] == "C")
                        {
                            if (_disk.GetComponent<DiskMoveAnimation>() != null)
                                _disk.GetComponent<DiskMoveAnimation>().enabled = true;
                            else
                                _disk.AddComponent<DiskMoveAnimation>();
                            _disk.GetComponent<DiskMoveAnimation>().SetPositon(diskBPos, diskCPos, stackB, stackC);
                            animationMoving = true;
                            stackC.Push(_disk);
                        }

                        break;
                    }
                case "C":
                    {
                        GameObject _disk = stackC.Pop() as GameObject;
                        if (array[1] == "A")
                        {
                            if (_disk.GetComponent<DiskMoveAnimation>() != null)
                                _disk.GetComponent<DiskMoveAnimation>().enabled = true;
                            else
                                _disk.AddComponent<DiskMoveAnimation>();
                            _disk.GetComponent<DiskMoveAnimation>().SetPositon(diskCPos, diskAPos, stackC, stackA);
                            animationMoving = true;
                            stackA.Push(_disk);
                        }
                        if (array[1] == "B")
                        {
                            if (_disk.GetComponent<DiskMoveAnimation>() != null)
                                _disk.GetComponent<DiskMoveAnimation>().enabled = true;
                            else
                                _disk.AddComponent<DiskMoveAnimation>();
                            _disk.GetComponent<DiskMoveAnimation>().SetPositon(diskCPos, diskBPos, stackC, stackB);
                            animationMoving = true;
                            stackB.Push(_disk);
                        }
                        break;
                    }
            }
            strategyMoving = false;
        }
    }

    //void OnGUI()
    //{
    //    if (GUI.Button(new Rect(20, 20, 40, 20), "Start"))
    //    {
    //        InitializeData();
    //        isMoving1 = true;
            
    //    }
    //    if (isMoving1 == false)
    //        GUI.Label(new Rect(20, 50, 80, 20), "执行结束");
    //}

    void Update()
    {
        DiskMoving();
    }
}
