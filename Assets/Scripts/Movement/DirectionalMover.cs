using UnityEngine;

public class DirectionalMover
{
    private CharacterController _characterController;
    private float _speed;
    private Vector3 _currentDirection;

    public DirectionalMover(CharacterController characterController, float speed)
    {
        _characterController = characterController;
        _speed = speed;
    }

    public Vector3 CurrentVelocity { get; private set; }

    public void SetInputDirection(Vector3 direction) => _currentDirection = direction;

    public void Update(float deltaTime)
    {
        CurrentVelocity = _currentDirection.normalized * _speed;

        _characterController.Move(CurrentVelocity * deltaTime);
    }
}
