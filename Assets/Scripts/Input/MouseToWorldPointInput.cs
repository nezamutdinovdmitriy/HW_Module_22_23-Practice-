using UnityEngine;

public class MouseToWorldPointInput : IPointToMoveInput
{
    private const int _pointToMoveKey = 0;

    private readonly Camera _camera;

    private LayerMask _movementMask;

    public MouseToWorldPointInput(Camera camera, LayerMask movementMask)
    {
        _camera = camera;
        _movementMask = movementMask;
    }

    public bool TryGetPoint(out Vector3 hitPoint)
    {
        if (Input.GetMouseButtonDown(_pointToMoveKey))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, _movementMask))
            {
                hitPoint = hitInfo.point;
                return true;
            }
        }

        hitPoint = Vector3.zero;
        return false;
    }
}
