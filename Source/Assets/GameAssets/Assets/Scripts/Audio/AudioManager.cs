using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public bool IsMusicPlaying { get; private set; }
    private AudioClip currentMusic;

    public static AudioManager Instance;

    [Header("Ses Kaynaklarý")]
    public AudioSource musicSource;
    public AudioSource[] sfxSources; // Dizi olarak deðiþtirildi
    private int currentSfxSource = 0;

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
        // Singleton deseni güncellemesi
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Müzik kaynaðýný otomatik oluþtur
            if (musicSource == null)
            {
                musicSource = gameObject.AddComponent<AudioSource>();
                musicSource.outputAudioMixerGroup = musicGroup;
                musicSource.loop = true;
            }

            // ESKÝ KODU SÝL: sfxSource ile ilgili kýsým tamamen kaldýrýldý
        }
        else
        {
            Destroy(gameObject);
            return; // Yeni eklenen: return ekleyerek aþaðýdaki kodun çalýþmasýný engelle
        }

        // SFX kaynak havuzu oluþtur (5 kaynaklý)
        sfxSources = new AudioSource[5];
        for (int i = 0; i < sfxSources.Length; i++)
        {
            sfxSources[i] = gameObject.AddComponent<AudioSource>();
            sfxSources[i].outputAudioMixerGroup = sfxGroup;
            sfxSources[i].playOnAwake = false;
            sfxSources[i].spatialBlend = 0; // 2D ses için
        }
    }

    public void PlayMusic(AudioClip clip)
    {
        if (clip == null) return;

        // Ayný müzik çalýyorsa yeniden baþlatma
        if (currentMusic == clip && IsMusicPlaying) return;

        musicSource.clip = clip;
        musicSource.Play();
        currentMusic = clip;
        IsMusicPlaying = true;
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip == null) return;

        // Boþ kaynak bul
        currentSfxSource = (currentSfxSource + 1) % sfxSources.Length;
        AudioSource source = sfxSources[currentSfxSource];

        // Çal
        source.clip = clip;
        source.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
        IsMusicPlaying = false;
    }
}