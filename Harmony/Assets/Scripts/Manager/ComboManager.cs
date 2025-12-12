using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ComboLevel
{
    Perfect,
    Good,
    Miss
}

public class ComboManager : MonoBehaviour
{
    private int _combo = 0;

    private void Awake()
    {
        SingletonHub.Instance.Register(this);
    }

    public void ProcessHit(ComboLevel level)
    {
        if (level == ComboLevel.Perfect || level == ComboLevel.Good)
        {
            _combo++;
        }
        else
        {
            _combo = 0;
        }

        SingletonHub.Instance.Get<EventBus>().Publish(new ComboChangedEvent
        {
            CurrentCombo = _combo,
            Level = level
        });
    }
}
