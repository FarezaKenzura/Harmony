using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIFeedback : UIBase
{
    [SerializeField] private TMP_Text _feedbackText;
    [SerializeField] private float _displayDuration = 0.5f;

    private Coroutine _hideCoroutine;

    protected override void OnInitialize()
    {
        SingletonHub.Instance.Get<EventBus>().Subscribe<ComboChangedEvent>(FeedbackStatus);
    }

    protected override void OnUnitialize()
    {
        SingletonHub.Instance.Get<EventBus>().Unsubscribe<ComboChangedEvent>(FeedbackStatus);
    }

    private void FeedbackStatus(ComboChangedEvent eventData)
    {
        string text = "";
        Color color = Color.white;

        switch (eventData.Level)
        {
            case ComboLevel.Perfect:
                text = "PERFECT";
                color = Color.yellow;
                break;
            case ComboLevel.Good:
                text = "GOOD";
                color = Color.green;
                break;
            case ComboLevel.Miss:
                text = "MISS";
                color = Color.red;
                break;
            default:
                return;
        }

        _feedbackText.text = text;
        _feedbackText.color = color;

        Show();

        if (_hideCoroutine != null)
        {
            StopCoroutine(_hideCoroutine);
        }
        _hideCoroutine = StartCoroutine(AutoHideAfterDelay(_displayDuration));
    }

    private IEnumerator AutoHideAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        Hide();
        _hideCoroutine = null;
    }
}
