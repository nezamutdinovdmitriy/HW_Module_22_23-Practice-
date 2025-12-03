using UnityEngine;
using UnityEngine.AI;

public class DirectionalMovableAgroController : Controller
{
    private const int MinCornersCountInPathToMove = 1;
    private const int StartCornerIndex = 0;
    private const int TargetCornerIndex = 1;

    private IDirectionalMovable _movable;

    private Transform _target;

    private NavMeshPath _pathToTarget = new();
    private NavMeshQueryFilter _queryFilter;

    private float _agroRange;
    private float _minDistanceToTarget;
    private float _idleTimer;
    private float _timeForIdle;

    public DirectionalMovableAgroController(
        IDirectionalMovable movable,
        Transform target,
        NavMeshQueryFilter queryFilter,
        float agroRange,
        float minDistanceToTarget,
        float timeForIdle)
    {
        _movable = movable;
        _target = target;
        _agroRange = agroRange;
        _minDistanceToTarget = minDistanceToTarget;
        _queryFilter = queryFilter;
        _timeForIdle = timeForIdle;
    }

    protected override void UpdateLogic(float deltaTime)
    {
        _idleTimer -= deltaTime;

        if (NavMeshUtils.TryGetPath(_movable.Position, _target.position, _queryFilter, _pathToTarget))
        {
            float distanceToTarget = NavMeshUtils.GetPathLength(_pathToTarget);

            if (IsTargetReached(distanceToTarget))
                _idleTimer = _timeForIdle;

            if (InAgroRagne(distanceToTarget)
                && EnoughCornersInPath(_pathToTarget)
                && IdleTimerIsUp())
            {
                _movable.SetMoveDirection(_pathToTarget.corners[TargetCornerIndex] - _pathToTarget.corners[StartCornerIndex]);
                return;
            }
        }

        _movable.SetMoveDirection(Vector3.zero);
    }

    private bool IsTargetReached(float distanceToTarget) => distanceToTarget <= _minDistanceToTarget;
    private bool InAgroRagne(float distanceToTarget) => distanceToTarget <= _agroRange;
    private bool EnoughCornersInPath(NavMeshPath pathToTarget) => _pathToTarget.corners.Length >= MinCornersCountInPathToMove;
    private bool IdleTimerIsUp() => _idleTimer <= 0;
}
