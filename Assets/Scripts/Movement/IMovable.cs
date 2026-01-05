using UnityEngine;

public interface IMovable
{
    public Vector3 CurrentVelocity { get; }
    public float MoveSpeed { get; }
}
