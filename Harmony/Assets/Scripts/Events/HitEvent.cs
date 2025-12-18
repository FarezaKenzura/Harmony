public struct ScoreChangedEvent
{
    public long TotalScore;
}

public struct ComboChangedEvent
{
    public int Lane;
    public int CurrentCombo;
    public ComboLevel Level;
}