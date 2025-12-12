public struct ScoreChangedEvent
{
    public int NewScore;
    public int ScoreDelta;
}

public struct ComboChangedEvent
{
    public int CurrentCombo;
    public ComboLevel Level;
}