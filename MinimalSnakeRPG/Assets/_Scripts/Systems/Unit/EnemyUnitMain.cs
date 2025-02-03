public class EnemyUnitMain : UnitMain
{
    public override void OnUnitDie()
    {
        // remove & give reward
        CurrentBox.BoxStatus = BoxStatus.Empty;
        var exp = unitLevelProgression.CalculateExp();
        HeroHeadGroup.instance.OnEnemyDie(exp);
        base.OnUnitDie();
    }
}