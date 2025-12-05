using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class BombLogic : MonoBehaviour
{
    private readonly Collider[] targetsArray = new Collider[5];

    [Header("Configuration")]
    [SerializeField, Range(2, 15)] private float _explosionRadius;
    [SerializeField, Range(2, 15)] private float _triggerRadius;
    [SerializeField] private float _damage;
    [SerializeField] private float _timerToExplosion;
    [SerializeField] private LayerMask _mask;

    private SphereCollider _triggerCollider;
    private bool _isActive;
    private float _currentTime;

    public float CurrentTime => _currentTime;
    public float TimeToExplosion => _timerToExplosion;
    public float ExplosionRadius => _explosionRadius;
    public bool HasExploded { get; private set; }

    private void Awake()
    {
        InitializeCollider();
    }

    private void Update()
    {
        if (_isActive)
            _currentTime += Time.deltaTime;

        if (_currentTime >= _timerToExplosion)
            Explode();
    }

    private void OnTriggerEnter(Collider other) => _isActive = true;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, _explosionRadius);

        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(transform.position, _triggerRadius);
    }

    public void Explode()
    {
        int countTargets = Physics.OverlapSphereNonAlloc(transform.position, _explosionRadius, targetsArray, _mask);

        for (int i = 0; i < countTargets; i++)
        {
            Collider target = targetsArray[i];

            if (target.TryGetComponent<IDamageable>(out IDamageable damageable))
                damageable.TakeDamage(_damage);
        }

        System.Array.Clear(targetsArray, 0, countTargets);

        HasExploded = true;
    }

    private void InitializeCollider()
    {
        _triggerCollider = GetComponent<SphereCollider>();
        _triggerCollider.radius = _triggerRadius / transform.localScale.x;
        _triggerCollider.isTrigger = true;
    }
}
