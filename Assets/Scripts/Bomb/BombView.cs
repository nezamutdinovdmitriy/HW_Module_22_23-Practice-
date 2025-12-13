using UnityEngine;

public class BombView : MonoBehaviour
{
    private const float BaseExplosionRadius = 6f;
    private const float BaseParticleScale = 1f;

    private readonly float _scalingFactor = BaseParticleScale / BaseExplosionRadius;

    [SerializeField] private BombLogic _bombLogic;
    [SerializeField] private ParticleSystem _explosionEffectPrefab;

    [SerializeField] private Material _activatedBomb;

    [SerializeField] private AudioClip _explosionEffectClip;
    [SerializeField] private AudioController _audioController;

    private Vector3 _newParticleScale;
    private bool _activeMaterialApplied = false;
    private MeshRenderer _meshRenderer;

    private void Awake()
    {
        _newParticleScale = _bombLogic.ExplosionRadius * _scalingFactor * Vector3.one;
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        if (_bombLogic.IsActive && _activeMaterialApplied == false)
        {
            _meshRenderer.material = _activatedBomb;
            _activeMaterialApplied = true;
        }

        if (_bombLogic.HasExploded)
        {
            ParticleSystem explosionEffect = Instantiate(_explosionEffectPrefab, transform.position, Quaternion.identity, null);

            explosionEffect.transform.localScale = _newParticleScale;

            _audioController.PlayClip(_explosionEffectClip, AudioType.Sfx);

            Destroy(_bombLogic.gameObject);
        }
    }
}
