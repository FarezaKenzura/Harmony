using System;

[Serializable]
public struct WaveConfig : IEquatable<WaveConfig>
{
    public WaveType WaveType;
    public int AmplitudeStep;
    public int FrequencyStep;

    public static WaveConfig Invalid => new WaveConfig(WaveType.Sin, -1, -1);
    public static WaveConfig Min => new WaveConfig(WaveType.Sin, WaveHandler.MinAmplitudeStep, WaveHandler.MinFrequencyStep);

    public WaveConfig(WaveType waveType, int amplitudeStep, int frequencyStep)
    {
        WaveType = waveType;
        AmplitudeStep = amplitudeStep;
        FrequencyStep = frequencyStep;
    }

    public bool Equals(WaveConfig other)
    {
        return WaveType == other.WaveType
            && AmplitudeStep == other.AmplitudeStep
            && FrequencyStep == other.FrequencyStep;
    }

    public override bool Equals(object obj)
    {
        return obj is WaveConfig other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(WaveType, AmplitudeStep, FrequencyStep);
    }

    public static bool operator ==(WaveConfig left, WaveConfig right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(WaveConfig left, WaveConfig right)
    {
        return !left.Equals(right);
    }
}
