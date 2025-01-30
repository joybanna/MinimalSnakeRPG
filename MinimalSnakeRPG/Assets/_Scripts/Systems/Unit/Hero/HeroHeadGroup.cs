using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Serialization;

public class HeroHeadGroup : MonoBehaviour
{
    public static HeroHeadGroup instance;
    [SerializeField] private List<UnitMain> heroMovements;
    [SerializeField] private UnitMain head;
    [SerializeField] private HistoryMove historyMove;
    [SerializeField] private HeroControl heroControl;

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
        var box = GridBoxesCollector.instance.GetBoxMoved(dir);
        if (box == null)
        {
            CustomDebug.SetMessage("Can't move box is null", Color.red);
            return;
        }

        head.UnitMovement.Move(dir, box);
        historyMove.AddHistory(dir, box);

        // move other hero
        for (var index = 0; index < heroMovements.Count; index++)
        {
            var heroMovement = heroMovements[index];
            if (heroMovement == head) continue;
            var heroBox = historyMove.GetHeroPos(index);
            heroMovement.UnitMovement.Move(dir, heroBox);
        }
    }

    public void RecruitHero(UnitMain unitMain)
    {
        var box = historyMove.GetLastHeroPos(heroMovements.Count);
        CustomDebug.SetMessage($"Recruit Hero at {box.name} : {heroMovements.Count}", Color.green);
        unitMain.OnUnitRecruited();
        unitMain.UnitMovement.Move(head.UnitMovement.CurrentDirection, box);
        heroMovements.Add(unitMain);
    }

    public void OnPlayerTurnStart()
    {
        var currentBox = historyMove.GetCurrentBox();
        // open moveable area
        GridBoxesCollector.instance.ShowMoveableArea(currentBox, head.UnitMovement.CurrentDirection);
        // open input control
        heroControl.OpenInputControl();
    }

    private void OnDrawGizmos()
    {
        if (heroMovements == null || heroMovements.Count == 0) return;
        var box = historyMove.GetLastHeroPos(heroMovements.Count);
        if (box == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(box.transform.position, 0.5f);
    }
}