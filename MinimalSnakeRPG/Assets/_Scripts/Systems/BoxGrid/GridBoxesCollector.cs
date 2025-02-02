using System;
using System.Collections.Generic;
using UnityEngine;

public class GridBoxesCollector : MonoBehaviour
{
    public static GridBoxesCollector instance;

    [SerializeField] private List<Box> gridBoxes;
    public List<Box> GridBoxes => gridBoxes;

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

    public void RemoveGridBoxes()
    {
        gridBoxes ??= new List<Box>();
        gridBoxes.Clear();
    }


    public Dictionary<UnitDirection, Box> GetNeighbours(Box box) =>
        gridBoxes.GetNeighbours(box);

    public Dictionary<UnitDirection, Box> GetNeighboursWithMoveable(Box box,
        UnitType unitType, UnitDirection dir) => gridBoxes.GetNeighboursWithMoveable(box, unitType, dir);

    private Dictionary<UnitDirection, Box> _currentNeighbours;

    public void ShowMoveableArea(Box box, UnitDirection headDir)
    {
        _currentNeighbours = gridBoxes.GetNeighboursWithMoveable(box, UnitType.Hero, headDir);
        // CustomDebug.SetMessage($"Neighbours : {_currentNeighbours.Count}", Color.yellow);
        if (_currentNeighbours == null) return;
        // _currentNeighbours = _currentNeighbours.RemoveOppositeDirection(headDir);
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

    [SerializeField] private Vector2 horizontalGridCap;
    [SerializeField] private Vector2 verticalGridCap;

    public void SetGridCap()
    {
        var hMin = 999f;
        var hMax = -1f;

        var vMin = 999f;
        var vMax = -1f;

        foreach (var box in gridBoxes)
        {
            if (box.Grid.x < hMin)
            {
                hMin = box.Grid.x;
            }

            if (box.Grid.x > hMax)
            {
                hMax = box.Grid.x;
            }

            if (box.Grid.y < vMin)
            {
                vMin = box.Grid.y;
            }

            if (box.Grid.y > vMax)
            {
                vMax = box.Grid.y;
            }
        }

        horizontalGridCap = new Vector2(hMin, hMax);
        verticalGridCap = new Vector2(vMin, vMax);
    }

    public bool IsBorderBox(Box box)
    {
        return Mathf.Approximately(box.Grid.x, horizontalGridCap.x) ||
               Mathf.Approximately(box.Grid.x, horizontalGridCap.y) ||
               Mathf.Approximately(box.Grid.y, verticalGridCap.x) ||
               Mathf.Approximately(box.Grid.y, verticalGridCap.y);
    }
}

#if UNITY_EDITOR

[UnityEditor.CustomEditor(typeof(GridBoxesCollector), true)]
public class GridBoxesCollectorEditor : UnityEditor.Editor
{
    private GridBoxesCollector _gridBoxesCollector;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        _gridBoxesCollector = (GridBoxesCollector)target;

        if (GUILayout.Button("ResetColorGrids"))
        {
            foreach (var box in _gridBoxesCollector.GridBoxes)
            {
                box.BoxStatus = BoxStatus.Empty;
            }
        }

        if (GUILayout.Button("SetGridCap"))
        {
            _gridBoxesCollector.SetGridCap();
        }
    }
}

#endif