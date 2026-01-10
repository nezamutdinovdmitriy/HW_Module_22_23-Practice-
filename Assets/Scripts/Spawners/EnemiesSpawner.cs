using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemiesSpawner : MonoBehaviour
{
    [SerializeField] private AgentCharacter _prefab;
    [SerializeField] private float _radius;
    [SerializeField] private int _count;

    private ControllersUpdateService _controllersUpdateService;
    private ControllersFactory _controllersFactory;

    public void Initialize(ControllersUpdateService controllersUpdateService, ControllersFactory controllersFactory)
    {
        _controllersUpdateService = controllersUpdateService;
        _controllersFactory = controllersFactory;
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

            AgentCharacter instance = Instantiate(_prefab, spawnPosition.position, Quaternion.identity, null);

            instance.Initialize();

            Controller controller = _controllersFactory.CreateAgentCharacterAgroController(instance, target, 5, 3, 1);

            controller.Enable();

            _controllersUpdateService.Add(controller);
        }
    }
}
