using UnityEngine;

public class UIUnitStatsCards : MonoBehaviour
{
    [SerializeField] private UIUnitStatCard[] unitStatCards;
    [SerializeField] private StatType[] statTypes;

    public void InitStatsCards(UnitClass uClass, int level)
    {
        var unitStats = uClass.GetUnitStats(level);
        for (int iStat = statTypes.Length - 1; iStat >= 0; iStat--)
        {
            var stat = statTypes[iStat];
            for (int iCard = unitStatCards.Length - 1; iCard >= 0; iCard--)
            {
                if (unitStatCards[iCard].StatType != statTypes[iStat]) continue;
                var cCard = unitStatCards[iCard];
                var statValue = unitStats.GetStatValue(stat);
                cCard.SetStatValue(statValue, 0);
            }
        }
    }
}