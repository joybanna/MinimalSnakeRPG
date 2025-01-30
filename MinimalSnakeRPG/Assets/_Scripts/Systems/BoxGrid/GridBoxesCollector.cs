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
    }

    public void AddGridBox(Box box)
    {
        gridBoxes ??= new List<Box>();
        gridBoxes.Add(box);
    }

    public Dictionary<UnitDirection, Box> GetNeighbours(Box box)
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

            neighbours.TryAdd(direction, gridBox);
        }

        return neighbours;
    }

    private Dictionary<UnitDirection, Box> _currentNeighbours;

    public void ShowMoveableArea(Box box, UnitDirection headDir)
    {
        _currentNeighbours = GetNeighbours(box);
        if (_currentNeighbours == null) return;
        foreach (var neighbour in _currentNeighbours)
        {
            var isCrossDirection = headDir.IsOppositeDirection(neighbour.Key);
            if (isCrossDirection) continue;
            neighbour.Value.ShowMoveableArea(true);
        }
    }

    public void HideMoveableArea()
    {
        if (_currentNeighbours == null) return;
        foreach (var neighbour in _currentNeighbours)
        {
            neighbour.Value.ShowMoveableArea(false);
        }
    }
}