using UnityEngine;

public interface IDirectionalMovable : ITransformPosition
{
    public Vector3 CurrentVelocity { get; }
    public void SetMoveDirection(Vector3 inputDirection);
}
