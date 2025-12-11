using UnityEngine;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioMixer _mixer;

    private AudioHandler _handler;

    private void Awake()
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
}
