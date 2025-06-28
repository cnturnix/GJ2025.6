public enum EventType
{
    ClickBody,
    GetRemain,
}
public class ClickBodyEventArgs
{
    public int BodyID;
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