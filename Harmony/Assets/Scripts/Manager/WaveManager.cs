using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [field: SerializeField] public SOWave WaveData { get; private set; }

    private void Awake()
    {
        SingletonHub.Instance.Register(this);
    }
}
