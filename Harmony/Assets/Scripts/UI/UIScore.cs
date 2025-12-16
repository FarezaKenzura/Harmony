using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIScore : UIBase
{
    [SerializeField] private TMP_Text _scoreText;

    protected override void OnInitialize()
    {
        SingletonHub.Instance.Get<EventBus>().Subscribe<ScoreChangedEvent>(ScoreStatus);
    }

    protected override void OnUnitialize()
    {
        SingletonHub.Instance.Get<EventBus>().Unsubscribe<ScoreChangedEvent>(ScoreStatus);
    }

    private void ScoreStatus(ScoreChangedEvent eventData)
    {
        _scoreText.text = eventData.TotalScore.ToString("N0");
    }
}
