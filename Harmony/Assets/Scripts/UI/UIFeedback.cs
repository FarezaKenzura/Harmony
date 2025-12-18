using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIFeedback : UIBase
{
    [SerializeField] private GameObject _feedbackPrefab;
    [SerializeField] private Transform _feedbackPoints;
    [SerializeField] private Transform[] _activatorPositions;
    [SerializeField] private float _displayDuration = 0.5f;
    [SerializeField] private float _yOffset = 1.5f;

    protected override void OnInitialize()
    {
        SingletonHub.Instance.Get<EventBus>().Subscribe<ComboChangedEvent>(SpawnFeedback);
    }

    protected override void OnUnitialize()
    {
        SingletonHub.Instance.Get<EventBus>().Unsubscribe<ComboChangedEvent>(SpawnFeedback);
    }

    private void SpawnFeedback(ComboChangedEvent eventData)
    {
        string text = "";
        Color color = Color.white;

        switch (eventData.Level)
        {
            case ComboLevel.Perfect: text = "PERFECT"; color = Color.yellow; break;
            case ComboLevel.Good: text = "GOOD"; color = Color.green; break;
            case ComboLevel.Miss: text = "MISS"; color = Color.red; break;
            default: return;
        }

        if (eventData.Lane < 0 || eventData.Lane >= _activatorPositions.Length) return;

        Vector3 spawnPos = _activatorPositions[eventData.Lane].position;
        spawnPos.y += _yOffset;

        GameObject obj = SingletonHub.Instance.Get<ObjectPool>().GetPooledObject(_feedbackPrefab, spawnPos, Quaternion.identity, _feedbackPoints);

        UIFeedbackItem item = obj.GetComponent<UIFeedbackItem>();
        item.Setup(text, color, _displayDuration);
    }
}
