using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataSound", menuName = "GameData/DataSound")]
public class DataSound : ScriptableObject
{
    [SerializeField] private AudioClip bgm;
    [SerializeField] private AudioClip move;
    [SerializeField] private AudioClip attack;
    [SerializeField] private AudioClip die;
    [SerializeField] private AudioClip levelUp;
    [SerializeField] private AudioClip gameOver;
    [SerializeField] private AudioClip collect;
    [SerializeField] private AudioClip uiClick;

    public Dictionary<SoundSource, AudioClip> GetSounds()
    {
        return new Dictionary<SoundSource, AudioClip>
        {
            { SoundSource.BGM, bgm },
            { SoundSource.Move, move },
            { SoundSource.Attack, attack },
            { SoundSource.Die, die },
            { SoundSource.LevelUp, levelUp },
            { SoundSource.GameOver, gameOver },
            { SoundSource.Collect, collect },
            { SoundSource.UIClick, uiClick }
        };
    }
}

public class LoadDataSound : Singleton<LoadDataSound>
{
    private DataSound _dataSound;
    private const string Path = "DataSound";
    private Dictionary<SoundSource, AudioClip> _soundSource = new Dictionary<SoundSource, AudioClip>();

    public LoadDataSound()
    {
        _dataSound = Resources.Load<DataSound>(Path);
        if (_dataSound == null)
        {
            Debug.LogError($"Can't load data sound from path: {Path}");
        }
        else
        {
            _soundSource = _dataSound.GetSounds();
        }
    }

    public AudioClip GetSoundAudioClip(SoundSource source)
    {
        var isFound = _soundSource.TryGetValue(source, out var sound);
        if (!isFound)
        {
            CustomDebug.SetMessage($"Can't find sound with source: {source}", Color.red);
            return null;
        }
        else
        {
            return sound;
        }
    }
}