using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayIntroVideo : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public RawImage rawImage;
    public string nextScene = "MainMenu_1";  // Geçilecek sahne adý

    void Start()
    {
        // Fare imlecini baþlangýçta gizle ve kilitle
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        videoPlayer.targetTexture = new RenderTexture(Screen.width, Screen.height, 0);
        rawImage.texture = videoPlayer.targetTexture;
        videoPlayer.Play();

        // Video bitince ana menüye geç
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        // Fare imlecini tekrar göster ve kilidi aç
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        SceneManager.LoadScene(nextScene);
    }

    void OnDestroy()
    {
        // Script destroy edilirken temizlik yap
        videoPlayer.loopPointReached -= OnVideoEnd;
    }

    void OnApplicationQuit()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

}