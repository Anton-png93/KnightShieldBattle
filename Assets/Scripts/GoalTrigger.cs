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
            // Игроку забили гол
            scoreManager.AddRightScore();
            maceBall.ResetBall(true); // мяч появляется на стороне игрока
        }
        else
        {
            // Игрок забил гол
            scoreManager.AddLeftScore();
            maceBall.ResetBall(false); // мяч появляется на стороне противника
        }
    }
}

}