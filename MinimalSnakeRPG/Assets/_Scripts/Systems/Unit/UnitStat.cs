using System;

public enum UnitClass
{
    None = 0,
    Warrior = 1,
    Rogue = 2,
    Wizard = 3,
}

[Serializable]
public struct UnitStat
{
    public int hp;
    public int attack;
    public int defense;

    public UnitStat(int hp, int attack, int defense)
    {
        this.hp = hp;
        this.attack = attack;
        this.defense = defense;
    }

    public UnitStat(UnitStat stats)
    {
        this.hp = stats.hp;
        this.attack = stats.attack;
        this.defense = stats.defense;
    }

    public void SetStatCurrentLevel(UnitStat baseStat, int level)
    {
        hp = baseStat.CalHpOnLevel(level);
        attack = baseStat.CalAtkOnLevel(level);
        defense = baseStat.CalDefOnLevel(level);
    }

    public int GetStatValue(StatType statType)
    {
        switch (statType)
        {
            case StatType.Attack:
                return attack;
            case StatType.Defense:
                return defense;
            case StatType.Health:
                return hp;
            default:
                return 0;
        }
    }
}