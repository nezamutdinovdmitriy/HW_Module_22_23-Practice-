using UnityEngine;

public class AgentCharacterView : MonoBehaviour
{
    private const float Step = 0.1f;

    private readonly int _velocityKey = Animator.StringToHash("VelocityX");
    private readonly int _isAliveKey = Animator.StringToHash("IsAlive");
    private readonly int _isHitTriggerKey = Animator.StringToHash("Hit");

    [SerializeField] private Animator _animator;
    [SerializeField] private AgentCharacter _character;
    [SerializeField] private float smoothTime = 0.25f;

    private float _currentVelocity;
    private float _velocitySmooth;

    private int _baseLayerIndex;
    private int _injerdLayerIndex;
    private int _hitLayerIndex;

    private bool _isTakingDamage => _prevHealthValue > _character.CurrentHealth;
    private float _prevHealthValue;

    private void Start()
    {
        _prevHealthValue = _character.CurrentHealth;

        _baseLayerIndex = _animator.GetLayerIndex("Base Layer");
        _injerdLayerIndex = _animator.GetLayerIndex("Injured Layer");
        _hitLayerIndex = _animator.GetLayerIndex("Hit Layer");
    }

    private void Update()
    {
        float targetVelocity = Mathf.Clamp01(_character.CurrentVelocity.magnitude / _character.MoveSpeed);

        _currentVelocity = Mathf.SmoothDamp(_currentVelocity, targetVelocity, ref _velocitySmooth, smoothTime);

        if (_currentVelocity < 0.01f)
            _currentVelocity = 0f;

        if (_character.CurrentHealth / _character.MaxHealth > 0.3f)
            _animator.SetLayerWeight(_injerdLayerIndex, Mathf.Lerp(_animator.GetLayerWeight(_injerdLayerIndex), 0, Step));
        else
            _animator.SetLayerWeight(_injerdLayerIndex, Mathf.Lerp(_animator.GetLayerWeight(_injerdLayerIndex), 1, Step));

        _animator.SetFloat(_velocityKey, _currentVelocity);
        _animator.SetBool(_isAliveKey, _character.IsAlive);
    }

    private void LateUpdate()
    {
        if (_isTakingDamage)
        {
            _animator.SetLayerWeight(_hitLayerIndex, 1);
            _animator.SetTrigger(_isHitTriggerKey);
            //_animator.SetLayerWeight(_hitLayerIndex, Mathf.Lerp(_animator.GetLayerWeight(_hitLayerIndex), 0, Step));
        }

        _prevHealthValue = _character.CurrentHealth;
    }

    public void ResetLayerWeight(int layerIndex)
    {
        _animator.SetLayerWeight(layerIndex, Mathf.Lerp(_animator.GetLayerWeight(layerIndex), 0, Step));
    }
}
