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

    public GameObject NPC01;
    public GameObject NPC02;
    public GameObject Book;
    public GameObject[] AudioManager;
    [Header("场景变量")]
    public GameObject[] Bodies;
    public GameObject[] Remains;
    public GameObject[] AliveBodies;
    [Header("书中变量")]
    public GameObject[] BookBody;
    public GameObject[] BookAliveBody;
    public GameObject[] BookBodyBG;
    
    public GameObject[] BookRemainBG;
    public GameObject[] BookRemain;
    public GameObject BookRemainParent;
    public GameObject BookRemainDesc;
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
        EventManager.Instance.AddListener<ClickBodyEventArgs>(EventType.ClickBody, OnClickBody);
        EventManager.Instance.AddListener<GetRemainEventArgs>(EventType.GetRemain, OnGetRemain);
    }

    public void OnDestroy()
    {
        EventManager.Instance.RemoveListener<BodyConfirmedEventArgs>(EventType.BodyConfirmed, OnBodyConfirmed);
        EventManager.Instance.RemoveListener<ClickBodyEventArgs>(EventType.ClickBody, OnClickBody);
        EventManager.Instance.RemoveListener<GetRemainEventArgs>(EventType.GetRemain, OnGetRemain);
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
    public void OnClickBody(ClickBodyEventArgs args)
    { 
        BookBodyBG[args.BodyID-1].SetActive(false);
        BookBody[args.BodyID-1].SetActive(true);
    }
    public void OnGetRemain(GetRemainEventArgs args)
    {
        BookRemainBG[args.RemainID-1].SetActive(false);
        BookRemain[args.RemainID-1].SetActive(true);
    }
}