using System.Collections.Generic;
using UnityEngine;

public class EnemyAutoMove : MonoBehaviour
{
    [SerializeField] private bool _isMovable;
    [SerializeField] private UnitMain _unitMain;

    private Box _currentBox => _unitMain ? _unitMain.CurrentBox : null;
    private UnitDirection _currentDirection => _unitMain ? _unitMain.UnitMovement.CurrentDirection : UnitDirection.None;
    public bool IsMovable => _isMovable;

    public void InitEnemy(bool isMovable)
    {
        this._isMovable = isMovable;
    }

    private Dictionary<UnitDirection, Box> GetMyNeighbours()
    {
        if (!_isMovable)
        {
            CustomDebug.SetMessage("Enemy is not movable", Color.red);
            return null;
        }

        var neighbourBox =
            GridBoxesCollector.instance.GetNeighboursWithMoveable(_currentBox, UnitType.Enemy, _currentDirection);
        if (neighbourBox.Count == 0)
        {
            CustomDebug.SetMessage("No neighbour box", Color.red);
            return null;
        }

        return neighbourBox;
    }


    public void MoveEnemy()
    {
        if (!_isMovable) return;
        var neighbourBox = GetMyNeighbours();
        if (neighbourBox == null) return;

        var playerBox = HeroHeadGroup.instance.HeadBox;
        if (playerBox == null) return;

        var isBox = neighbourBox.GetNearestBox(playerBox, out Box nearestBox, out UnitDirection dir);
        if (!isBox)
        {
            CustomDebug.SetMessage("Nearest box is null", Color.red);
            return;
        }

        _unitMain.UnitCollisionDetect.MoveCondition(nearestBox, dir);
    }
}