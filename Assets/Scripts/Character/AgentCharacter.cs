using UnityEngine;
using UnityEngine.AI;

public class AgentCharacter : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Transform _target;

    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _moveSpeed;

    private AgentMover _mover;
    private DirectionalRotator _rotator;

    private void Awake()
    {
        _agent.updateRotation = false;

        _mover = new AgentMover(_agent, _moveSpeed);
        _rotator = new DirectionalRotator(transform, _rotationSpeed);
    }

    public Vector3 CurrentVelocity => _mover.CurrentVelocity;
    public float MoveSpeed => _moveSpeed;

    private void Update()
    {
        _rotator.Update(Time.deltaTime);
    }

    public void StopMove() => _mover.Stop();

    public void ResumeMove() => _mover.Resume();

    public void SetDestination(Vector3 position) => _mover.SetDestination(position);

    public void SetRotationDirection(Vector3 inputDirection) => _rotator.SetInputDirection(inputDirection);

    public bool TryGetPath(Vector3 targetPosition, NavMeshPath pathToTarget) => NavMeshUtils.TryGetPath(_agent, targetPosition, pathToTarget);
}
