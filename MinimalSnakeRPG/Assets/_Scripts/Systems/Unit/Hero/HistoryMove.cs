using System;
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


    public void AddHistory(UnitDirection direction, Box box)
    {
        historyBoxes.Add(box);
        historyDirections.Add(direction);
    }

    public Box GetCurrentBox()
    {
        if (historyBoxes.Count == 0 || historyBoxes == null)
        {
            CustomDebug.SetMessage("History Box is null", Color.red);
            return null;
        }

        return historyBoxes[^1];
    }

    public Box GetLastHeroPos(int currentIndex)
    {
        if (historyBoxes.Count == 0 || historyBoxes == null)
        {
            CustomDebug.SetMessage("History Box is null", Color.red);
            return null;
        }

        var index = historyBoxes.Count - currentIndex;
        return historyBoxes[index];
    }

    public Box GetHeroPos(int indexHero)
    {
        if (historyBoxes.Count == 0 || historyBoxes == null)
        {
            CustomDebug.SetMessage("History Box is null", Color.red);
            return null;
        }

        var index = historyBoxes.Count - (indexHero);
        return historyBoxes[index];
    }

    private void OnDrawGizmos()
    {
        if (historyBoxes == null || historyBoxes.Count <= 3) return;
        var cap = historyBoxes.Count - 3;
        for (var index = historyBoxes.Count - 1; index >= cap; index--)
        {
            var box = historyBoxes[index];
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(box.transform.position, Vector3.one * 0.5f);
        }
    }
}