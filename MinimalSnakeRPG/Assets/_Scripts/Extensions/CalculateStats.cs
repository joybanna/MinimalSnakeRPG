using System;
using UnityEngine;

public static class CalculateStats
{
    public static int CalAtkOnLevel(this UnitStat stat, int level)
    {
        var defaultAtk = stat.attack - (level - 1);
        return defaultAtk + level;
    }

    public static int CalDefOnLevel(this UnitStat stat, int level)
    {
        var defaultDef = stat.defense - (level - 1);
        var round = Mathf.RoundToInt(level / 2f);
        return defaultDef + round;
    }

    public static int CalHpOnLevel(this UnitStat stat, int level)
    {
        var defaultHp = stat.hp - (level - 1);
        var round = Mathf.RoundToInt(level * 1.5f);
        return defaultHp + round;
    }

    public static int CalDamage(this UnitStat stat, UnitStat bonus)
    {
        return stat.attack + bonus.attack;
    }


    public static int CalDefend(this UnitStat stat, UnitStat bonus)
    {
        return stat.defense + bonus.defense;
    }

    public static int CalDamaged(this UnitClass defenderClass, int finalDefender, InfoDamage infoDamage)
    {
        var weakness = GetWeakness(defenderClass, infoDamage.attackerClass);
        var damage = infoDamage.finalDamage - finalDefender;
        damage = Mathf.Clamp(damage, 0, int.MaxValue);
        return damage * weakness;
    }


    public static int GetWeakness(this UnitClass def, UnitClass atk)
    {
        if (def == atk) return 1;
        switch (def)
        {
            case UnitClass.Warrior when atk == UnitClass.Wizard:
            case UnitClass.Rogue when atk == UnitClass.Warrior:
            case UnitClass.Wizard when atk == UnitClass.Rogue:
                return 2;
            default:
                return 1;
        }
    }

    private static readonly UnitClass[] UNIT_CLASSES = { UnitClass.Warrior, UnitClass.Rogue, UnitClass.Wizard };

    public static UnitClass RandomUnitClass()
    {
        var index = UnityEngine.Random.Range(0, UNIT_CLASSES.Length);
        return UNIT_CLASSES[index];
    }

    public static UnitStat GetUnitStats(this UnitClass uClass, int level)
    {
        var baseStats = LoadDataUnitClassStats.Instance.GetBaseStats(uClass);
        baseStats.SetStatCurrentLevel(level);
        return baseStats;
    }
}