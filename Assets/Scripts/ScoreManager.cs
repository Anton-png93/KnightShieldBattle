using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour


{
    public bool lastGoalByPlayer = false;
    
    public TextMeshProUGUI scoreText;
    public int leftScore = 0;
    public int rightScore = 0;

    public void AddLeftScore()
{
    leftScore++;
    lastGoalByPlayer = true; // игрок забил
    UpdateScoreText();
}

    public void AddRightScore()
{
    rightScore++;
    lastGoalByPlayer = false; // комп забил
    UpdateScoreText();
}

    void UpdateScoreText()
    {
        scoreText.text = leftScore + " : " + rightScore;
    }
}