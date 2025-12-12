using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WaveType
{
    Sin,
    Square,
    Triangle,
    Sawtooth,
    Pulse
}

public class WaveManager : MonoBehaviour
{
    [field: SerializeField] public SOWave WaveData { get; private set; }

    private void Awake()
    {
        SingletonHub.Instance.Register(this);
    }
}
