using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneWithFade(sceneName));
    }

    private IEnumerator LoadSceneWithFade(string sceneName)
    {
        // Fade out
        yield return FadeManager.Instance.StartCoroutine(FadeManager.Instance.FadeOut());

        // Sahneyi yükle
        SceneManager.LoadScene(sceneName);

        // Fade in (yeni sahnede yapýlacak)
    }

    public void QuitGame()
    {
        StartCoroutine(QuitWithFade());
    }

    private IEnumerator QuitWithFade()
    {
        yield return FadeManager.Instance.StartCoroutine(FadeManager.Instance.FadeOut());

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}