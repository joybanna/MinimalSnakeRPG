using System;
using UnityEngine;

public class HeroControl : MonoBehaviour
{
    [SerializeField] private HeroHeadGroup headGroup;
    [SerializeField] private float delayControl = 0.5f;

    private float _lastInputTime;


    public void Move(UnitDirection dir)
    {
        _lastInputTime = Time.time;
        if (headGroup.IsMoveAble(dir))
        {
            CustomDebug.SetMessage($"Move {dir}", Color.green);
            headGroup.Move(dir);
            GameplayStateController.instance.OnPlayerTurnEnd();
            this.enabled = false;
        }
        else
        {
            CustomDebug.SetMessage("Can't move to opposite direction", Color.yellow);
        }
    }
    
    public void OpenInputControl()
    {
        this.enabled = true;
    }

    private void Update()
    {
        if (Time.time - _lastInputTime < delayControl) return;
        if (GameplayStateController.instance.CurrentState != GameplayState.PlayerTurn) return;
        var hAxis = Input.GetAxisRaw("Horizontal");
        var vAxis = Input.GetAxisRaw("Vertical");
        if (hAxis == 0 && vAxis == 0) return;
        if (Mathf.Abs(hAxis) > Mathf.Abs(vAxis))
        {
            Move(hAxis > 0 ? UnitDirection.Right : UnitDirection.Left);
        }
        else
        {
            Move(vAxis > 0 ? UnitDirection.Up : UnitDirection.Down);
        }
    }
}