using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeManager : MonoBehaviour
{
    public static FadeManager Instance;

    private Image fadePanel;
    public float fadeSpeed = 1f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            CreateFadePanel();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void CreateFadePanel()
    {
        // Canvas oluþtur
        GameObject canvasObj = new GameObject("FadeCanvas");
        Canvas canvas = canvasObj.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 1000; // En üstte olmasý için

        // Canvas'ý FadeManager'ýn child'ý yap
        canvasObj.transform.SetParent(transform);

        // Image (FadePanel) oluþtur
        GameObject panelObj = new GameObject("FadePanel");
        fadePanel = panelObj.AddComponent<Image>();

        // Image'ýn canvas'ý kaplamasýný saðla
        RectTransform rect = panelObj.GetComponent<RectTransform>();
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;

        // Renk ve transparanlýk
        fadePanel.color = new Color(0, 0, 0, 0);

        // Image'ý canvas'ýn child'ý yap
        panelObj.transform.SetParent(canvasObj.transform, false);

        // Baþlangýçta pasif yap
        panelObj.SetActive(false);
    }

    public IEnumerator FadeOut()
    {
        // Panel yoksa yeniden oluþtur
        if (fadePanel == null || fadePanel.gameObject == null)
            CreateFadePanel();

        fadePanel.gameObject.SetActive(true);
        float alpha = 0f;

        while (alpha < 1f)
        {
            if (fadePanel == null) yield break; // Güvenlik kontrolü

            // eskisi
            // alpha += Time.deltaTime * fadeSpeed;
            alpha += Time.unscaledDeltaTime * fadeSpeed;
            fadePanel.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
    }

    public IEnumerator FadeIn()
    {
        // Panel yoksa yeniden oluþtur
        if (fadePanel == null || fadePanel.gameObject == null)
            CreateFadePanel();

        float alpha = 1f;

        while (alpha > 0f)
        {
            if (fadePanel == null) yield break; // Güvenlik kontrolü

            alpha -= Time.deltaTime * fadeSpeed;
            fadePanel.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        fadePanel.gameObject.SetActive(false);
    }

    public bool IsScreenDark()
    {
        if (fadePanel == null) return false;
        return fadePanel.color.a > 0.1f && fadePanel.gameObject.activeSelf;
    }

}