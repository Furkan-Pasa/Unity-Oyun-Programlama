using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    void Start()
    {
        // Ana menü müziðini çal
        AudioManager.Instance.PlayMusic(AudioManager.Instance.mainMenuMusic);
    }

    public void OnContinueClicked()
    {
        // Continue butonu þu anlýk new game gibi
        SceneLoader.Instance.LoadScene("Gameplay_Scene");
    }
    public void OnNewGameClicked()
    {
        SceneLoader.Instance.LoadScene("Gameplay_Scene");
    }

    public void OnSettingsClicked()
    {
        // Ayarlar sahnesinde müziði durdur
        // AudioManager.Instance.StopMusic();
        SceneLoader.Instance.LoadScene("Settings_Scene");
    }

    public void OnQuitClicked()
    {
        SceneLoader.Instance.QuitGame();
    }

    // Settings sahnesinden dönüþ için
    public void OnBackClicked()
    {
        // Eðer oyun içinden geliyorsa
        if (SceneManager.GetActiveScene().name == "Gameplay_Scene")
        {
            // Oyun duraklatýlmýþsa sýfýrla
            if (Time.timeScale == 0f)
            {
                Time.timeScale = 1f;
            }
        }
        SceneLoader.Instance.LoadScene("MainMenu_1");
    }
}