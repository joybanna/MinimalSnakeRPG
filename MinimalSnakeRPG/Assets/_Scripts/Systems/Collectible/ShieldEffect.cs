public class ShieldEffect : CollectibleEffect
{
    public override void ApplyEffect()
    {
        var buff = new BuffRunner(Buff.Defense, 10, 3);
        buff.ApplyBuff();
    }
}