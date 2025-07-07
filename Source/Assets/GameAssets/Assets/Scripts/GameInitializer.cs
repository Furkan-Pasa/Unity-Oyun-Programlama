using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    void Start()
    {
        // Oyun baþlangýcýnda fareyi kilitle
        MouseMovement.Instance.SetMouseLock(true);

        // Zamaný baþlat
        Time.timeScale = 1f;

        // Oyun müziðini baþlat
        AudioManager.Instance.PlayMusic(AudioManager.Instance.gameplayMusic);
    }
}