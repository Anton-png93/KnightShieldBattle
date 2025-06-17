using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public int leftScore = 0;
    public int rightScore = 0;

    public void AddLeftScore()
    {
        leftScore++;
        UpdateScoreText();
    }

    public void AddRightScore()
    {
        rightScore++;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        scoreText.text = leftScore + " : " + rightScore;
    }
}