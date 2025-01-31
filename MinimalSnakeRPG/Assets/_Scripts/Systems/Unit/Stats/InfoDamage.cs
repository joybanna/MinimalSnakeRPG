using UnityEngine;

public struct InfoDamage
{
    public UnitClass attackerClass;
    public int finalDamage;

    public InfoDamage(UnitClass attackerClass, int finalDamage)
    {
        this.attackerClass = attackerClass;
        this.finalDamage = finalDamage;
    }
}