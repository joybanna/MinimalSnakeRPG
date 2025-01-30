public static class ExtensionSystems
{
    public static bool IsOppositeDirection(this UnitDirection dirMain, UnitDirection dirSub)
    {
        return (dirMain == UnitDirection.Up && dirSub == UnitDirection.Down) ||
               (dirMain == UnitDirection.Down && dirSub == UnitDirection.Up) ||
               (dirMain == UnitDirection.Left && dirSub == UnitDirection.Right) ||
               (dirMain == UnitDirection.Right && dirSub == UnitDirection.Left);
    }
}