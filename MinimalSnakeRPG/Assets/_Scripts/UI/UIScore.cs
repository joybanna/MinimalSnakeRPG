using UnityEngine;

public class UIScore : MonoBehaviour
{
    public static UIScore instance;
    [SerializeField] private TMPro.TMP_Text scoreText;

    private int _score;
    
    public int Score => _score;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        _score = 0;
        UpdateScore();
    }

    public void AddScore(int score)
    {
        _score += score;
        UpdateScore();
    }

    private void UpdateScore()
    {
        scoreText.text = $"Score: {_score}";
    }
}