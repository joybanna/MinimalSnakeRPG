using UnityEngine;
using UnityEngine.UI;

public class UIMainController : MonoBehaviour
{
    [SerializeField] private Button startGameButton;

    public void OnClickStartGame()
    {
        GameplayStateController.instance.OnGameStart();
    }
}