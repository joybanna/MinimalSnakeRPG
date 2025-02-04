using System.Collections.Generic;
using UnityEngine;

public static class CalculateSpawnMap
{
   

   

    public static int GetContSpawnUnit(UnitType unitType, int wave)
    {
        return unitType switch
        {
            UnitType.Hero => FormulaHeroWave(wave),
            UnitType.Enemy => FormulaEnemyWave(wave),
            _ => 1
        };
    }
    
    private const int MinEnemyPerWave = 2;
    private const int MaxEnemyPerWave = 6;
    private static int FormulaEnemyWave(int wave)
    {
        var main = wave + 1;
        var clamp = Mathf.Clamp(main, MinEnemyPerWave, MaxEnemyPerWave);
        return clamp;
    }

    private const int MinHeroPerWave = 0;
    private const int MaxHeroPerWave = 2;
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

    public static int GetCollectibleSpawn(int wave)
    {
        var mod = wave % 5;
        return mod switch
        {
            0 => 1,
            3 => 2,
            _ => 1
        };
    }

    public static List<ObstacleType> GetObstacleSpawn(ObstacleType[] spawnable, int wave)
    {
        var list = new List<ObstacleType>();
        list.AddRange(spawnable);
        list.AddRange(spawnable);
        list.AddRange(spawnable);
        list.Shuffle();

        var sumCount = list.Count;
        var cutCout = sumCount - UnityEngine.Random.Range(2, 5);
        list.RemoveRange(0, cutCout);
        return list;
    }
}