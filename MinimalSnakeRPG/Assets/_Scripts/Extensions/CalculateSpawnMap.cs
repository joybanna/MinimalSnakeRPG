using UnityEngine;

public static class CalculateSpawnMap
{
    private const int MinEnemyPerWave = 2;
    private const int MaxEnemyPerWave = 6;

    private const int MinHeroPerWave = 0;
    private const int MaxHeroPerWave = 2;

    public static int GetContSpawnUnit(UnitType unitType, int wave)
    {
        return unitType switch
        {
            UnitType.Hero => FormulaHeroWave(wave),
            UnitType.Enemy => FormulaEnemyWave(wave),
            _ => 1
        };
    }

    private static int FormulaEnemyWave(int wave)
    {
        var main = wave + 1;
        var clamp = Mathf.Clamp(main, MinEnemyPerWave, MaxEnemyPerWave);
        return clamp;
    }

    private static int FormulaHeroWave(int wave)
    {
        var mod = wave % 5;
        return mod switch
        {
            0 => 1,
            3 => MaxHeroPerWave,
            _ => MinHeroPerWave
        };
    }

    public static bool GetEnemyMoveable()
    {
        return UnityEngine.Random.Range(0, 100) < 100;
    }
}