using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Ses Kaynaklarý")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Ses Klipleri")]
    public AudioClip mainMenuMusic;
    public AudioClip gameplayMusic;
    public AudioClip buttonClick;
    public AudioClip footstep;

    [Header("Mixer Gruplarý")]
    public AudioMixerGroup musicGroup;
    public AudioMixerGroup sfxGroup;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Ses kaynaklarýný ayarla
            musicSource = gameObject.AddComponent<AudioSource>();
            sfxSource = gameObject.AddComponent<AudioSource>();

            musicSource.outputAudioMixerGroup = musicGroup;
            sfxSource.outputAudioMixerGroup = sfxGroup;

            musicSource.loop = true;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }
}