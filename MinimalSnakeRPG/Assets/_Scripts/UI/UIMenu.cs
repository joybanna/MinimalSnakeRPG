using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIMenu : MonoBehaviour
{
    [SerializeField] private RectTransform myPanel;
    [SerializeField] Button startButton;
    [SerializeField] Button exitButton;
    [SerializeField] Button restartButton;
    [SerializeField] Button resumeButton;


    public void OpenMenu(bool isInGame)
    {
        myPanel.gameObject.SetActive(true);
        startButton.gameObject.SetActive(!isInGame);
        exitButton.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(isInGame);
        resumeButton.gameObject.SetActive(isInGame);
        Time.timeScale = 0;
        PlayerHeroControl.instance.IsControlEnable = false;
    }

    public void OnClickStart()
    {
        Time.timeScale = 1;
        myPanel.gameObject.SetActive(false);
        SoundController.instance.PlaySFX(SoundSource.UIClick);
        UIGameplayController.instance.OpenSelectStartHero();
        PlayerHeroControl.instance.IsControlEnable = true;
    }

    public void OnClickExit()
    {
        SoundController.instance.PlaySFX(SoundSource.UIClick);
        Application.Quit();
    }

    public void OnClickRestart()
    {
        SoundController.instance.PlaySFX(SoundSource.UIClick);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }

    public void OnClickResume()
    {
        Time.timeScale = 1;
        SoundController.instance.PlaySFX(SoundSource.UIClick);
        myPanel.gameObject.SetActive(false);
        PlayerHeroControl.instance.IsControlEnable = true;
    }
}