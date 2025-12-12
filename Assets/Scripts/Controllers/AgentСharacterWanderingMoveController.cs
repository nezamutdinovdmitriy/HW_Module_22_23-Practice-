using UnityEngine;

public class AgentСharacterWanderingMoveController : Controller
{
    private const float DeathZone = 0.05f;

    private readonly AgentCharacter _character;
    private readonly float _timeToWanderStart;
    private readonly float _wanderingAreaRadius;

    private float _currentTime;
    private Vector3 _nextPositionToMove;

    public AgentСharacterWanderingMoveController(AgentCharacter character, float timeToWanderStart, float wanderingAreaRadius)
    {
        _character = character;
        _timeToWanderStart = timeToWanderStart;
        _wanderingAreaRadius = wanderingAreaRadius;
    }

    public bool IsIdle => _currentTime >= _timeToWanderStart;

    protected override void UpdateLogic(float deltaTime)
    {
        if (_character.CurrentVelocity.magnitude <= DeathZone)
            _currentTime += Time.deltaTime;
        else
            _currentTime = 0;

        if (IsIdle)
        {
            if (_character.CurrentVelocity.magnitude <= DeathZone)
            {
                _nextPositionToMove = NavMeshUtils.GetRandomPointOnNavMesh(_character.Position, _wanderingAreaRadius);

                _character.SetDestination(_nextPositionToMove);
            }
        }
    }
}
