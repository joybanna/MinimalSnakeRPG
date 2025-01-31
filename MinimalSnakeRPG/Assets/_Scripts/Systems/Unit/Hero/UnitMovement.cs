using System;
using UnityEngine;

public enum UnitDirection
{
    None = -1,
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
    private UnitType _unitType;
    [SerializeField] private float gridPadding = 0.1f;
    [SerializeField] private float gridSize = 1.0f;
    [SerializeField] private UnitDirection direction = UnitDirection.Up;
    private UnitDirection _previousDirection = UnitDirection.Up;

    public UnitDirection CurrentDirection => direction;

    [SerializeField] private Box _currentBox;
    [SerializeField] private Box _previousBox;

    public Box CurrentBox => _currentBox;

    public void Init(UnitType uType, UnitDirection dir, Box box)
    {
        _unitType = uType;
        direction = dir;
        _previousDirection = dir;
        this.transform.position = box.transform.position;
        SetRotation();
        SetBoxStatus(box);
    }

    public void Move(UnitDirection dir, Box box)
    {
        _previousDirection = direction;
        direction = dir;
        this.transform.position = box.transform.position;
        SetRotation();
        SetBoxStatus(box);
    }

    public void SetBoxStatus(Box currentBox)
    {
        if (_currentBox != null)
        {
            _currentBox.BoxStatus = BoxStatus.Empty;
        }

        _currentBox = currentBox;
        _currentBox.BoxStatus = _unitType == UnitType.Hero ? BoxStatus.Hero : BoxStatus.Enemy;
    }

    private void SetRotation()
    {
        var zEulerAngle = GetZEulerAngle(direction);
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