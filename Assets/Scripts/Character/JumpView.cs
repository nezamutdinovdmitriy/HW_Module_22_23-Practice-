using UnityEngine;

public class JumpView : MonoBehaviour, IInitializable
{
    private readonly int _isJumpingKey = Animator.StringToHash("IsJumping");

    [SerializeField] private Animator _animator;

    private IJumper _jumperEntity;

    private bool _isInit;

    public void Initialize()
    {
        _jumperEntity = GetComponentInParent<IJumper>();

        _isInit = true;
    }

    private void Update()
    {
        if (_isInit == false)
            return;

        _animator.SetBool(_isJumpingKey, _jumperEntity.InJumpProcess);
    }
}
