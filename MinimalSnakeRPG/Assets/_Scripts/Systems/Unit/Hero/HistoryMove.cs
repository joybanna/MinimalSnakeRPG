using System;
using System.Collections.Generic;
using UnityEngine;

public class HistoryMove : MonoBehaviour
{
    public static HistoryMove instance;
    [SerializeField] private List<Box> historyBoxes;
    [SerializeField] private List<UnitDirection> historyDirections;

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

    public void InitHistory(Box box, UnitDirection direction)
    {
        historyBoxes = new List<Box>();
        historyDirections = new List<UnitDirection>();
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
        if (historyBoxes.IsEmptyCollection())
        {
            CustomDebug.SetMessage("History Box is null", Color.red);
            return null;
        }

        return historyBoxes[^1];
    }

    public Box GetLastHeroPos(int currentIndex)
    {
        if (historyBoxes.IsEmptyCollection() || currentIndex > historyBoxes.Count)
        {
            // CustomDebug.SetMessage("History Box is null", Color.red);
            return null;
        }

        var index = historyBoxes.Count - currentIndex;
        return historyBoxes[index];
    }

    public UnitDirection GetLastHeroDir(int currentIndex)
    {
        if (historyDirections.IsEmptyCollection() || currentIndex > historyDirections.Count)
        {
            return UnitDirection.None;
        }

        var index = historyDirections.Count - currentIndex;
        return historyDirections[index];
    }
}