using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject gameOverPanel;
    public TextMeshProUGUI finalScoreText;
    bool isGameOver = false;

    void Awake()
    {
        instance = this;
    }

    public void GameOver()
    {
        if (isGameOver) return;
        isGameOver = true;
        AudioManager.instance.PlayGameOver();
        // Show final score
        finalScoreText.text = "SCORE\n" + ScoreManager.instance.GetScore();

        // Show game over panel
        gameOverPanel.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public bool IsGameOver()
    {
        return isGameOver;
    }
}