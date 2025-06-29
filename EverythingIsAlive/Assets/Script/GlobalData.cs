using Unity.VisualScripting;
using UnityEngine;

public class GlobalData:MonoBehaviour
{
    // 单例实例
    private static GlobalData _instance;

    // 公共访问属性
    public static GlobalData Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("GlobalData");
                _instance = go.AddComponent<GlobalData>();
            }
            return _instance;
        }
    }
    
    //内描边材质
    public Material M_Outline;
    //默认材质
    public Material M_Defalut;
    public GameObject Mark;
    public GameObject wall;
    public GameObject[] Bodies;
    public GameObject[] Remains;
    public GameObject[] AliveBodies;
    public GameObject NPC01;
    public GameObject NPC02;
    public GameObject Book;
    public GameObject[] AudioManager;
    public bool[] FoundBody;
    public bool[] FoundRemain;
    public GameObject[] Body;
    public GameObject[] RemainBG;
    public GameObject Remain;
    public GameObject RemainDesc;
    void Awake()
    {
        // 确保只有一个实例存在
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject); // 保持跨场景存在
        }
    }
    public void Start()
    {
        EventManager.Instance.AddListener<BodyConfirmedEventArgs>(EventType.BodyConfirmed, OnBodyConfirmed);
    }

    public void OnDestroy()
    {
        EventManager.Instance.RemoveListener<BodyConfirmedEventArgs>(EventType.BodyConfirmed, OnBodyConfirmed);
    }
    
    public void OnBodyConfirmed(BodyConfirmedEventArgs args)
    {
        if (Mark.GetComponent<ButtonClick>().FirstTime)
        {
            NPC02.GetComponentInChildren<NPC03Dialog>().Body01Confirmed();
        }
        Bodies[args.BodyId].SetActive(false);
        AliveBodies[args.BodyId].SetActive(true);
       
    }
}