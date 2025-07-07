using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneWithFade(sceneName));
    }

    private IEnumerator LoadSceneWithFade(string sceneName)
    {
        yield return StartCoroutine(FadeManager.Instance.FadeOut());

        // Mevcut sahne bilgisini sakla
        string currentScene = SceneManager.GetActiveScene().name;

        SceneManager.LoadScene(sceneName);

        // Müzik yönetimi
        if (AudioManager.Instance != null)
        {
            // Settings'ten MainMenu'ye dönüþte müziði devam ettir
            if (currentScene == "Settings_Scene" && sceneName == "MainMenu_1")
            {
                // Müziði deðiþtirme, sadece devam etsin
            }
            // Gameplay'e geçiþte müziði deðiþtir
            else if (sceneName == "Gameplay_Scene")
            {
                yield return new WaitForSecondsRealtime(0.1f);
                AudioManager.Instance.PlayMusic(AudioManager.Instance.gameplayMusic);
            }
        }

    }

    public void LoadSceneWithPauseReset(string sceneName)
    {
        // Oyun duraklatýlmýþsa sýfýrla
        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
        }
        LoadScene(sceneName);
    }

    public void QuitGame()
    {
        StartCoroutine(QuitWithFade());
    }

    private IEnumerator QuitWithFade()
    {
        // Fade out sýrasýnda zaman ölçeðini sýfýrla
        Time.timeScale = 1f;

        yield return StartCoroutine(FadeManager.Instance.FadeOut());

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
    }
}