using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuCanvas;
    public GameObject gameObjectsToEnable; // всё, что нужно включить при старте игры

    public void StartGame()
    {
        mainMenuCanvas.SetActive(false);
        gameObjectsToEnable.SetActive(true);
    }
}