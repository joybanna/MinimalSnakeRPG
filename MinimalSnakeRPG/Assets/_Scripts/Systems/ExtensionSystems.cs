public static class ExtensionSystems
{
    public static bool IsOppositeDirection(this UnitDirection dirMain, UnitDirection dirSub)
    {
        return (dirMain == UnitDirection.Up && dirSub == UnitDirection.Down) ||
               (dirMain == UnitDirection.Down && dirSub == UnitDirection.Up) ||
               (dirMain == UnitDirection.Left && dirSub == UnitDirection.Right) ||
               (dirMain == UnitDirection.Right && dirSub == UnitDirection.Left);
    }

    public static UnitDirection GetOppositeDirection(this UnitDirection dir)
    {
        return dir switch
        {
            UnitDirection.Up => UnitDirection.Down,
            UnitDirection.Down => UnitDirection.Up,
            UnitDirection.Left => UnitDirection.Right,
            UnitDirection.Right => UnitDirection.Left,
            _ => UnitDirection.Up
        };
    }
}