using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMainController : MonoBehaviour
{
    [SerializeField] private TMP_Text enemyWaveText;
    [SerializeField] private TMP_Text turnCountText;

    public void SetEnemyWaveText(int wave)
    {
        enemyWaveText.text = $"Wave {wave}";
    }

    public void SetTurnCountText(int turnCount)
    {
        turnCountText.text = $"Turn {turnCount}";
    }
}