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
    private int _score;

    private void Awake()
    {
        SingletonHub.Instance.Register(this);
    }

    public int Score => _score;

    public void AddScore(int amount)
    {
        _score += amount;
        OnScoreChanged?.Invoke(_score);
    }

    public void DecreaseScore(int amount)
    {
        _score = Mathf.Max(0, _score - amount);
        OnScoreChanged?.Invoke(_score);
    }
}
