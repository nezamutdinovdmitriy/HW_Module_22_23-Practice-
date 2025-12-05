using UnityEngine;

public class PointToMoveView : ISelectedPosition
{
    private readonly GameObject _pointViewPrefab;
    private readonly float _destroyTimer;

    private GameObject _marker;
    public PointToMoveView(GameObject pointViewPrefab, float destroyTimer)
    {
        _pointViewPrefab = pointViewPrefab;
        _destroyTimer = destroyTimer;
    }

    public void SelectPosition(Vector3 position)
    {
        if (_marker != null)
            GameObject.Destroy(_marker);

        _marker = GameObject.Instantiate(_pointViewPrefab, position, Quaternion.identity);

        GameObject.Destroy(_marker, _destroyTimer);
    }
}
