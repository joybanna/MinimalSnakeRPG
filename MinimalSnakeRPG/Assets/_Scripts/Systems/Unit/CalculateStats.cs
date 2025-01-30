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
}