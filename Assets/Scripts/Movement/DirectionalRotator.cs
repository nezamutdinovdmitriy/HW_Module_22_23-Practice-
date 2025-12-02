using UnityEngine;

public class DirectionalRotator
{
    private const float DeathZone = 0.05f;

    private Transform _transform;
    private float _speed;
    private Vector3 _currentDirection;

    public DirectionalRotator(Transform transform, float speed)
    {
        _transform = transform;
        _speed = speed;
    }

    public Quaternion CurrentRotation => _transform.rotation;

    public void SetInputDirection(Vector3 direction) => _currentDirection = direction;

    public void Update(float deltaTime)
    {
        if (_currentDirection.sqrMagnitude < DeathZone)
            return;

        Quaternion lookRotaton = Quaternion.LookRotation(_currentDirection.normalized);

        float step = _speed * deltaTime;

        _transform.rotation = Quaternion.RotateTowards(_transform.rotation, lookRotaton, step);
    }
}
