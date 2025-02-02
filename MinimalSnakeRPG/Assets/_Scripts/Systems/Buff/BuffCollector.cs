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
    }

    public void AddBuff(Buff buff)
    {
        // Add buff to player
    }
}