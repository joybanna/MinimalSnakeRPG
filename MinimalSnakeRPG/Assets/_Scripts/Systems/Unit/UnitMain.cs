using UnityEngine;

public class UnitMain : MonoBehaviour
{
    [SerializeField] private UnitType unitType = UnitType.Hero;
    [SerializeField] private UnitMovement unitMovement;
    [SerializeField] private UnitCollisionDetect unitCollisionDetect;

    public UnitMovement UnitMovement => unitMovement;
    public UnitCollisionDetect UnitCollisionDetect => unitCollisionDetect;

    private void Start()
    {
        unitMovement.Init(unitType, unitType == UnitType.Hero ? UnitDirection.Up : UnitDirection.Down);
    }

    public void OnUnitRecruited()
    {
        unitMovement.enabled = false;
        unitCollisionDetect.DisableCollisionDetect();
    }
}