using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Serialization;

public class HeroHeadGroup : MonoBehaviour
{
    [SerializeField] private List<UnitMain> heroMovements;
    [SerializeField] private UnitMain head;
    [SerializeField] private HistoryMove historyMove;
    [SerializeField] private HeroControl heroControl;

    private void Awake()
    {
        heroMovements = new List<UnitMain>();
        heroMovements.Add(head);
    }

    public void InitHero(Box box, UnitDirection direction)
    {
        head.UnitMovement.Init(UnitType.Hero, direction);
        historyMove.InitHistory(box, direction);
    }

    public void Move(UnitDirection dir)
    {
        head.UnitMovement.Move(dir);
        historyMove.AddHistory(dir);
    }

    public bool IsMoveAble(UnitDirection dir)
    {
        return !head.UnitMovement.IsOppositeDirection(dir);
    }

    public void RecruitHero(UnitMain unitMain)
    {
        heroMovements.Add(unitMain);
    }

    public void SwapHead(UnitMovement other)
    {
        // var tempPos = head.transform.position;
        // head.transform.position = other.transform.position;
        // other.transform.position = tempPos;
    }

    public void OnPlayerTurnStart()
    {
        var currentBox = historyMove.GetCurrentBox();
        // open moveable area
        GridBoxesCollector.instance.ShowMoveableArea(currentBox, head.UnitMovement.CurrentDirection);
        // open input control
        heroControl.OpenInputControl();
    }
}