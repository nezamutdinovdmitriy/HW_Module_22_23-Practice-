using UnityEngine;

public interface IDirectionalMovable
{
    public Vector3 CurrentVelocity { get; }
    public void SetMoveDirection(Vector3 inputDirection);
}
