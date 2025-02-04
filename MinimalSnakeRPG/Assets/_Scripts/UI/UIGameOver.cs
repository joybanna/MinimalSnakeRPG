using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIGameOver : MonoBehaviour
{
    [SerializeField] protected RectTransform panel;
    [SerializeField] private TMP_Text txtScore;

    public void OpenPanel()
    {
        panel.gameObject.SetActive(true);
        PlayerHeroControl.instance.IsControlEnable = false;
        txtScore.text = "Score: " + UIScore.instance.Score;
    }

    public void OnClickRestart()
    {
        SoundController.instance.PlaySFX(SoundSource.UIClick);
        panel.gameObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }
}