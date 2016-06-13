using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;
public class InitialDisk : MonoBehaviour {

    //[SerializeField]
    //private int diskCount = 3;

    [SerializeField]
    private GameObject disk = null;

    [SerializeField]
    private Transform A, B, C;

    public Transform diskParent;

    public Transform TextTransform;
    public Transform sliderTransform;
    public Transform sliderTextTransform;
	// Use this for initialization
	void Start () {

        if (diskParent == null)
            diskParent = GameObject.Find("DiskList").transform;

        StrategyManagement.diskAPos = A.position;
        StrategyManagement.diskBPos = B.position;
        StrategyManagement.diskCPos = C.position;

        //InitialDiskCount();
	}

    public void InitialDiskCount()
    {
        int _count = System.Convert.ToInt32(TextTransform.GetComponent<Text>().text);
        if (_count < 1)
            StrategyManagement.capacity = 3;
        else
            StrategyManagement.capacity = _count;
        StrategyManagement.stackA.Clear();
        StrategyManagement.stackB.Clear();
        StrategyManagement.stackC.Clear();
        //StrategyManagement.capacity = diskCount;
        InstantiateDisk(_count);
    }
	
    /// <summary>
    /// 初始化圆盘
    /// </summary>
    /// <param name="_count"></param>
    void InstantiateDisk(int _count)
    {
        for (int i = _count-1; i >= 0; i--) 
        {
            GameObject newDisk=(GameObject)Instantiate(disk);
            
            newDisk.transform.localScale = new Vector3(1 + 0.2f * i, 0.2f, 1 + 0.2f * i);
            newDisk.transform.position = new Vector3(-10.5f, 0.7f + 0.4f * (_count - i - 1), 0);
            newDisk.transform.SetParent(diskParent);
            float r = Random.Range(0f, 255f)/255f ;
            float g = Random.Range(0f, 255f)/255f ;
            float b = Random.Range(0f, 255f)/255f ;
            newDisk.GetComponent<MeshRenderer>().material.color = new Color(r, g, b);
            newDisk.transform.name = (i + 1).ToString();
            newDisk.transform.GetChild(0).GetComponent<TextMesh>().text = (i + 1).ToString();
            StrategyManagement.stackA.Push(newDisk);
        }
    }

    public void SetActive(Transform _trans)
    {
        _trans.gameObject.SetActive(false);
        StartCoroutine(WaitSomeTime());
    }
    IEnumerator WaitSomeTime()
    {
        Camera.main.GetComponent<BlurOptimized>().enabled = false;
        yield return new WaitForSeconds(2); 
        StrategyManagement.InitializeData();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }

    public void SetSliderValueOnChanged()
    {
        StrategyManagement.moveSpeed = sliderTransform.GetComponent<Slider>().value;
        sliderTextTransform.GetComponent<Text>().text = Mathf.RoundToInt(StrategyManagement.moveSpeed).ToString();
    }
}
