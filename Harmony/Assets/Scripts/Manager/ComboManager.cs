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
    public event System.Action<int> OnComboChanged;

    private int _combo = 0;

    private void Awake()
    {
        SingletonHub.Instance.Register(this);
    }

    public int Combo => _combo;

    public void AddCombo()
    {
        _combo++;
        OnComboChanged?.Invoke(_combo);
    }

    public void ResetCombo()
    {
        _combo = 0;
        OnComboChanged?.Invoke(_combo);
    }

    public void RegisterHit(ComboLevel level, GameObject note)
    {
        switch (level)
        {
            case ComboLevel.Perfect:
                SingletonHub.Instance.Get<ScoreManager>().AddScore(100);
                AddCombo();
                break;
            case ComboLevel.Good:
                SingletonHub.Instance.Get<ScoreManager>().AddScore(50);
                AddCombo();
                break;
            case ComboLevel.Miss:
                ResetCombo();
                break;
        }
        Debug.Log($"Hit: {level}, Current Combo: {_combo}");
        SingletonHub.Instance.Get<ObjectPool>().ReturnToPool(note);
    }
}
