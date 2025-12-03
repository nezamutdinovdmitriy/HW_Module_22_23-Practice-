using UnityEngine;

public class AgentCharacterView : MonoBehaviour
{
    private readonly int VelocityKey = Animator.StringToHash("VelocityX");

    [SerializeField] private Animator _animator;
    [SerializeField] private AgentCharacter _character;
    [SerializeField] private float smoothTime = 0.25f;

    private float _currentVelocity;
    private float _velocitySmooth;

    private void Update()
    {
        float targetVelocity = Mathf.Clamp01(_character.CurrentVelocity.magnitude / _character.MoveSpeed);

        _currentVelocity = Mathf.SmoothDamp(_currentVelocity, targetVelocity, ref _velocitySmooth, smoothTime);

        if (_currentVelocity < 0.01f)
            _currentVelocity = 0f;

        _animator.SetFloat(VelocityKey, _currentVelocity);
    }
}
