using System;
using UnityEngine;
using UnityEngine.Serialization;

public enum UnitDoSomething
{
    None = 0,
    Move = 1,
    Attack = 2,
    Recruit = 3,
    Collect = 4,
    Obstacle = 5
}

public class UnitCollisionDetect : MonoBehaviour
{
    [SerializeField] private string _myTag;
    [SerializeField] private Collider2D myCollider;
    private UnitMain _myUnitMain;
    private UnitType _unitType;


    public void Init(UnitMain unitMain, UnitType unitType)
    {
        _myUnitMain = unitMain;
        _myTag = gameObject.tag;
        _unitType = unitType;
    }

    public void MoveCondition(Box box, UnitDirection dir)
    {
        var isAttackBox = box.IsAttackUnitThisBox(_unitType);
        if (isAttackBox)
        {
            var isEnemy = UnitsCollector.instance.GetUnitDamaged(_unitType.OppositeUnitType(), box, out var enemy);
            if (isEnemy)
            {
                var infoDamage = _myUnitMain.UnitStatus.OnUnitAttack();
                enemy.OnUnitDamaged(infoDamage);
                _myUnitMain.UnitMovement.SetRotation(dir);
                // CustomDebug.SetMessage($"Attack Enemy {enemy.name}", Color.yellow);
            }
            else
            {
                CustomDebug.SetMessage("Enemy is null", Color.red);
            }
        }
        else
        {
            if (_unitType == UnitType.Hero)
            {
                HeroHeadGroup.instance.MoveUnit(box, dir);
            }
            else
            {
                _myUnitMain.UnitMovement.Move(dir, box);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var unitDoSomething = GetUnitDoSomething(other);
        switch (unitDoSomething)
        {
            case UnitDoSomething.None:
                break;
            case UnitDoSomething.Move:
                CustomDebug.SetMessage("Move", Color.green);
                break;
            case UnitDoSomething.Attack:
                CustomDebug.SetMessage($"Attack => {other.gameObject.name}", Color.red);
                TakeDamage(other);
                break;
            case UnitDoSomething.Recruit:
                RecruitHero(other);
                break;
            case UnitDoSomething.Collect:
                CustomDebug.SetMessage("Collect", Color.yellow);
                break;
            case UnitDoSomething.Obstacle:
                CustomDebug.SetMessage("Obstacle", Color.yellow);
                break;
            default:
                break;
        }
    }

    private UnitDoSomething GetUnitDoSomething(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            return _myTag == "Hero" ? UnitDoSomething.Attack : UnitDoSomething.None;
        }
        else if (other.CompareTag("Hero"))
        {
            return _myTag == "Hero" ? UnitDoSomething.Recruit : UnitDoSomething.None;
        }
        else if (other.CompareTag("Collectable"))
        {
            return UnitDoSomething.Collect;
        }
        else if (other.CompareTag("Obstacle"))
        {
            return UnitDoSomething.Obstacle;
        }
        else
        {
            return UnitDoSomething.Move;
        }
    }

    private void TakeDamage(Collider2D other)
    {
        var unitMain = other.GetComponent<UnitMain>();
        if (!unitMain) return;
        var infoDamage = _myUnitMain.UnitStatus.OnUnitAttack();
        unitMain.OnUnitDamaged(infoDamage);
    }

    private void RecruitHero(Collider2D other)
    {
        var unitMain = other.GetComponent<UnitMain>();
        if (unitMain) HeroHeadGroup.instance.RecruitHero(unitMain);
    }


    public void EnableCollisionDetect()
    {
        enabled = true;
        myCollider.enabled = true;
    }

    public void DisableCollisionDetect()
    {
        enabled = false;
        myCollider.enabled = false;
    }
}