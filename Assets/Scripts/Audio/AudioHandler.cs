using UnityEngine;
using UnityEngine.Audio;

public class AudioHandler
{
    private const float OffVolumeValue = -80f;
    private const float OnVolumeValue = 0f;

    private const string MusicKey = "MusicVolume";
    private const string SFXKey = "SFXVolume";

    private readonly AudioMixer _mixer;

    public AudioHandler(AudioMixer mixer)
    {
        _mixer = mixer;
    }

    public bool IsMusicOn() => IsVolumeOn(MusicKey);
    
    public bool IsSFXOn() => IsVolumeOn(SFXKey);

    public void OffMusic() => OffVolume(MusicKey);

    public void OnMusic() => OnVolume(MusicKey);

    public void OffSFX() => OffVolume(SFXKey);

    public void OnSFX() => OnVolume(SFXKey);


    private bool IsVolumeOn(string mixerKey) => _mixer.GetFloat(mixerKey, out float volume) && Mathf.Abs(volume - OffVolumeValue) >= 0.01f;

    private void OnVolume(string mixerKey) => _mixer.SetFloat(mixerKey, OnVolumeValue);

    private void OffVolume(string mixerKey) => _mixer.SetFloat(mixerKey, OffVolumeValue);
}
