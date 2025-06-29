public enum EventType
{
    ClickBody,
    GetRemain,
    OpenBook,
    BodyConfirmed,
}
public class ClickBodyEventArgs
{
    public int BodyID;

    public ClickBodyEventArgs(int i)
    {
        BodyID = i;
    }
}
// public void Start()
// {
//     EventManager.Instance.AddListener<ClickBodyEventArgs>(EventType.ClickBody, OnClickBody);
// }
// public void OnDestroy()
// {
//     EventManager.Instance.RemoveListener<ClickBodyEventArgs>(EventType.ClickBody, OnClickBody);
// }
//EventManager.Instance.TriggerEvent(EventType.ClickBody,new ClickBodyEventArgs(1));

public class GetRemainEventArgs
{
    public int RemainID;

    public GetRemainEventArgs(int i)
    {
        RemainID = i;
    }
}
// public void Start()
// {
//     EventManager.Instance.AddListener<GetRemainEventArgs>(EventType.GetRemain, OnGetRemain);
// }
// public void OnDestroy()
// {
//     EventManager.Instance.RemoveListener<GetRemainEventArgs>(EventType.GetRemain, OnGetRemain);
// }
//EventManager.Instance.TriggerEvent(EventType.GetRemain,new GetRemainEventArgs(1));
public class OpenBookEventArgs
{
    public bool FirstTime;
    public bool open;

    public OpenBookEventArgs(bool i,bool j)
    {
        FirstTime = i;
        open = j;
    }
}
// public void Start()
// {
//     EventManager.Instance.AddListener<OpenBookEventArgs>(EventType.OpenBook, OnOpenBook);
// }
// public void OnDestroy()
// {
//     EventManager.Instance.RemoveListener<OpenBookEventArgs>(EventType.OpenBook, OnOpenBook);
// }
//EventManager.Instance.TriggerEvent(EventType.OpenBook,new OpenBookEventArgs(true));
public class BodyConfirmedEventArgs
{
    public int BodyId; 

    public BodyConfirmedEventArgs(int i)
    {
        BodyId = i;
    }
}
// public void Start()
// {
//     EventManager.Instance.AddListener<BodyConfirmedEventArgs>(EventType.BodyConfirmed, OnBodyConfirmed);
// }
// public void OnDestroy()
// {
//     EventManager.Instance.RemoveListener<BodyConfirmedEventArgs>(EventType.BodyConfirmed, OnBodyConfirmed);
// }
//EventManager.Instance.TriggerEvent(EventType.BodyConfirmed,new BodyConfirmedEventArgs(1));
