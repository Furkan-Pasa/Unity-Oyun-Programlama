using UnityEngine;
using System.Collections;

public class SceneStarter : MonoBehaviour
{
    IEnumerator Start()
    {
        // FadeManager'ýn hazýr olmasýný bekle
        yield return new WaitUntil(() => FadeManager.Instance != null);

        // Eðer ekran kararýksa aç
        if (FadeManager.Instance.IsScreenDark())
        {
            yield return FadeManager.Instance.StartCoroutine(FadeManager.Instance.FadeIn());
        }
        else
        {
            // Ekran zaten aydýnlýksa direkt fade-in yap
            yield return FadeManager.Instance.StartCoroutine(FadeManager.Instance.FadeIn());
        }

        // Ýþi bitince kendini yok et
        Destroy(gameObject);
    }
}