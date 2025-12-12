using System.Collections;
using UnityEngine;

public class PointToMoveView : ISelectedPositionView
{
    private readonly GameObject _pointViewPrefab;
    private readonly MonoBehaviour _coroutineRunner;
    private readonly WaitForSeconds _disappearanceTimer;

    private GameObject _moveMarker;

    private Coroutine _disappearanceCoroutine;

    public PointToMoveView(GameObject pointViewPrefab, float disappearanceTime, MonoBehaviour coroutineRunner)
    {
        _pointViewPrefab = pointViewPrefab;
        _disappearanceTimer = new WaitForSeconds(disappearanceTime);
        _coroutineRunner = coroutineRunner;

        CreateMarker();
    }

    public void SelectPosition(Vector3 position)
    {
        if (_moveMarker.activeSelf == true && _disappearanceCoroutine != null)
        {
            _coroutineRunner.StopCoroutine(_disappearanceCoroutine);
            _moveMarker.SetActive(false);
        }

        _moveMarker.SetActive(true);
        _moveMarker.transform.position = position;

        _disappearanceCoroutine = _coroutineRunner.StartCoroutine(HidePointAfterDelay());
    }

    private IEnumerator HidePointAfterDelay()
    {
        yield return _disappearanceTimer;
        _moveMarker.SetActive(false);
    }

    private void CreateMarker()
    {
        _moveMarker = GameObject.Instantiate(_pointViewPrefab, Vector3.zero, Quaternion.identity, null);
        _moveMarker.SetActive(false);
    }
}
