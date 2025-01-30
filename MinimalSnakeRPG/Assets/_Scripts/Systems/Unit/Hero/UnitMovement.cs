using System;
using UnityEngine;

public enum UnitDirection
{
    Up = 0,
    Down = 1,
    Left = 2,
    Right = 3,
}

public enum UnitType
{
    Hero = 0,
    Enemy = 1,
}

public class UnitMovement : MonoBehaviour
{
    [SerializeField] private UnitType unitType = UnitType.Hero;
    [SerializeField] private float gridPadding = 0.1f;
    [SerializeField] private float gridSize = 1.0f;
    [SerializeField] private UnitDirection direction = UnitDirection.Up;
    private UnitDirection _previousDirection = UnitDirection.Up;

    [SerializeField] private bool isStandalone = false;

    public UnitDirection CurrentDirection => direction;

    private void Start()
    {
        if (isStandalone) Init(UnitType.Hero, unitType == UnitType.Hero ? UnitDirection.Up : UnitDirection.Down);
    }

    public void Init(UnitType uType, UnitDirection dir)
    {
        unitType = uType;
        direction = dir;
        _previousDirection = dir;
        SetRotation();
    }

    public void Move(UnitDirection dir)
    {
        _previousDirection = direction;
        direction = dir;

        Vector3 pos = transform.position;
        switch (direction)
        {
            case UnitDirection.Up:
                pos.y += gridPadding + gridSize;
                break;
            case UnitDirection.Down:
                pos.y -= gridPadding + gridSize;
                break;
            case UnitDirection.Left:
                pos.x -= gridPadding + gridSize;
                break;
            case UnitDirection.Right:
                pos.x += gridPadding + gridSize;
                break;
        }

        this.transform.position = pos;
        SetRotation();
    }

    public bool IsOppositeDirection(UnitDirection dir)
    {
        return direction.IsOppositeDirection(dir);
    }

    private void SetRotation()
    {
        Debug.Log($"Direction : {direction} - Previous : {_previousDirection}");
        if (direction == _previousDirection) return;
        var zEulerAngle = GetZEulerAngle(direction);
        Debug.Log($"Z-Euler : {zEulerAngle}");
        this.transform.rotation = Quaternion.Euler(0, 0, zEulerAngle);
    }

    private float GetZEulerAngle(UnitDirection dir)
    {
        return dir switch
        {
            UnitDirection.Up => 0,
            UnitDirection.Down => 180,
            UnitDirection.Left => 90,
            UnitDirection.Right => -90,
            _ => 0,
        };
    }
}