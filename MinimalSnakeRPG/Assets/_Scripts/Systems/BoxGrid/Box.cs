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

public class Box : MonoBehaviour
{
    [SerializeField] private Vector2 grid;
    public BoxStatus BoxStatus { get; set; }

    [SerializeField] private BoxGuide boxGuide;

    public Vector2 Grid => grid;

    public void InitBox(int x, int y)
    {
        grid = new Vector2(x, y);
        BoxStatus = BoxStatus.Empty;
        this.name = $"Box({x},{y})";
    }

    public void SetBoxStatus(BoxStatus status)
    {
        BoxStatus = status;
        CustomDebug.SetMessage($"Box Status : {BoxStatus} , {grid}", Color.yellow);
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
                direction = UnitDirection.Right;
            }
            else if (grid.x > box.grid.x)
            {
                direction = UnitDirection.Left;
            }
            else if (grid.y < box.grid.y)
            {
                direction = UnitDirection.Up;
            }
            else if (grid.y > box.grid.y)
            {
                direction = UnitDirection.Down;
            }
            return true; 
        }
        
        
    }
}