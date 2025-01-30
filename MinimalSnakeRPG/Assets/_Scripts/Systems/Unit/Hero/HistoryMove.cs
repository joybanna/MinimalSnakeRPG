using System.Collections.Generic;
using UnityEngine;

public class HistoryMove : MonoBehaviour
{
    [SerializeField] private List<Box> historyBoxes;
    [SerializeField] private List<UnitDirection> historyDirections;

    private void Awake()
    {
        historyBoxes = new List<Box>();
        historyDirections = new List<UnitDirection>();
    }

    public void InitHistory(Box box, UnitDirection direction)
    {
        historyBoxes.Add(box);
        historyDirections.Add(direction);
    }


    public void AddHistory(UnitDirection direction)
    {
        // historyBoxes.Add(box);
        historyDirections.Add(direction);
    }

    public Box GetCurrentBox()
    {
        if (historyBoxes.Count == 0 || historyBoxes == null) return null;
        return historyBoxes[^1];
    }
}