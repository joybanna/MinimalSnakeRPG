using UnityEngine;
using UnityEngine.SceneManagement;

public class UIGameOver : MonoBehaviour
{
    [SerializeField] protected RectTransform panel;

    public void OpenPanel()
    {
        panel.gameObject.SetActive(true);
    }

    public void OnClickRestart()
    {
        panel.gameObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }
}