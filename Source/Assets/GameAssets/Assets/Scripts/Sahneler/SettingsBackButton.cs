using UnityEngine;

public class SettingsBackButton : MonoBehaviour
{
    public void OnBackClicked()
    {
        // Ana menüye dön
        SceneLoader.Instance.LoadScene("MainMenu_1");
    }
}