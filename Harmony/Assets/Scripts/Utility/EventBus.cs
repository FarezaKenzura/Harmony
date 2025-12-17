using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void EventAction<T>(T eventData);

[DefaultExecutionOrder(-50)]
public class EventBus : MonoBehaviour
{
    private readonly Dictionary<Type, Delegate> _eventDictionary = new Dictionary<Type, Delegate>();

    private void Awake()
    {
        SingletonHub.Instance.Register(this);
    }

    public void Subscribe<T>(EventAction<T> listener) where T : struct
    {
        Type eventType = typeof(T);

        if (_eventDictionary.TryGetValue(eventType, out Delegate existingDelegate))
        {
            _eventDictionary[eventType] = Delegate.Combine(existingDelegate, listener);
        }
        else
        {
            _eventDictionary.Add(eventType, listener);
        }
    }

    public void Unsubscribe<T>(EventAction<T> listener) where T : struct
    {
        Type eventType = typeof(T);

        if (_eventDictionary.TryGetValue(eventType, out Delegate existingDelegate))
        {
            _eventDictionary[eventType] = Delegate.Remove(existingDelegate, listener);

            if (_eventDictionary[eventType] == null)
            {
                _eventDictionary.Remove(eventType);
            }
        }
    }

    public void Publish<T>(T eventData) where T : struct
    {
        Type eventType = typeof(T);

        if (_eventDictionary.TryGetValue(eventType, out Delegate existingDelegate))
        {
            EventAction<T> action = existingDelegate as EventAction<T>;
            if (action != null)
            {
                action.Invoke(eventData);
            }
        }
    }
}
