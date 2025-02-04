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
        UIGameplayController.instance.PlayerUnits.InitHeadHero(head);
        head.ShowArrowDir(true);
    }

    public void HealHeadHero(int heal)
    {
        head.OnUnitHealed(heal);
        UpdatePlayerUnit(head);
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

        head.OnTurnEnd();
    }

    public void MoveUnit(Box box, UnitDirection dir)
    {
        head.UnitMovement.Move(dir, box);
        historyMove.AddHistory(dir, box);
        head.AbilityBase.OnMoved();
        // move other hero
        MoveOtherHeroes();

        SoundController.instance.PlaySFX(SoundSource.Move);
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

        UIGameplayController.instance.PlayerUnits.RecruitedHero(unitMain);
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
        head.ShowArrowDir(false);
        heroMovements.Remove(head);
        heroMovements.Add(head);
        PickHead();
        RearrangeHeroes();
        CustomDebug.SetMessage("Swap Head to Last Hero", Color.green);
        UIGameplayController.instance.PlayerUnits.SwapFirstToLast();
        GameplayStateController.instance.OnPlayerTurnEnd();
        SoundController.instance.PlaySFX(SoundSource.UIClick);
    }

    public void SwapLastHeroToHead()
    {
        if (heroMovements.Count <= 1) return;
        head.ShowArrowDir(false);
        var lastHero = heroMovements[^1];
        heroMovements.Remove(lastHero);
        heroMovements.Insert(0, lastHero);
        RearrangeHeroes();
        PickHead();
        CustomDebug.SetMessage("Swap Last Hero to Head", Color.green);
        UIGameplayController.instance.PlayerUnits.SwapLastToFirst();
        GameplayStateController.instance.OnPlayerTurnEnd();
        SoundController.instance.PlaySFX(SoundSource.UIClick);
    }

    private void PickHead()
    {
        head = heroMovements[0];
        head.ShowArrowDir(true);
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
        UpdatePlayerUnit(head);

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
            CustomDebug.SetMessage("#### Game Over", Color.red);
            GameplayStateController.instance.OnGameEnd();
            SoundController.instance.PlaySFX(SoundSource.GameOver);
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

    public void UpdatePlayerUnit(UnitMain unitMain) // buff , level up
    {
        UIGameplayController.instance.PlayerUnits.UpdatePlayerUnit(unitMain);
    }

    public void UpdateBonusStat()
    {
        foreach (var heroMovement in heroMovements)
        {
            heroMovement.UnitStatus.UpdateBonusStat();
        }

        UpdatePlayerUnit(null);
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