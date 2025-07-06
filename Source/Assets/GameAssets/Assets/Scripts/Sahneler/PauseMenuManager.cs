using System;
using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    public static PauseMenuManager Instance;
    public GameObject pauseMenuUI;

    private bool isPaused = false;

    public static event Action<bool> OnPauseStateChanged;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f; // Oyun zamanýný durdur
        pauseMenuUI.SetActive(true);

        // Fare kontrolünü serbest býrak
        MouseMovement.Instance.SetMouseLock(false);

        OnPauseStateChanged?.Invoke(true);
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f; // Oyun zamanýný baþlat
        pauseMenuUI.SetActive(false);

        // Fare kontrolünü kilitle
        MouseMovement.Instance.SetMouseLock(true);

        OnPauseStateChanged?.Invoke(false);
    }

    public void LoadSettings()
    {
        // Ayarlar sahnesine geçiþ yap
        Time.timeScale = 1f; // Zamaný sýfýrla
        SceneLoader.Instance.LoadScene("Settings_Scene");
    }

    public void QuitToMainMenu()
    {
        // Ana menüye dön
        Time.timeScale = 1f;
        SceneLoader.Instance.LoadScene("MainMenu_1");
    }

    public void QuitGame()
    {
        // Oyunu kapat
        SceneLoader.Instance.QuitGame();
    }
}