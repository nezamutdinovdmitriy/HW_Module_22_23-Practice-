using UnityEngine;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour, IInitializable
{
    [SerializeField] private AudioMixer _mixer;
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _SFXSource;

    private AudioHandler _handler;

    public void Initialize()
    {
        _handler = new AudioHandler(_mixer);
    }

    public void ToggleMusic()
    {
        if (_handler.IsMusicOn())
            _handler.OffMusic();
        else
            _handler.OnMusic();
    }

    public void ToggleSFX()
    {
        if (_handler.IsSFXOn())
            _handler.OffSFX();
        else
            _handler.OnSFX();
    }

    public void PlayClip(AudioClip audioClip, AudioType type)
    {
        switch (type)
        {
            case AudioType.Music:
                _musicSource.PlayOneShot(audioClip);
                break;
            case AudioType.Sfx:
                _SFXSource.PlayOneShot(audioClip);
                break;
        }
    }
}
