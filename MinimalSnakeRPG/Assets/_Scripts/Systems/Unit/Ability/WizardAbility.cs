using System.Collections.Generic;

public class WizardAbility : AbilityBase
{
    public override void OnAttack()
    {
        AttackEnemies();
        base.OnAttack();
    }

    private void AttackEnemies()
    {
        var myBox = _unitMain.CurrentBox;
        var dir = _unitMain.UnitMovement.CurrentDirection;
        var unitType = _unitMain.UnitType;
        var neighborBox = GridBoxesCollector.instance.GetNeighboursWithMoveable(myBox, _unitMain.UnitType, dir);
        foreach (var box in neighborBox)
        {
            var isEnemy = UnitsCollector.instance.GetUnitDamaged(unitType.OppositeUnitType(), box.Value, out var enemy);
            if (isEnemy)
            {
                var infoDamage = _unitMain.UnitStatus.OnUnitAttack();
                enemy.OnUnitDamaged(infoDamage);
            }
        }
    }
}