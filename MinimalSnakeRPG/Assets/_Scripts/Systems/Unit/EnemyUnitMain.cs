public class EnemyUnitMain : UnitMain
{
    public override void OnUnitDie()
    {
        CurrentBox.BoxStatus = BoxStatus.Hero;
        var exp = unitLevelProgression.CalculateExp();
        UIScore.instance.AddScore(unitLevelProgression.CalculateScore());
        HeroHeadGroup.instance.OnEnemyDie(exp);
        base.OnUnitDie();
    }
}