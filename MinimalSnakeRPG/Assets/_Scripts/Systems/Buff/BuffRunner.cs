using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class BuffRunner : IEquatable<BuffRunner>
{
    [SerializeField] protected Buff buff;
    [SerializeField] protected int value;
    [SerializeField] protected int turnCount;

    public Buff Buff => buff;
    public int Value => value;
    public int TurnCount => turnCount;
    public UnityAction<int> onBuffTurnCountChange;
    public UnityAction onBuffRemove;

    public BuffRunner(Buff buff, int value, int turnCount)
    {
        this.buff = buff;
        this.value = value;
        this.turnCount = turnCount;
    }

    public void DecreaseTurn()
    {
        turnCount--;
        onBuffTurnCountChange?.Invoke(turnCount);
        if (turnCount > 0) return;
        BuffCollector.instance.RemoveBuff(this);
        onBuffRemove?.Invoke();
    }

    public bool Equals(BuffRunner other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return buff == other.buff && value == other.value;
    }

    public override bool Equals(object obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((BuffRunner)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine((int)buff, value);
    }
}