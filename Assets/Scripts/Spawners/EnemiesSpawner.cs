using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemiesSpawner
{
    private EnemiesFactory _enemiesFactory;

    public EnemiesSpawner(EnemiesFactory enemiesFactory)
    {
        _enemiesFactory = enemiesFactory;
    }

    public List<AgentCharacter> Spawn(AgentEnemyConfig config, Transform target, float radius, float count)
    {
        Vector3 positionAroundTarget;
        NavMeshHit spawnPosition;

        NavMeshQueryFilter queryFilter = new NavMeshQueryFilter();
        queryFilter.agentTypeID = 0;
        queryFilter.areaMask = 1;

        List<AgentCharacter> spawnedEnemies = new List<AgentCharacter>();

        for (int i = 0; i < count; i++)
        {
            do
            {
                Vector2 randomPositionCircle = Random.insideUnitCircle * radius;

                Vector3 offset = new Vector3(randomPositionCircle.x, 0, randomPositionCircle.y);

                positionAroundTarget = target.position + offset;

            } while (NavMesh.SamplePosition(positionAroundTarget, out spawnPosition, 0.1f, queryFilter) == false);

            spawnedEnemies.Add(_enemiesFactory.CreateAgentEnemy(config, spawnPosition.position, target));
        }

        return spawnedEnemies;
    }
}
