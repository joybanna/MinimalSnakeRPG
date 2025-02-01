using System;
using System.Collections.Generic;
using UnityEngine;

public enum BoxStatus
{
    Empty = 0,
    Hero = 1,
    Enemy = 2,
    Collectible = 3,
    Obstacle = 4,
    Blind = 5,
}

public class Box : MonoBehaviour, IEquatable<Box>
{
    [SerializeField] private Vector2 grid;

    public BoxStatus BoxStatus
    {
        get => showStatus;
        set
        {
            showStatus = value;
            boxGuide.SetColor(value);
        }
    }

    [SerializeField] private BoxGuide boxGuide;

    [SerializeField] private BoxStatus showStatus;
    public Vector2 Grid => grid;

    public void InitBox(int x, int y)
    {
        grid = new Vector2(x, y);
        BoxStatus = BoxStatus.Empty;
        this.name = $"Box({x},{y})";
    }

    public void ShowMoveableArea(bool isShow)
    {
        boxGuide.ShowMoveableArea(isShow);
    }

    public bool IsNeighbour(Box box, out UnitDirection direction)
    {
        var xDiff = Mathf.Abs(grid.x - box.grid.x);
        var yDiff = Mathf.Abs(grid.y - box.grid.y);
        direction = UnitDirection.Up;

        if (!Mathf.Approximately(xDiff + yDiff, 1))
        {
            return false;
        }
        else
        {
            if (grid.x < box.grid.x)
            {
                direction = UnitDirection.Left;
            }
            else if (grid.x > box.grid.x)
            {
                direction = UnitDirection.Right;
            }
            else if (grid.y < box.grid.y)
            {
                direction = UnitDirection.Down;
            }
            else if (grid.y > box.grid.y)
            {
                direction = UnitDirection.Up;
            }

            return true;
        }
    }

    public bool BoxIsMovable(UnitType unitType)
    {
        if (unitType == UnitType.Hero)
        {
            return BoxStatus switch
            {
                BoxStatus.Empty => true,
                BoxStatus.Collectible => true,
                BoxStatus.Enemy => true,
                BoxStatus.Obstacle => true,
                BoxStatus.Hero => true,
                _ => false
            };
        }
        else
        {
            return BoxStatus switch
            {
                BoxStatus.Empty => true,
                BoxStatus.Collectible => false,
                BoxStatus.Enemy => false,
                BoxStatus.Obstacle => false,
                BoxStatus.Hero => true,
                _ => false
            };
        }
    }

    public bool IsBorderBox()
    {
        return GridBoxesCollector.instance.IsBorderBox(this);
    }

    public bool Equals(Box other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return base.Equals(other) && grid.Equals(other.grid) && showStatus == other.showStatus;
    }

    public override bool Equals(object obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((Box)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), grid, (int)showStatus);
    }
}