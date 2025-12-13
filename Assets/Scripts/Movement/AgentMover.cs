using UnityEngine;
using UnityEngine.AI;

public class AgentMover
{
    private readonly NavMeshAgent _agent;

    public Vector3 CurrentVelocity
    {
        get
        {
            if (_agent == null)
                return Vector3.zero;

            return _agent.desiredVelocity;
        }
    }

    public AgentMover(NavMeshAgent agent, float movementSpeed)
    {
        _agent = agent;
        _agent.speed = movementSpeed;
        _agent.acceleration = 999;
    }

    public void SetDestination(Vector3 position)
    {
        if (_agent != null)
            _agent.SetDestination(position);
    }

    public void Stop()
    {
        if (_agent != null)
            _agent.isStopped = true;
    }

    public void Resume()
    {
        if (_agent != null)
            _agent.isStopped = false;
    }
}
