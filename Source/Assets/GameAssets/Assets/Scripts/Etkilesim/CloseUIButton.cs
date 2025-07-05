using UnityEngine;
using UnityEngine.UI;

public class CloseUIButton : MonoBehaviour
{
    void Start()
    {
        Button btn = GetComponent<Button>();
        if (btn != null)
        {
            btn.onClick.AddListener(CloseParentUI);
        }
    }

void CloseParentUI()
{
    Transform current = transform;

    // En fazla 5 üst parent'a kadar çýkalým (sonsuz döngüyü engellemek için sýnýr koyarýz)
    for (int i = 0; i < 5; i++)
    {
        if (current == null) break;

        if (current.name.Contains("UI"))
        {
            current.gameObject.SetActive(false);
            MouseMovement.Instance.SetMouseLock(true);
            return;
        }

        current = current.parent; // Bir üst parent’a çýk
    }

    Debug.LogWarning("UI içeren bir parent bulunamadý.");
}

}