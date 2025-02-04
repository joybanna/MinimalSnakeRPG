using UnityEngine;

public class UIDmgShowGroup : MonoBehaviour
{
    [SerializeField] private UIDmgShowCard _dmgShowCardPlayer;
    [SerializeField] private UIDmgShowCard _dmgShowCardEnemy;

    [SerializeField] private Camera _camera;

    private void Awake()
    {
        _dmgShowCardPlayer.Init(_camera);
        _dmgShowCardEnemy.Init(_camera);
    }

    public void ShowDmg(UnitMain unitMain, int dmg)
    {
        if (unitMain.UnitType == UnitType.Hero)
        {
            _dmgShowCardPlayer.ShowDmg(unitMain, dmg);
        }
        else
        {
            _dmgShowCardEnemy.ShowDmg(unitMain, dmg);
        }
    }
}