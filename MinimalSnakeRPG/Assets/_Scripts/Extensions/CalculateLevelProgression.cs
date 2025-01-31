public static class CalculateLevelProgression
{
    public static int NextLevelExp(this UnitLevelProgression unitLevel)
    {
        return unitLevel.CurrentLevel + 1;
    }

    public static int CalculateExp(this UnitLevelProgression unitLevel)
    {
        return unitLevel.CurrentLevel;
    }
}