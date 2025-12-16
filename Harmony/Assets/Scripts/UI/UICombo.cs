using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UICombo : UIBase
{
    [SerializeField] private TMP_Text _comboText;

    protected override void OnInitialize()
    {
        SingletonHub.Instance.Get<EventBus>().Subscribe<ComboChangedEvent>(ComboStatus);
    }

    protected override void OnUnitialize()
    {
        SingletonHub.Instance.Get<EventBus>().Unsubscribe<ComboChangedEvent>(ComboStatus);
    }

    private void ComboStatus(ComboChangedEvent eventData)
    {
        int combo = eventData.CurrentCombo;

        if (combo > 1)
        {
            _comboText.text = $"COMBO\nx{combo}";
            Show();
        }
        else
        {
            Hide();
        }
    }
}
