using UnityEngine;

public static class CalculateLevelProgression
{
    public static int NextLevelExp(this UnitLevelProgression unitLevel)
    {
        var nextLevel = unitLevel.CurrentLevel + 1;
        var mult = nextLevel * 10;
        return mult;
    }

    public static int CalculateExp(this UnitLevelProgression unitLevel)
    {
        var mult = unitLevel.CurrentLevel * 10;
        return mult;
    }

    public static int CalculateScore(this UnitLevelProgression unitLevel)
    {
        var score = unitLevel.CurrentLevel * 10;
        return score;
    }
}