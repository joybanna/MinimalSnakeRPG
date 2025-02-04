public class PotionEffect : CollectibleEffect
{
    public override void ApplyEffect()
    {
        HeroHeadGroup.instance.HealHeadHero(5);
    }
}