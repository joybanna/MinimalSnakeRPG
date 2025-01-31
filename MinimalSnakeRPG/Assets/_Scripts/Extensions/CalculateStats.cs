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

    public static int CalDamage(this UnitStat stat, int bonus)
    {
        return stat.attack + bonus;
    }

    public static int CalHeal(this UnitStat stat, int bonus)
    {
        return stat.hp + bonus;
    }

    public static int CalDefend(this UnitStat stat, int bonus)
    {
        return stat.defense + bonus;
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
        return 1;
        switch (def)
        {
            case UnitClass.Warrior:
                switch (atk)
                {
                    case UnitClass.Warrior:
                        return 1;
                    case UnitClass.Rogue:
                        return 2;
                    case UnitClass.Wizard:
                        return 1;
                }

                break;
            case UnitClass.Rogue when atk == UnitClass.Warrior:
                return 1;
            case UnitClass.Rogue when atk == UnitClass.Rogue:
                return 1;
            case UnitClass.Rogue when atk == UnitClass.Wizard:
            case UnitClass.Wizard when atk == UnitClass.Warrior:
                return 2;
            case UnitClass.Wizard when atk == UnitClass.Rogue:
                return 3;
            case UnitClass.Wizard when atk == UnitClass.Wizard:
                return 1;
        }

        return 1;
    }

  
}