using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UICombo : UIBase
{
    [SerializeField] private TMP_Text _comboText;

    protected override void OnInitialize()
    {

    }

    private void ComboStatus(ComboChangedEvent eventData)
    {
        int combo = eventData.CurrentCombo;

        if (combo > 1)
        {
            _comboText.text = $"COMBO\nx{combo}";
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
