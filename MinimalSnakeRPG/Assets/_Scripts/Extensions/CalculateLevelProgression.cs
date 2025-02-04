using UnityEngine;

public static class CalculateLevelProgression
{
    public static int NextLevelExp(this UnitLevelProgression unitLevel)
    {
        var nextLevel = unitLevel.CurrentLevel + 1;
        var rInt =Mathf.RoundToInt(nextLevel * 1.5f);
        return rInt;
    }

    public static int CalculateExp(this UnitLevelProgression unitLevel)
    {
        return 1;
    }
}