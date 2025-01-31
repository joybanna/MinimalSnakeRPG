using System;
using UnityEngine;
using UnityEngine.Events;

public class UnitLevelProgression : MonoBehaviour
{
    private int _currentLevel;
    public int CurrentLevel => _currentLevel;

    [SerializeField] private UnitStatus unitStatus;

    private int _exp;
    private int _nextLevelExp;
    private UnityAction<int> _onUnitLevelUp;

    public void Init(UnitType uType, int level)
    {
        _currentLevel = level;
        unitStatus.Init(uType, level);
        _exp = this.CalculateExp();
    }

    public void AssignOnUnitLevelUp(UnityAction<int> action)
    {
        _onUnitLevelUp = action;
    }

    private void OnUnitLevelUp()
    {
        _currentLevel++;
        unitStatus.OnUnitLevelUp(_currentLevel);
        CustomDebug.SetMessage($"Unit Level Up {_currentLevel}", Color.green);
        _nextLevelExp = this.CalculateExp();
        _onUnitLevelUp?.Invoke(_currentLevel);
    }

    public void ReceiveExp(int exp)
    {
        CustomDebug.SetMessage($"Receive Exp {exp}", Color.green);
        _exp += exp;
        if (_exp >= _nextLevelExp)
        {
            OnUnitLevelUp();
        }
    }

    private void OnDisable()
    {
        _onUnitLevelUp = null;
    }
}