public enum UnitClass
{
    None = 0,
    Warrior = 1,
    Rogue = 2,
    Wizard = 3,
}

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

    public void SetStatCurrentLevel(int level)
    {
        hp = this.CalHpOnLevel(level);
        attack = this.CalAtkOnLevel(level);
        defense = this.CalDefOnLevel(level);
    }
}