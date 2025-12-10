using UnityEngine;

public class DirectionalRotator
{
    private const float DeathZone = 0.05f;
    private readonly Transform _transform;
    private readonly float _speed;

    private Vector3 _currentDirection;

    public DirectionalRotator(Transform transform, float speed)
    {
        _transform = transform;
        _speed = speed;
    }

    public Quaternion CurrentRotation => _transform.rotation;

    public void Update(float deltaTime)
    {
        if (_currentDirection.sqrMagnitude < DeathZone)
            return;

        Quaternion lookRotaton = Quaternion.LookRotation(_currentDirection.normalized);

        float step = _speed * deltaTime;

        _transform.rotation = Quaternion.RotateTowards(_transform.rotation, lookRotaton, step);
    }

    public void SetInputDirection(Vector3 direction)
    {
        direction.y = 0;
        _currentDirection = direction;
    }
}
