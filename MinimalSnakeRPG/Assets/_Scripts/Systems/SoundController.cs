using UnityEngine;

public enum SoundSource
{
    None = 0,
    BGM = 1,
    Move = 2,
    Attack = 3,
    Die = 4,
    LevelUp = 5,
    GameOver = 6,
    Collect = 7,
    UIClick = 8,
}

public class SoundController : MonoBehaviour
{
    public static SoundController instance;
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource sfxSource;

    private void Start()
    {
        PlayBGM(LoadDataSound.Instance.GetSoundAudioClip(SoundSource.BGM));
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void PlayBGM(AudioClip clip)
    {
        bgmSource.clip = clip;
        bgmSource.Play();
    }

    public void PlaySFX(SoundSource source)
    {
        var sound = LoadDataSound.Instance.GetSoundAudioClip(source);
        sfxSource.clip = sound;
        sfxSource.PlayOneShot(sound);
    }
}