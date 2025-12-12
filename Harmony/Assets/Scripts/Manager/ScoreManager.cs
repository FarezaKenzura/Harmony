using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private int _score;
    private int _comboMultiplier = 1;

    private const int PerfectScoreBase = 100;
    private const int GoodScoreBase = 50;

    private void OnEnable()
    {
        SingletonHub.Instance.Get<EventBus>().Subscribe<ComboChangedEvent>(ComboMultiplier);
        SingletonHub.Instance.Get<EventBus>().Subscribe<ComboChangedEvent>(ScoreLevel);
    }

    private void OnDisable()
    {
        SingletonHub.Instance.Get<EventBus>().Unsubscribe<ComboChangedEvent>(ComboMultiplier);
        SingletonHub.Instance.Get<EventBus>().Unsubscribe<ComboChangedEvent>(ScoreLevel);
    }

    private void ComboMultiplier(ComboChangedEvent eventData)
    {
        if (eventData.CurrentCombo >= 30)
        {
            _comboMultiplier = 4;
        }
        else if (eventData.CurrentCombo >= 20)
        {
            _comboMultiplier = 3;
        }
        else if (eventData.CurrentCombo >= 10)
        {
            _comboMultiplier = 2;
        }
        else
        {
            _comboMultiplier = 1;
        }
    }

    private void ScoreLevel(ComboChangedEvent eventData)
    {
        int baseScore = 0;

        switch (eventData.Level)
        {
            case ComboLevel.Perfect:
                baseScore = PerfectScoreBase;
                break;
            case ComboLevel.Good:
                baseScore = GoodScoreBase;
                break;
            case ComboLevel.Miss:
                return;
        }

        int finalScore = baseScore * _comboMultiplier;
        _score += finalScore;
    }
}
