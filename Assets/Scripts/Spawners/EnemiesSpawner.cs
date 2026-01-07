using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemiesSpawner : MonoBehaviour
{
    [SerializeField] private AgentCharacter _prefab;
    [SerializeField] private float _radius;
    [SerializeField] private int _count;

    private List<Controller> _controllers = new();

    private ControllersUpdateService _controllersUpdateService;

    public void Initialize(ControllersUpdateService controllersUpdateService)
    {
        _controllersUpdateService = controllersUpdateService;
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

            //Controller controller создание контроллера который будет отвечать за логику врага

            //controller.Enable();

            //_controllers.Add(controller);
        }
    }

    private void Update()
    {
        foreach (Controller controller in _controllers)
            controller.Update(Time.deltaTime);
    }
}
