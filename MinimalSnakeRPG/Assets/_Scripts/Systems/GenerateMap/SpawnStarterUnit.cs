using UnityEngine;

public class SpawnStarterUnit : SpawnUnit
{
    [SerializeField] private HeroHeadGroup heroHeadGroup;

    protected override UnitMain SpawnUnitMain(UnitMain prefab, Box box)
    {
        var unit = Instantiate(prefab, box.transform.position, Quaternion.identity);
        var dir = ExtensionSystems.GetRandomDirection();
        var info = new InfoInitUnit(unitType, dir, box, 1);
        unit.Init(info);
        if (parent) unit.transform.SetParent(parent);
        heroHeadGroup.InitHeadHero(unit, info);
        return unit;
    }
}