public class WarriorAbility : AbilityBase
{
    private readonly float _heal = 0.1f;

    public override void OnMoved()
    {
        var maxHp = _unitMain.UnitStatus.MaxHp;
        var heal = (int)(maxHp * _heal);
        _unitMain.OnUnitHealed(heal);
        base.OnMoved();
    }
}