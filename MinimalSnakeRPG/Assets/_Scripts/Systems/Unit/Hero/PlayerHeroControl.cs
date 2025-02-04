using System;
using UnityEngine;

public class PlayerHeroControl : MonoBehaviour
{
    public static PlayerHeroControl instance;
    [SerializeField] private HeroHeadGroup headGroup;
    [SerializeField] private float delayControl = 0.5f;

    private float _lastInputTime;

    public bool IsControlEnable
    {
        get => _isControlEnable;
        set
        {
            _isControlEnable = value;
            this.enabled = value;
            // CustomDebug.SetMessage($"IsControlEnable: {value}", Color.yellow);
        }
    }

    private bool _isControlEnable;

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

    public void Move(UnitDirection dir)
    {
        _lastInputTime = Time.time;
        if (GridBoxesCollector.instance.IsMoveAble(dir))
        {
            // CustomDebug.SetMessage($"Move {dir}", Color.green);
            headGroup.Move(dir);
            IsControlEnable = false;
        }
        else
        {
            CustomDebug.SetMessage("Can't move to opposite direction", Color.yellow);
        }
    }
    

    private void Update()
    {
        if (IsControlEnable == false) return;
        if (Time.time - _lastInputTime < delayControl) return;
        if (GameplayStateController.instance.CurrentState != GameplayState.PlayerTurn) return;
        if (Input.GetKeyUp(KeyCode.Q))
        {
            headGroup.SwapHeadToLastHero();
            return;
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            headGroup.SwapLastHeroToHead();
            return;
        }

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