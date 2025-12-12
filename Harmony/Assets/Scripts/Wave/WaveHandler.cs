using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WaveHandler
{
    public static float GetWaveY(WaveType type, float xPosition, float amplitude, float frequency, float speed, float timeNow)
    {
        float t = frequency * xPosition + speed * timeNow;

        switch (type)
        {
            case WaveType.Sin:
                return amplitude * Mathf.Sin(t);
            case WaveType.Square:
                return amplitude * Mathf.Sign(Mathf.Sin(t));
            case WaveType.Triangle:
                return amplitude * (Mathf.PingPong(t, Mathf.PI) / Mathf.PI * 2f - 1f);
            case WaveType.Sawtooth:
                return amplitude * (-1f + 2f * Mathf.Repeat(t / (2f * Mathf.PI), 1f));
            case WaveType.Pulse:
                float pulseTime = Mathf.Repeat(t / (2f * Mathf.PI) + 0.5f, 1f);
                if (pulseTime < 0.2f)
                {
                    return amplitude;
                }
                else
                {
                    return -amplitude;
                }
            default:
                return 0f;
        }
    }

    public static int MinAmplitudeStep => 0;
    public static int MaxAmplitudeStep => SingletonHub.Instance.Get<WaveManager>().WaveData.MaxAmplitudeStep;
    public static int MinFrequencyStep => 1;
    public static int MaxFrequencyStep => SingletonHub.Instance.Get<WaveManager>().WaveData.MaxFrequencyStep;

    public static int GetClampedAmplitudeStep(int amplitudeStep, int minStep, int maxStep)
    {
        return Mathf.Clamp(amplitudeStep, minStep, maxStep);
    }

    public static int GetClampedFrequencyStep(int frequencyStep, int minStep, int maxStep)
    {
        return Mathf.Clamp(frequencyStep, minStep, maxStep);
    }
}
