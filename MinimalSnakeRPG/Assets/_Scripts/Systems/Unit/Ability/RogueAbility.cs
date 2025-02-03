public class RogueAbility : AbilityBase
{
    private int count = 0;

    public override void OnTurnEnd()
    {
        count++;
        if (count != 2) return;
        base.OnTurnEnd();
        count = 0;
    }
}