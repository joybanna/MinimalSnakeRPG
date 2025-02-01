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
    [SerializeField] private PlayerHeroControl playerHeroControl;

    public Box HeadBox => head.UnitMovement.CurrentBox;

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

    public void InitHeadHero(UnitMain leader, InfoInitUnit infoInitUnit)
    {
        head = leader;
        heroMovements ??= new List<UnitMain>();
        heroMovements.Add(head);
        historyMove.InitHistory(infoInitUnit.box, infoInitUnit.direction);
    }


    private UnitDirection _willMoveDir;
    private Box _willMoveBox;

    public void Move(UnitDirection dir)
    {
        var box = GridBoxesCollector.instance.GetBoxMoved(dir);
        if (box == null)
        {
            CustomDebug.SetMessage("Can't move box is null", Color.red);
            return;
        }

        _willMoveDir = dir;
        _willMoveBox = box;
        head.UnitCollisionDetect.MoveCondition(box, dir);
    }

    public void MoveUnit(Box box, UnitDirection dir)
    {
        head.UnitMovement.Move(dir, box);
        historyMove.AddHistory(dir, box);
        // move other hero
        MoveOtherHeroes();
    }

    private void MoveOtherHeroes()
    {
        for (var index = 0; index < heroMovements.Count; index++)
        {
            var heroMovement = heroMovements[index];
            if (heroMovement == head) continue;
            var heroBox = historyMove.GetLastHeroPos(index + 1);
            var heroDir = historyMove.GetLastHeroDir(index + 1);
            heroMovement.UnitMovement.Move(heroDir, heroBox);
        }
    }

    public void RecruitHero(UnitMain unitMain)
    {
        if (unitMain == head) return;
        var box = historyMove.GetLastHeroPos(heroMovements.Count + 1);
        CustomDebug.SetMessage($"Recruit Hero at {box.name} : {heroMovements.Count}", Color.green);
        unitMain.OnUnitRecruited();
        unitMain.UnitMovement.Move(head.UnitMovement.CurrentDirection, box);
        heroMovements.Add(unitMain);
    }

    public void OnPlayerTurnStart()
    {
        var currentBox = historyMove.GetCurrentBox();
        if (currentBox == null)
        {
            CustomDebug.SetMessage("Current Box is null", Color.red);
            return;
        }

        GridBoxesCollector.instance.ShowMoveableArea(currentBox, head.UnitMovement.CurrentDirection);
        playerHeroControl.OpenInputControl();
    }

    private void OnDrawGizmos()
    {
        if (heroMovements.IsEmptyCollection()) return;
        var box = historyMove.GetLastHeroPos(heroMovements.Count + 1);
        if (box == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(box.transform.position, 0.5f);
    }

    public void SwapHeadToLastHero()
    {
        if (heroMovements.Count <= 1) return;
        heroMovements.Remove(head);
        heroMovements.Add(head);
        RearrangeHeroes();
        head = heroMovements[0];
        CustomDebug.SetMessage("Swap Head to Last Hero", Color.green);
        GameplayStateController.instance.OnPlayerTurnEnd();
    }

    public void SwapLastHeroToHead()
    {
        if (heroMovements.Count <= 1) return;
        var lastHero = heroMovements[^1];
        heroMovements.Remove(lastHero);
        heroMovements.Insert(0, lastHero);
        RearrangeHeroes();
        head = heroMovements[0];
        CustomDebug.SetMessage("Swap Last Hero to Head", Color.green);
        GameplayStateController.instance.OnPlayerTurnEnd();
    }

    public void RearrangeHeroes()
    {
        for (var index = 0; index < heroMovements.Count; index++)
        {
            var heroMovement = heroMovements[index];
            var box = historyMove.GetLastHeroPos(index + 1);
            var dir = historyMove.GetLastHeroDir(index + 1);
            heroMovement.UnitMovement.Move(dir, box);
        }
    }

    public void OnEnemyDie(int exp)
    {
        head.UnitLevelProgression.ReceiveExp(exp);

        if (_willMoveBox) // replace enemy box
        {
            MoveUnit(_willMoveBox, _willMoveDir);
        }
    }

    public void OnHeroDie(UnitMain unitMain)
    {
        heroMovements.Remove(unitMain);
        if (heroMovements.IsEmptyCollection())
        {
            // Game Over
        }
        else
        {
            if (unitMain == head)
            {
                head = heroMovements[0];
            }

            RearrangeHeroes();
        }
    }
}

#if UNITY_EDITOR
[UnityEditor.CustomEditor(typeof(HeroHeadGroup))]
public class HeroHeadGroupEditor : UnityEditor.Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var heroHeadGroup = target as HeroHeadGroup;
        if (GUILayout.Button("Swap Head to Last Hero"))
        {
            heroHeadGroup.SwapHeadToLastHero();
        }

        if (GUILayout.Button("Swap Last Hero to Head"))
        {
            heroHeadGroup.SwapLastHeroToHead();
        }

        if (GUILayout.Button("Rearrange Heroes"))
        {
            heroHeadGroup.RearrangeHeroes();
        }
    }
}

#endif