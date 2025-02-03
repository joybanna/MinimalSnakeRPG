using System;
using UnityEngine;
using UnityEngine.Events;

public class UnitLevelProgression : MonoBehaviour
{
    private int _currentLevel;
    public int CurrentLevel => _currentLevel;
    private int _exp;
    public int CurrentExp => _exp;
    private int _nextLevelExp;
    public int NextLevelExp => _nextLevelExp;
    private UnityAction<int> _onUnitLevelUp;

    public void Init(InfoInitUnit infoInitUnit)
    {
        _currentLevel = infoInitUnit.level;
        _exp = this.CalculateExp();
    }

    public void AssignOnUnitLevelUp(UnityAction<int> action)
    {
        _onUnitLevelUp = action;
    }

    private void OnUnitLevelUp()
    {
        _currentLevel++;
        _nextLevelExp = this.NextLevelExp();
        CustomDebug.SetMessage($"Unit Level Up {_currentLevel} : {_exp} / {_nextLevelExp} to next level", Color.green);
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