using System.Collections.Generic;
using UnityEngine;

public enum Buff
{
    None = 0,
    Damage = 1,
    Defense = 2,
}

public class BuffCollector : MonoBehaviour
{
    public static BuffCollector instance;
    [SerializeField] private List<BuffRunner> buffs;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        buffs = new List<BuffRunner>();
    }

    public void AddBuff(BuffRunner buffRunner)
    {
        buffs.Add(buffRunner);
        UIGameplayController.instance.BuffGroup.AddBuff(buffRunner);
        HeroHeadGroup.instance.UpdateBonusStat();
    }

    public void RemoveBuff(BuffRunner buffRunner)
    {
        buffs.Remove(buffRunner);
    }

    public UnitStat GetBuffBonus()
    {
        var dmg = GetSumBuffValue(Buff.Damage);
        var def = GetSumBuffValue(Buff.Defense);
        var bonusStat = new UnitStat(0, dmg, def);
        return bonusStat;
    }

    public void OnTurnChange()
    {
        if (buffs.IsEmptyCollection()) return;
        for (var index = buffs.Count - 1; index >= 0; index--)
        {
            var buffRunner = buffs[index];
            if (buffRunner == null) continue;
            buffRunner.DecreaseTurn();
        }
    }

    private int GetSumBuffValue(Buff buff)
    {
        var sum = 0;
        foreach (var buffRunner in buffs)
        {
            if (buffRunner.Buff == buff)
            {
                sum += buffRunner.Value;
            }
        }

        return sum;
    }
}

#if UNITY_EDITOR
[UnityEditor.CustomEditor(typeof(BuffCollector))]
public class BuffCollectorEditor : UnityEditor.Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Add Damage Buff"))
        {
            var buffRunner = new BuffRunner(Buff.Damage, 10, 3);
            BuffCollector.instance.AddBuff(buffRunner);
        }

        if (GUILayout.Button("Add Defense Buff"))
        {
            var buffRunner = new BuffRunner(Buff.Defense, 10, 3);
            BuffCollector.instance.AddBuff(buffRunner);
        }
    }
}
#endif