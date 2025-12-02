using UnityEngine;

public interface IDirectionalRotatable
{
    public Quaternion CurrentRotation { get; }

    public void SetRotationDirection(Vector3 inputDirection);
}
