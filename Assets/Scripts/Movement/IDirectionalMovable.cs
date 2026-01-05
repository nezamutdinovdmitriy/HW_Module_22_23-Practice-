using UnityEngine;

public interface IDirectionalMovable : ITransformPosition, IMovable
{
    public void SetMoveDirection(Vector3 inputDirection);
}
