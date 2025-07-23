using UnityEngine;
using TMPro;
using System.Collections;

public class ScoreManager : MonoBehaviour




{
    public bool lastGoalByPlayer = false;
    public bool isFirstServe = true;

    public TextMeshProUGUI scoreText;
    public int leftScore = 0;
    public int rightScore = 0;
    public MaceBallController ballController;

    public void AddLeftScore()
    {
        leftScore++;
        lastGoalByPlayer = true;
        UpdateScoreText();

        if (leftScore >= 10 || rightScore >= 10)
        {

        }
        StartCoroutine(RestartAfterGoal());
    }

    public void AddRightScore()
    {
        rightScore++;
        lastGoalByPlayer = false;
        UpdateScoreText();

        if (leftScore >= 10 || rightScore >= 10)
        {

        }
        StartCoroutine(RestartAfterGoal());
    }

    void UpdateScoreText()
    {
        scoreText.text = leftScore + " : " + rightScore;
    }
    IEnumerator RestartAfterGoal()
{
    yield return new WaitForSeconds(1f);
    ballController.ResetBall(lastGoalByPlayer);
}
    
    
}