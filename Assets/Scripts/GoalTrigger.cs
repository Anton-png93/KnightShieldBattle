using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    public bool isTopGoal; // Если true — это верхний гол, значит игрок пропустил
    public MaceBallController maceBall;
    public ScoreManager scoreManager;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            if (isTopGoal)
{
        // Игроку забили гол → подача с его стороны
        scoreManager.AddRightScore();
          Debug.Log("Гол в ворота игрока → ResetBall(true)");
        maceBall.ResetBall(true);  // игрок начинает
}
else
{
        // Игрок забил гол → подача с противника
        scoreManager.AddLeftScore();
          Debug.Log("Гол в ворота врага → ResetBall(false)");
        maceBall.ResetBall(false); // враг начинает
}

           maceBall.ResetBall(!scoreManager.lastGoalByPlayer);
        }
    }
}