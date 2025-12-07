using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WaveHandler
{
    public enum WaveType
    {
        Sin,
        Square,
        PingPong
    }

    public static float GetWaveY(WaveType type, float xPosition, float amplitude, float frequency, float speed, float timeNow)
    {
        float t = frequency * xPosition + speed * timeNow;

        switch (type)
        {
            case WaveType.Sin: return amplitude * Mathf.Sin(t);
            case WaveType.Square: return amplitude * Mathf.Sign(Mathf.Sin(t));
            case WaveType.PingPong: return amplitude * (Mathf.PingPong(t, 1f) * 2f - 1f);
            default: return 0f;
        }
    }

    public static int GetClampedAmplitudeStep(int amplitudeStep, int minStep, int maxStep)
    {
        return Mathf.Clamp(amplitudeStep, minStep, maxStep);
    }

    public static int GetClampedFrequencyStep(int frequencyStep, int minStep, int maxStep)
    {
        return Mathf.Clamp(frequencyStep, minStep, maxStep);
    }
}
