using System;
using System.Collections.Generic;
using UnityEngine;

// public void Start()
// {
//     EventManager.Instance.AddListener<Vector2>(EventType.CameraMove, OnPlayerMove);
// }
// public void OnDestroy()
// {
//     EventManager.Instance.RemoveListener<Vector2>(EventType.CameraMove, OnPlayerMove);
// }

// public enum EventType
// {
//
// }
//EventManager.Instance.TriggerEvent(EventType.TriggerDialogue,new TriggerDialogueEventArgs(DialogeEvent.EnterLevel, GetComponent<Block>()));


public class EventManager : MonoBehaviour
{
    private static EventManager instance;
    public static EventManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<EventManager>();
                if (instance == null)
                {
                    GameObject go = new GameObject("EventManager");
                    instance = go.AddComponent<EventManager>();
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
            // 清理所有事件监听
            ClearEvents();
        }
    }
    // 事件字典：无参数事件
    private Dictionary<EventType, Action> eventDictionary = new Dictionary<EventType, Action>();

    // 事件字典：带参数事件
    private Dictionary<EventType, Dictionary<Type, Delegate>> paramEventDictionary
        = new Dictionary<EventType, Dictionary<Type, Delegate>>();



    #region 无参数事件
    // 添加监听
    public void AddListener(EventType eventType, Action listener)
    {
        if (!eventDictionary.ContainsKey(eventType))
        {
            eventDictionary.Add(eventType, null);
        }
        eventDictionary[eventType] += listener;
    }

    // 移除监听
    public void RemoveListener(EventType eventType, Action listener)
    {
        if (eventDictionary.ContainsKey(eventType))
        {
            eventDictionary[eventType] -= listener;
        }
    }

    // 触发事件
    public void TriggerEvent(EventType eventType)
    {
        if (eventDictionary.ContainsKey(eventType))
        {
            eventDictionary[eventType]?.Invoke();
        }
    }
    #endregion

    #region 带参数事件
    // 添加带参数的监听
    public void AddListener<T>(EventType eventType, Action<T> listener)
    {
        if (!paramEventDictionary.ContainsKey(eventType))
        {
            paramEventDictionary.Add(eventType, new Dictionary<Type, Delegate>());
        }

        var type = typeof(T);
        if (!paramEventDictionary[eventType].ContainsKey(type))
        {
            paramEventDictionary[eventType].Add(type, null);
        }

        paramEventDictionary[eventType][type]
            = Delegate.Combine(paramEventDictionary[eventType][type], listener);
    }

    // 移除带参数的监听
    public void RemoveListener<T>(EventType eventType, Action<T> listener)
    {
        if (paramEventDictionary.ContainsKey(eventType))
        {
            var type = typeof(T);
            if (paramEventDictionary[eventType].ContainsKey(type))
            {
                paramEventDictionary[eventType][type]
                    = Delegate.Remove(paramEventDictionary[eventType][type], listener);
            }
        }
    }

    // 触发带参数的事件
    public void TriggerEvent<T>(EventType eventType, T parameter)
    {
        if (paramEventDictionary.ContainsKey(eventType))
        {
            var type = typeof(T);
            if (paramEventDictionary[eventType].ContainsKey(type))
            {
                var action = paramEventDictionary[eventType][type] as Action<T>;
                action?.Invoke(parameter);
            }
        }
    }
    #endregion

    // 清除所有事件
    public void ClearEvents()
    {
        eventDictionary.Clear();
        paramEventDictionary.Clear();
    }
}