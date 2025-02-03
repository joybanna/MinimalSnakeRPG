using System.Collections.Generic;
using System.Collections.ObjectModel;

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

    public static UnitDirection GetRandomDirection()
    {
        return (UnitDirection)UnityEngine.Random.Range(0, 4);
    }

    public static bool IsEmptyCollection<T>(this ICollection<T> list) => list == null || list.Count == 0;

    public static bool IsAttackUnitThisBox(this Box box, UnitType unitType)
    {
        if (unitType == UnitType.Hero)
        {
            if (box.BoxStatus == BoxStatus.Enemy)
            {
                return true;
            }
        }
        else
        {
            if (box.BoxStatus == BoxStatus.Hero)
            {
                return true;
            }
        }

        return false;
    }

    public static UnitType OppositeUnitType(this UnitType unitType)
    {
        return unitType == UnitType.Hero ? UnitType.Enemy : UnitType.Hero;
    }
    
    public static bool IsCollectibleThisBox(this Box box, UnitType unitType)
    {
        if (unitType == UnitType.Hero)
        {
            if (box.BoxStatus == BoxStatus.Collectible)
            {
                return true;
            }
        }
        return false;
    }
}