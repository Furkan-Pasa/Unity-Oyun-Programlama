using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{
    void Start()
    {
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(PlayButtonSound);
        }
    }

    void PlayButtonSound()
    {
        if (AudioManager.Instance != null)
        {
            // Debug ekle
            Debug.Log($"Button clicked! Clip: {AudioManager.Instance.buttonClick?.name}");

            AudioManager.Instance.PlaySFX(AudioManager.Instance.buttonClick);
        }
        else
        {
            Debug.LogError("AudioManager instance not found!");
        }
    }
}