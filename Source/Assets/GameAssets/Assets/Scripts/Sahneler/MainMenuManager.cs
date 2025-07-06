using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
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
        SceneLoader.Instance.LoadScene("Settings_Scene");
    }

    public void OnQuitClicked()
    {
        SceneLoader.Instance.QuitGame();
    }

    // Settings sahnesinden dönüþ için
    public void OnBackClicked()
    {
        SceneLoader.Instance.LoadScene("MainMenu_1");
    }
}