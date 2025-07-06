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
        // Fade out
        yield return StartCoroutine(FadeManager.Instance.FadeOut());

        // Sahneyi yükle
        SceneManager.LoadScene(sceneName);

        // Yeni sahnede SceneStarter otomatik fade-in yapacak
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