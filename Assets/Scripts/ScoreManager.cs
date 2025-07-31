using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour



{
    public GameObject restartButton; // сюда перетащим кнопку из Canvas
    public bool lastGoalByPlayer = false;
    public bool isFirstServe = true;
    
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

    // Проверка: если разница >= 10
    if (Mathf.Abs(leftScore - rightScore) >= 10)
    {
        EndGame();
    }
}

void EndGame()
{
    // Останавливаем время
    Time.timeScale = 0f;

    // Показываем кнопку Restart
    restartButton.SetActive(true);
}

// Метод для кнопки
public void RestartGame()
{
    Time.timeScale = 1f;
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
}
}