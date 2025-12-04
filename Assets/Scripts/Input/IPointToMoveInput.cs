using UnityEngine;

public interface IPointToMoveInput
{
    public bool TryGetPoint(out Vector3 hitPoint);
}
