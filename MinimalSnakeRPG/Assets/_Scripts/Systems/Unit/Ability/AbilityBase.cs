using UnityEngine;

public class AbilityBase : MonoBehaviour
{
    protected UnitMain _unitMain;

    public void InitAbility(UnitMain unitMain)
    {
        _unitMain = unitMain;
    }

    public virtual void OnTurnEnd()
    {
        GameplayStateController.instance.OnPlayerTurnEnd();
    }

    public virtual void OnMoved()
    {
        
    }

    public virtual void OnAttack()
    {
    }
}