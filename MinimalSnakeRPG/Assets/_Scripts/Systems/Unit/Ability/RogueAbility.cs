using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class RogueAbility : AbilityBase
{
    private int count = 0;

    public override void OnTurnEnd()
    {
        count++;
        if (count < 2)
        {
            StartCoroutine(ExtraAction());
        }
        else
        {
            count = 0;
            base.OnTurnEnd();
        }
    }


    private IEnumerator ExtraAction()
    {
        GridBoxesCollector.instance.HideMoveableArea();
        CustomDebug.SetMessage("Rogue extra action", Color.magenta);
        yield return new WaitForSeconds(0.2f);
        HeroHeadGroup.instance.OnPlayerTurnStart();
    }
    
}