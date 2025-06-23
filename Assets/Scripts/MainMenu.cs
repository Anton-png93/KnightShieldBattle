 using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuCanvas;
    public GameObject gameObjectsToEnable;
    public GameObject scorePanel; // <- ДОБАВИЛИ это

    public void StartGame()
    {
        mainMenuCanvas.SetActive(false);
        gameObjectsToEnable.SetActive(true);
        scorePanel.SetActive(true); // <- ДОБАВИЛИ это
    }
}