using UnityEngine;

public class MovableView : MonoBehaviour, IInitializable
{
    private readonly int _velocityKey = Animator.StringToHash("VelocityX");

    [SerializeField] private Animator _animator;
    [SerializeField] private float _smoothTime = 0.25f;

    private IMovable _movableEntity;

    private float _velocitySmooth;

    private bool _isInit;


    public void Initialize()
    {
        _movableEntity = GetComponentInParent<IMovable>();

        _isInit = true;
    }

    public void Update()
    {
        if (_isInit == false)
            return;

        float targetVelocity = Mathf.Clamp01(_movableEntity.CurrentVelocity.magnitude / _movableEntity.MoveSpeed);

        float currentVelocity = _movableEntity.CurrentVelocity.magnitude;

        currentVelocity = Mathf.SmoothDamp(currentVelocity, targetVelocity, ref _velocitySmooth, _smoothTime);

        if (currentVelocity < 0.01f)
            currentVelocity = 0f;

        _animator.SetFloat(_velocityKey, currentVelocity);
    }
}
