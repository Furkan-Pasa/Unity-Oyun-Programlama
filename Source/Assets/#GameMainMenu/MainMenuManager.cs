using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void OnContinueClicked()
    {
        // Continue butonu için þu anlýk boþ býrakýyoruz
        SceneManager.LoadScene("Gameplay_Scene");
    }

    public void OnNewGameClicked()
    {
        // Gameplay sahnesini yükle
        SceneManager.LoadScene("Gameplay_Scene");
    }

    public void OnSettingsClicked()
    {
        // Settings sahnesini yükle
        SceneManager.LoadScene("Settings_Scene");
    }

    public void OnQuitClicked()
    {
        // Oyunu kapat
        Debug.Log("Oyun kapatýlýyor...");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}