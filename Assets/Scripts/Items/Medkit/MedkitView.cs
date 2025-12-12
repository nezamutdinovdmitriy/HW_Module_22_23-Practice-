using UnityEngine;

public class MedkitView : MonoBehaviour
{
    [SerializeField] private Medkit _medkitLogic;
    [SerializeField] private ParticleSystem _useEffectPrefab;

    [SerializeField] private AudioClip _healClip;
    [SerializeField] private AudioController _audioController;

    public void Initialize(AudioController audioController) => _audioController = audioController;

    private void Update()
    {
        if (_medkitLogic.HasUsed)
        {
            Instantiate(_useEffectPrefab, _medkitLogic.Target.position, Quaternion.identity, _medkitLogic.Target);

            _audioController.PlayClip(_healClip, AudioType.Sfx);

            Destroy(_medkitLogic.gameObject);
        }
    }
}
