using UnityEngine;

public class MedkitView : MonoBehaviour
{
    [SerializeField] private Medkit _medkitLogic;
    [SerializeField] private ParticleSystem _useEffectPrefab;

    private void Update()
    {
        if (_medkitLogic.HasUsed)
        {
            Instantiate(_useEffectPrefab, _medkitLogic.Target.position, Quaternion.identity, _medkitLogic.Target);
            Destroy(_medkitLogic.gameObject);
        }
    }
}
