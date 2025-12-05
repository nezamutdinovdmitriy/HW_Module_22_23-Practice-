using UnityEngine;

public class BombView : MonoBehaviour
{
    private const float BaseExplosionRadius = 14f;
    private const float BaseParticleScale = 1.1f;

    private readonly float _scalingFactor = BaseParticleScale / BaseExplosionRadius;

    [SerializeField] private BombLogic _bombLogic;
    [SerializeField] private ParticleSystem _explosionEffectPrefab;

    private Vector3 _newParticleScale;

    private void Awake()
    {
        _newParticleScale = _bombLogic.ExplosionRadius * _scalingFactor * Vector3.one;
    }

    private void Update()
    {
        if (_bombLogic.HasExploded)
        {
            ParticleSystem explosionEffect = Instantiate(_explosionEffectPrefab, transform.position, Quaternion.identity, null);
            explosionEffect.transform.localScale = _newParticleScale;

            Destroy(_bombLogic.gameObject);
        }
    }
}
