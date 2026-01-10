using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemiesSpawner : MonoBehaviour
{
    [SerializeField] private AgentEnemyConfig _config;
    [SerializeField] private float _radius;
    [SerializeField] private int _count;

    private EnemiesFactory _enemiesFactory;

    public void Initialize(EnemiesFactory enemiesFactory)
    {
        _enemiesFactory = enemiesFactory;
    }

    public void Spawn(Transform target)
    {
        Vector3 positionAroundTarget;
        NavMeshHit spawnPosition;

        NavMeshQueryFilter queryFilter = new NavMeshQueryFilter();
        queryFilter.agentTypeID = 0;
        queryFilter.areaMask = 1;

        for (int i = 0; i < _count; i++)
        {
            do
            {
                Vector2 randomPositionCircle = Random.insideUnitCircle * _radius;

                Vector3 offset = new Vector3(randomPositionCircle.x, 0, randomPositionCircle.y);

                positionAroundTarget = target.position + offset;

            } while (NavMesh.SamplePosition(positionAroundTarget, out spawnPosition, 0.1f, queryFilter) == false);

            _enemiesFactory.CreateAgentEnemy(_config, spawnPosition.position, target);
        }
    }
}
