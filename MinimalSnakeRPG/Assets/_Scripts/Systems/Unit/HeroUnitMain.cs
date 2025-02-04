public class HeroUnitMain : UnitMain
{
    protected override void OnUnitLevelUp(int level)
    {
        base.OnUnitLevelUp(level);
        HeroHeadGroup.instance.UpdatePlayerUnit(this);
    }

    public override void OnUnitDamaged(InfoDamage infoDamage)
    {
        base.OnUnitDamaged(infoDamage);
        HeroHeadGroup.instance.UpdatePlayerUnit(this);
    }

    public override void OnUnitHealed(int heal)
    {
        base.OnUnitHealed(heal);
        HeroHeadGroup.instance.UpdatePlayerUnit(this);
    }

    public override void OnUnitDie()
    {
        CurrentBox.BoxStatus = BoxStatus.Enemy;
        HeroHeadGroup.instance.OnHeroDie(this);
        UIGameplayController.instance.PlayerUnits.RemoveCardPlayerUnit(this);
        base.OnUnitDie();
    }
}