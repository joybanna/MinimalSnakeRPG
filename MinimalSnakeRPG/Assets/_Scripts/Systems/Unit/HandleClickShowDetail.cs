using System;
using UnityEngine;

public class HandleClickShowDetail : MonoBehaviour
{
    private UnitMain _unitMain;
    private UIShowInfoUnit _uiShowInfoUnit;

    public void Init(UnitMain unitMain)
    {
        _unitMain = unitMain;
        _uiShowInfoUnit = UIGameplayController.instance.ShowInfoUnit;
    }

    private void OnMouseUp()
    {
        CustomDebug.SetMessage("Click on " + _unitMain.name, Color.cyan);
        _uiShowInfoUnit.ShowInfo(_unitMain);
        SoundController.instance.PlaySFX(SoundSource.UIClick);
    }

    private void OnDisable()
    {
        _uiShowInfoUnit.HideCard(_unitMain);
    }


    public void UpdateCard()
    {
        _uiShowInfoUnit.UpdateCard(_unitMain);
    }
}