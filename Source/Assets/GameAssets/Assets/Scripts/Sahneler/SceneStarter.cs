using UnityEngine;
using System.Collections;

public class SceneStarter : MonoBehaviour
{
    IEnumerator Start()
    {
        // 1 frame bekle (sistemlerin hazýr olmasý için)
        yield return null;

        if (FadeManager.Instance != null)
        {
            // Fade-in efekti
            yield return FadeManager.Instance.StartCoroutine(FadeManager.Instance.FadeIn());
        }

        // Ýþi bitince kendini yok et
        Destroy(gameObject);
    }
}