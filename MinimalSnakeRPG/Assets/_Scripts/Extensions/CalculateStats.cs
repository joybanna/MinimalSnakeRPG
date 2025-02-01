using System;
using UnityEngine;

public static class CalculateStats
{
    public static int CalAtkOnLevel(this UnitStat stat, int level)
    {
        return stat.attack + level;
    }

    public static int CalDefOnLevel(this UnitStat stat, int level)
    {
        return stat.defense + level;
    }

    public static int CalHpOnLevel(this UnitStat stat, int level)
    {
        return stat.hp + level;
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
            case UnitClass.Warrior when atk == UnitClass.Rogue:
            case UnitClass.Rogue when atk == UnitClass.Wizard:
            case UnitClass.Wizard when atk == UnitClass.Warrior:
                return 2;
            default:
                return 1;
        }
    }

    private static UnitClass[] _unitClasses = { UnitClass.Warrior, UnitClass.Rogue, UnitClass.Wizard };

    public static UnitClass RandomUnitClass()
    {
        var index = UnityEngine.Random.Range(0, _unitClasses.Length);
        return _unitClasses[index];
    }
}