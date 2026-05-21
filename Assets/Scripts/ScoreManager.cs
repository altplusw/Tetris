using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI linesText;

    int score = 0;
    int level = 1;
    int totalLines = 0;

    void Awake()
    {
        instance = this;
    }

    public void AddScore(int linesCleared)
    {
        // Classic Tetris scoring
        switch (linesCleared)
        {
            case 1: score += 100 * level; break;
            case 2: score += 300 * level; break;
            case 3: score += 500 * level; break;
            case 4: score += 800 * level; break; // Tetris!
        }

        totalLines += linesCleared;
        level = (totalLines / 10) + 1;

        UpdateUI();
    }

    void UpdateUI()
    {
        scoreText.text = "SCORE\n" + score.ToString();
        levelText.text = "LEVEL\n" + level.ToString();
        linesText.text = "LINES\n" + totalLines.ToString();
    }
    public int GetScore()
    {
    return score;
    }
}