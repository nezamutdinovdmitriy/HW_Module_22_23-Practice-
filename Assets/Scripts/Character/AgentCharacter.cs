using UnityEngine;
using UnityEngine.AI;

public class AgentCharacter : MonoBehaviour, IDirectionalRotatable, IDirectionalMovable, IHealth, IDamageable
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _speedJump;
    [SerializeField] private float _maxHealth;

    [SerializeField] AnimationCurve _jumpCurve;

    private AgentMover _mover;
    private DirectionalRotator _rotator;
    private AgentJumper _jumper;

    private void Awake()
    {
        _mover = new AgentMover(_agent, _agent.speed);
        _rotator = new DirectionalRotator(transform, _rotationSpeed);
        _jumper = new AgentJumper(_agent, _speedJump, this, _jumpCurve);

        CurrentHealth = _maxHealth;

        _agent.updateRotation = false;
    }

    public Vector3 CurrentVelocity => _mover.CurrentVelocity;
    public Quaternion CurrentRotation => transform.rotation;
    public Vector3 Position => transform.position;

    public float MoveSpeed => _agent.speed;

    public float MaxHealth => _maxHealth;
    public float CurrentHealth {  get; private set; }
    public bool IsAlive => CurrentHealth > 0;

    public bool InJumpProcess => _jumper.InProcessJump;

    private void Update()
    {
        if (IsAlive == false)
            return;

        _rotator.Update(Time.deltaTime);
    }

    public void SetDestination(Vector3 position) => _mover.SetDestination(position);

    public void SetRotationDirection(Vector3 inputDirection) => _rotator.SetInputDirection(inputDirection);

    public void SetMoveDirection(Vector3 inputDirection) => _rotator.SetInputDirection(inputDirection);

    public void TakeDamage(float damageAmount)
    {
        CurrentHealth -= damageAmount;

        if(IsAlive == false)
            _mover.Stop();
    }

    public void Heal(float healAmount)
    {
        if (IsAlive == false)
            return;

        CurrentHealth += healAmount;
    }

    public void Jump(OffMeshLinkData offMeshLinkData) => _jumper.Jump(offMeshLinkData);

    public bool IsOnNavMeshLink(out OffMeshLinkData offMeshLinkData)
    {
        if (_agent.isOnOffMeshLink)
        {
            offMeshLinkData = _agent.currentOffMeshLinkData;
            return true;
        }

        offMeshLinkData = default;
        return false;
    }
}
