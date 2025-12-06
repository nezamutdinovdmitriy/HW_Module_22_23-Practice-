using UnityEngine;

public class AgentCharacterView : MonoBehaviour
{
    private const float CriticalHealthThreshold = 0.3f;

    private readonly int _velocityKey = Animator.StringToHash("VelocityX");
    private readonly int _isAliveKey = Animator.StringToHash("IsAlive");
    private readonly int _isHitTriggerKey = Animator.StringToHash("Hit");

    [SerializeField] private Animator _animator;
    [SerializeField] private AgentCharacter _character;
    [SerializeField] private float smoothTime = 0.25f;
    [SerializeField] private float _injuredFadeSpeed = 5.0f;
    [SerializeField] private float _hitLayerStep = 0.50f;

    private float _currentVelocity;
    private float _velocitySmooth;

    private int _injerdLayerIndex;
    private int _hitLayerIndex;

    private float _prevHealthValue;
    private bool _isStartedProcessResetLayerWeight;

    private bool IsTakingDamage => _prevHealthValue > _character.CurrentHealth;

    private void Start()
    {
        _prevHealthValue = _character.CurrentHealth;

        _injerdLayerIndex = _animator.GetLayerIndex("Injured Layer");
        _hitLayerIndex = _animator.GetLayerIndex("Hit Layer");
    }

    private void Update()
    {
        float targetVelocity = Mathf.Clamp01(_character.CurrentVelocity.magnitude / _character.MoveSpeed);

        _currentVelocity = Mathf.SmoothDamp(_currentVelocity, targetVelocity, ref _velocitySmooth, smoothTime);

        if (_currentVelocity < 0.01f)
            _currentVelocity = 0f;

        float targetInjuredWeight = (_character.CurrentHealth / _character.MaxHealth > CriticalHealthThreshold) ? 0f : 1f;
        float currentInjuredWeight = _animator.GetLayerWeight(_injerdLayerIndex);

        float newInjuredWeight = Mathf.MoveTowards(currentInjuredWeight, targetInjuredWeight, _injuredFadeSpeed * Time.deltaTime);

        if (_character.CurrentHealth / _character.MaxHealth > CriticalHealthThreshold)
            _animator.SetLayerWeight(_injerdLayerIndex, newInjuredWeight);
        else
            _animator.SetLayerWeight(_injerdLayerIndex, newInjuredWeight);

        _animator.SetFloat(_velocityKey, _currentVelocity);
        _animator.SetBool(_isAliveKey, _character.IsAlive);

        if (_isStartedProcessResetLayerWeight)
        {
            ResetLayerWeight(_hitLayerIndex);

            if(_animator.GetLayerWeight(_hitLayerIndex) <= 0)
            {
                _isStartedProcessResetLayerWeight = false;
            }
        }
    }

    private void LateUpdate()
    {
        if (IsTakingDamage)
        {
            _animator.SetLayerWeight(_hitLayerIndex, 1);
            _animator.SetTrigger(_isHitTriggerKey);
        }

        _prevHealthValue = _character.CurrentHealth;
    }

    public void StartProcessResetLayerWeight() => _isStartedProcessResetLayerWeight = true;

    private void ResetLayerWeight(int layerIndex)
    {
        _animator.SetLayerWeight(layerIndex, _animator.GetLayerWeight(layerIndex) - _hitLayerStep * Time.deltaTime);
    }
}
