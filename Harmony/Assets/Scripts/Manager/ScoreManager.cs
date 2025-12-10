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
        SingletonHub.Instance.Get<EventBus>().Subscribe<ComboChangedEvent>(ComboRaise);
    }

    private void OnDisable()
    {
        SingletonHub.Instance.Get<EventBus>().Unsubscribe<ComboChangedEvent>(ComboRaise);
    }

    private void ComboRaise(ComboChangedEvent eventData)
    {
        ComboMultiplier(eventData.CurrentCombo);
        ScoreLevel(eventData.TriggeredLevel);
    }

    private void ComboMultiplier(int combo)
    {
        if (combo >= 30)
        {
            _comboMultiplier = 4;
        }
        else if (combo >= 20)
        {
            _comboMultiplier = 3;
        }
        else if (combo >= 10)
        {
            _comboMultiplier = 2;
        }
        else
        {
            _comboMultiplier = 1;
        }
    }

    private void ScoreLevel(ComboLevel level)
    {
        int baseScore = 0;

        switch (level)
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
