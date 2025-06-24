using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    private static MusicPlayer instance;
    private AudioSource audioSource;

    void Awake()
    {
        // Удаляем дубликаты при переходе между сценами
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject); // Сохраняем объект между сценами
        audioSource = GetComponent<AudioSource>();
        audioSource.Play(); // Запускаем музыку
    }
}
