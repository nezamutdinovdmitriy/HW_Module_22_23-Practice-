using System;
using UnityEngine;
using UnityEngine.AI;
using Object = UnityEngine.Object;

public class CharactersFactory
{
    public AgentCharacter CreateAgentCharacter(
        AgentCharacter prefab,
        Vector3 spawnPosition,
        float moveSpeed,
        float rotationSpeed,
        float jumpSpeed,
        AnimationCurve jumpCurve,
        float maxHealth)
    {
        AgentCharacter instance = Object.Instantiate(prefab, spawnPosition, Quaternion.identity, null);

        NavMeshAgent agent;

        if (instance.TryGetComponent(out agent) == false)
            throw new InvalidOperationException("Not found agent component");

        agent.updateRotation = false;

        AgentMover agentMover = new AgentMover(agent, moveSpeed);
        DirectionalRotator agentRotator = new DirectionalRotator(instance.transform, rotationSpeed);
        AgentJumper agentJumper = new AgentJumper(agent, jumpSpeed, rotationSpeed, instance, jumpCurve);

        instance.Initialize(agent, agentMover, agentRotator, agentJumper, maxHealth);

        return instance;
    }
}
