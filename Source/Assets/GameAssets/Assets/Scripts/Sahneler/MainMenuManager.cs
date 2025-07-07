using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    private bool isFirstLoad = true;
    void Start()
    {
        // AudioManager kontrolü ekleyin
        if (AudioManager.Instance != null)
        {
            // Sadece ilk yüklemede veya Settings'ten dönüþte müziði baþlat
            if (isFirstLoad || SceneManager.GetActiveScene().name != "Settings_Scene")
            {
                AudioManager.Instance.PlayMusic(AudioManager.Instance.mainMenuMusic);
            }
            isFirstLoad = false;
        }
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