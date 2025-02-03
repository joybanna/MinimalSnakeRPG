public class SwordEffect : CollectibleEffect
{
    public override void ApplyEffect()
    {
        var buff = new BuffRunner(Buff.Damage, 5, 5);
        buff.ApplyBuff();
    }
}