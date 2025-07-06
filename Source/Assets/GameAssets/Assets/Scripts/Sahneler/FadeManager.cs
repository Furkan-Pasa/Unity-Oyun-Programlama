using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeManager : MonoBehaviour
{
    public static FadeManager Instance;

    public Image fadePanel;
    public float fadeSpeed = 1f;

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

    public IEnumerator FadeOut()
    {
        fadePanel.gameObject.SetActive(true);
        float alpha = 0f;

        while (alpha < 1f)
        {
            alpha += Time.deltaTime * fadeSpeed;
            fadePanel.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
    }

    public IEnumerator FadeIn()
    {
        float alpha = 1f;

        while (alpha > 0f)
        {
            alpha -= Time.deltaTime * fadeSpeed;
            fadePanel.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        fadePanel.gameObject.SetActive(false);
    }
}