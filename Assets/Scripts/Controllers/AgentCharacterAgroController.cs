using UnityEngine;
using UnityEngine.AI;

public class AgentCharacterAgroController : Controller
{
    private AgentCharacter _character;
    private Transform _target;
    private NavMeshPath _pathToTarget = new NavMeshPath();

    private float _agroRange;
    private float _minDistanceToTarget;
    private float _idleTimer;
    private float _timeForIdle;

    public AgentCharacterAgroController(AgentCharacter character, Transform target, float agroRange, float minDistanceToTarget, float timeForIdle)
    {
        _character = character;
        _target = target;
        _agroRange = agroRange;
        _minDistanceToTarget = minDistanceToTarget;
        _timeForIdle = timeForIdle;
    }

    protected override void UpdateLogic(float deltaTime)
    {
        _idleTimer -= deltaTime;

        _character.SetRotationDirection(_character.CurrentVelocity);

        if(_character.TryGetPath(_target.position, _pathToTarget))
        {
            float distanceToTarget = NavMeshUtils.GetPathLength(_pathToTarget);

            if (IsTargetReached(distanceToTarget))
                _idleTimer = _timeForIdle;

            if (InAgroRagne(distanceToTarget)
                && IdleTimerIsUp())
            {
                _character.ResumeMove();
                _character.SetDestination(_target.position);
                return;
            }
        }

        _character.StopMove();
    }

    private bool IsTargetReached(float distanceToTarget) => distanceToTarget <= _minDistanceToTarget;
    private bool InAgroRagne(float distanceToTarget) => distanceToTarget <= _agroRange;
    private bool IdleTimerIsUp() => _idleTimer <= 0;
}
