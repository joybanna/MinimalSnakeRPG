public struct InfoInitUnit
{
    public UnitType unitType;
    public UnitDirection direction;
    public Box box;
    public int level;

    public InfoInitUnit(UnitType uType, UnitDirection dir, Box box, int level = 1)
    {
        unitType = uType;
        direction = dir;
        this.box = box;
        this.level = level;
    }
}