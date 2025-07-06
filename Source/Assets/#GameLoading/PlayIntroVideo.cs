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
        videoPlayer.targetTexture = new RenderTexture(Screen.width, Screen.height, 0);
        rawImage.texture = videoPlayer.targetTexture;
        videoPlayer.Play();

        // Video bitince ana menüye geç
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        SceneManager.LoadScene(nextScene);
    }
}