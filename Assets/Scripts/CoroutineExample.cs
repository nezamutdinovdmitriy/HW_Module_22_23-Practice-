using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineExample : MonoBehaviour
{
    [SerializeField] private Transform _transform;

    [SerializeField] private List<Transform> _patrolPointsList = new();

    private Queue<Transform> _points = new Queue<Transform>();
    private Transform _targetPoint;
    private float _speed = 0.15f;

    private void Awake()
    {
        foreach (Transform transform in _patrolPointsList)
            _points.Enqueue(transform);

        SwitchPoint();

        StartCoroutine(MoveProcess());
    }

    private void SwitchPoint()
    {
        _targetPoint = _points.Dequeue();
        _points.Enqueue(_targetPoint);
    }

    private IEnumerator MoveProcess()
    {
        while (true)
        {
            Vector3 direction = _targetPoint.position - _transform.position;
            transform.Translate(direction.normalized * _speed);

            if ((_targetPoint.position - _transform.position).magnitude <= 1f)
            {
                yield return new WaitForSeconds(2);
                SwitchPoint();
            }

            yield return null;
        }
    }
}
