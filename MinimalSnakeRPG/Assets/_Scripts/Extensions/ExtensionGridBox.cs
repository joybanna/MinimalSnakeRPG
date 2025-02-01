using System.Collections.Generic;
using UnityEngine;

public static class ExtensionGridBox
{
    public static Dictionary<UnitDirection, Box> GetNeighbours(this List<Box> list, Box box)
    {
        var neighbours = new Dictionary<UnitDirection, Box>();
        for (var index = list.Count - 1; index >= 0; index--)
        {
            var gridBox = list[index];
            if (gridBox == box)
            {
                continue;
            }

            var isNeighbour = gridBox.IsNeighbour(box, out var direction);
            if (!isNeighbour)
            {
                continue;
            }

            neighbours.TryAdd(direction, gridBox);
        }

        return neighbours;
    }

    public static Dictionary<UnitDirection, Box> GetNeighboursWithMoveable(this List<Box> list, Box box,
        UnitType unitType, UnitDirection dir)
    {
        var neighbours = new Dictionary<UnitDirection, Box>();
        for (var index = list.Count - 1; index >= 0; index--)
        {
            var gridBox = list[index];
            if (gridBox == box)
            {
                continue;
            }

            var isNeighbour = gridBox.IsNeighbour(box, out var direction);
            if (!isNeighbour)
            {
                continue;
            }

            if (!gridBox.BoxIsMovable(unitType)) continue;
            if (dir.IsOppositeDirection(direction)) continue;
            neighbours.TryAdd(direction, gridBox);
        }

        return neighbours;
    }

    public static Dictionary<UnitDirection, Box> RemoveOppositeDirection(this Dictionary<UnitDirection, Box> list,
        UnitDirection headDir)
    {
        var tempNeighbours = new Dictionary<UnitDirection, Box>(list);
        foreach (var neighbour in list)
        {
            var isCrossDirection = headDir.IsOppositeDirection(neighbour.Key);
            if (isCrossDirection)
            {
                tempNeighbours.Remove(neighbour.Key);
            }
        }

        return tempNeighbours;
    }

    public static bool GetNearestBox(this Dictionary<UnitDirection, Box> neighbourBox, Box toBox,
        out Box nearestBox, out UnitDirection direction)
    {
        nearestBox = null;
        direction = UnitDirection.None;
        var minDistance = float.MaxValue;
        foreach (var neighbour in neighbourBox)
        {
            var distance = Vector3.Distance(neighbour.Value.transform.position, toBox.transform.position);
            if (distance >= minDistance) continue;
            minDistance = distance;
            nearestBox = neighbour.Value;
            direction = neighbour.Key;
        }

        return nearestBox != null;
    }

    public static bool GetUnitOnMap(this List<UnitMain> units, Box box, out UnitMain unit)
    {
        unit = null;
        if (units.IsEmptyCollection()) return false;
        for (var index = units.Count - 1; index >= 0; index--)
        {
            var u = units[index];
            if (u == unit) continue;
            if (!u.CurrentBox.Equals(box)) continue;
            unit = u;
            return true;
        }

        return false;
    }
}