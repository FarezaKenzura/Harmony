using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveData", menuName = "Data/Wave Data")]
public class SOWave : ScriptableObject
{
    public float MaxAmplitude;
    public int MaxAmplitudeStep;
    public int MinAmplitudeStep = 1;

    public float MaxFrequency;
    public int MaxFrequencyStep;
    public int MinFrequencyStep = 1;
}
