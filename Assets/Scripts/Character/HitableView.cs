using UnityEngine;

public class HitableView : MonoBehaviour, IInitializable
{
    private const float CriticalHealthThreshold = 0.3f;
    private const float MaxLayerWeight = 1f;
    private const float MinLayerWeight = 0f;

    private readonly int _isHitTriggerKey = Animator.StringToHash("Hit");

    [SerializeField] private Animator _animator;
    [SerializeField] private float _hitLayerStep = 0.50f;
    [SerializeField] private float _injuredFadeSpeed = 5.0f;

    private IHealth _healthEntity;
    private float _prevHealthValue;

    private int _injerdLayerIndex;
    private int _hitLayerIndex;

    private bool _isStartedProcessResetLayerWeight;

    private bool _isInit;
    
    private bool IsTakingDamage => _prevHealthValue > _healthEntity.CurrentHealth;

    public void Initialize()
    {
        _healthEntity = GetComponentInParent<IHealth>();

        _injerdLayerIndex = _animator.GetLayerIndex("Injured Layer");
        _hitLayerIndex = _animator.GetLayerIndex("Hit Layer");

        _isInit = true;
    }

    private void Update()
    {
        if (_isInit == false)
            return;

        float targetInjuredWeight = (_healthEntity.CurrentHealth / _healthEntity.MaxHealth > CriticalHealthThreshold) ? MinLayerWeight : MaxLayerWeight;
        float currentInjuredWeight = _animator.GetLayerWeight(_injerdLayerIndex);

        float newInjuredWeight = Mathf.MoveTowards(currentInjuredWeight, targetInjuredWeight, _injuredFadeSpeed * Time.deltaTime);

        if (_healthEntity.CurrentHealth / _healthEntity.MaxHealth > CriticalHealthThreshold)
            _animator.SetLayerWeight(_injerdLayerIndex, newInjuredWeight);
        else
            _animator.SetLayerWeight(_injerdLayerIndex, newInjuredWeight);

        if (_isStartedProcessResetLayerWeight)
        {
            ResetLayerWeight(_hitLayerIndex);

            if (_animator.GetLayerWeight(_hitLayerIndex) <= MinLayerWeight)
            {
                _isStartedProcessResetLayerWeight = false;
            }
        }
    }

    private void LateUpdate()
    {
        if (_isInit == false)
            return;

        if (IsTakingDamage)
        {
            _animator.SetLayerWeight(_hitLayerIndex, MaxLayerWeight);
            _animator.SetTrigger(_isHitTriggerKey);

            _prevHealthValue = _healthEntity.CurrentHealth;
        }
    }

    public void StartProcessResetLayerWeight() => _isStartedProcessResetLayerWeight = true;
    private void ResetLayerWeight(int layerIndex)
    {
        _animator.SetLayerWeight(layerIndex, _animator.GetLayerWeight(layerIndex) - _hitLayerStep * Time.deltaTime);
    }
}
