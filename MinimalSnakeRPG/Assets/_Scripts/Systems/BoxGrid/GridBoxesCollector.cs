using System;
using System.Collections.Generic;
using UnityEngine;

public class GridBoxesCollector : MonoBehaviour
{
    public static GridBoxesCollector instance;

    [SerializeField] private List<Box> gridBoxes;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        _currentNeighbours = new Dictionary<UnitDirection, Box>();
    }

    public void AddGridBox(Box box)
    {
        gridBoxes ??= new List<Box>();
        gridBoxes.Add(box);
    }

    private Dictionary<UnitDirection, Box> GetNeighbours(Box box)
    {
        var neighbours = new Dictionary<UnitDirection, Box>();
        for (var index = gridBoxes.Count - 1; index >= 0; index--)
        {
            var gridBox = gridBoxes[index];
            if (gridBox == box)
            {
                continue;
            }

            var isNeighbour = gridBox.IsNeighbour(box, out var direction);
            if (!isNeighbour)
            {
                continue;
            }

            // CustomDebug.SetMessage("Neighbour : " + direction, Color.green);
            neighbours.TryAdd(direction, gridBox);
        }

        return neighbours;
    }

    private Dictionary<UnitDirection, Box> _currentNeighbours;

    public void ShowMoveableArea(Box box, UnitDirection headDir)
    {
        _currentNeighbours = GetNeighbours(box);
        // CustomDebug.SetMessage($"Neighbours : {_currentNeighbours.Count}", Color.yellow);
        if (_currentNeighbours == null) return;
        _currentNeighbours = RemoveOppositeDirection(headDir);
        foreach (var neighbour in _currentNeighbours)
        {
            var isCrossDirection = headDir.IsOppositeDirection(neighbour.Key);
            if (isCrossDirection) continue;
            neighbour.Value.ShowMoveableArea(true);
        }
    }

    private Dictionary<UnitDirection, Box> RemoveOppositeDirection(UnitDirection headDir)
    {
        var tempNeighbours = new Dictionary<UnitDirection, Box>(_currentNeighbours);
        foreach (var neighbour in _currentNeighbours)
        {
            var isCrossDirection = headDir.IsOppositeDirection(neighbour.Key);
            if (isCrossDirection)
            {
                tempNeighbours.Remove(neighbour.Key);
            }
        }

        return tempNeighbours;
    }

    public void HideMoveableArea()
    {
        if (_currentNeighbours == null) return;
        foreach (var neighbour in _currentNeighbours)
        {
            neighbour.Value.ShowMoveableArea(false);
        }
    }

    public Box GetBoxMoved(UnitDirection direction)
    {
        if (_currentNeighbours.TryGetValue(direction, out var box))
        {
            return box;
        }
        else
        {
            CustomDebug.SetMessage("No Box Found", Color.red);
            return null;
        }
    }

    public bool IsMoveAble(UnitDirection direction)
    {
        return _currentNeighbours.ContainsKey(direction);
    }
}