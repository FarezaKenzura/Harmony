using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ComboLevel
{
    Perfect,
    Good,
    Miss
}

public class ScoreManager : MonoBehaviour
{
    public event System.Action<int> OnScoreChanged;
    public event System.Action<int> OnComboChanged;
    private int _score;
    private int _combo;
    private ComboLevel _comboLevel;

    private void Awake()
    {
        SingletonHub.Instance.Register(this);
    }

    public int Score => _score;
    public int Combo => _combo;

    public void AddScore(int amount)
    {
        _score += amount;
        _combo++;
        OnScoreChanged?.Invoke(_score);
        OnComboChanged?.Invoke(_combo);
    }

    public void Miss()
    {
        _combo = 0;
        OnComboChanged?.Invoke(_combo);
    }

    public void DecreaseScore(int amount)
    {
        _score = Mathf.Max(0, _score - amount);
        OnScoreChanged?.Invoke(_score);
    }

    public void RegisterHit(ComboLevel level, GameObject note)
    {
        switch (level)
        {
            case ComboLevel.Perfect:
                AddScore(100);
                break;
            case ComboLevel.Good:
                AddScore(50);
                break;
            case ComboLevel.Miss:
                Miss();
                break;
        }
        Destroy(note);
    }
}
