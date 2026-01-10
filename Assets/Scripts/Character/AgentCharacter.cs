using UnityEngine;
using UnityEngine.AI;

public class AgentCharacter : MonoDestroyable, IDirectionalRotatable, IDirectionalMovable, IHealth, IDamageable, IHealable, IJumper
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Transform _cameraTarget;
    
    private float _maxHealth;

    private AgentMover _mover;
    private DirectionalRotator _rotator;
    private AgentJumper _jumper;

    private bool _isInit;

    public Vector3 Position => transform.position;
    public Vector3 CurrentVelocity => _mover.CurrentVelocity;
    public Quaternion CurrentRotation => transform.rotation;

    public float MoveSpeed => _agent.speed;

    public bool InJumpProcess => _jumper.InProcessJump;

    public float MaxHealth => _maxHealth;
    public float CurrentHealth { get; private set; }
    public bool IsAlive => CurrentHealth > 0;

    public Transform CameraTarget => _cameraTarget;


    public void Initialize(NavMeshAgent agent, AgentMover mover, DirectionalRotator rotator, AgentJumper jumper, float maxHealth)
    {
        _agent = agent;
        
        _mover = mover;
        _rotator = rotator;
        _jumper = jumper;
        
        _maxHealth = maxHealth;
        CurrentHealth = _maxHealth;

        foreach (IInitializable initializable in GetComponentsInChildren<IInitializable>())
            initializable.Initialize();

        _isInit = true;
    }

    private void Update()
    {
        if (_isInit == false)
            return;

        if (IsAlive == false)
            return;

        _rotator.Update(Time.deltaTime);
    }

    public void TakeDamage(float damageAmount)
    {
        CurrentHealth -= damageAmount;

        if (IsAlive == false)
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

    public void SetDestination(Vector3 position) => _mover.SetDestination(position);
    public void StopMove() => _mover.Stop();
    public void ResumeMove() => _mover.Resume();
    public bool TryGetPath(Vector3 targetPosition, NavMeshPath pathToTarget) => NavMeshUtils.TryGetPath(_agent, targetPosition, pathToTarget);

    public void SetRotationDirection(Vector3 inputDirection) => _rotator.SetInputDirection(inputDirection);
    public void SetMoveDirection(Vector3 inputDirection) => _rotator.SetInputDirection(inputDirection);
}
