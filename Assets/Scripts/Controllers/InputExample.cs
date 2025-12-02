using UnityEngine;
using UnityEngine.AI;

public class InputExample : MonoBehaviour
{
    [SerializeField] private Character _character;
    [SerializeField] private Character _enemy;

    private Controller _characterController;
    private Controller _enemyController;

    private NavMeshPath _path;

    private void Awake()
    {
        _path = new NavMeshPath();

        _characterController = new CompositeController(
            new PlayerDirectionalMovableController(_character),
            new PlayerDirectionalRotatableController(_character));

        _characterController.Enable();
    }

    private void Update()
    {
        _characterController.Update(Time.deltaTime);
    }

    private void OnDrawGizmosSelected()
    {
        NavMeshQueryFilter queryFilter = new();
        queryFilter.agentTypeID = 0;
        queryFilter.areaMask = NavMesh.AllAreas;

        NavMesh.CalculatePath(_enemy.transform.position, _character.transform.position, queryFilter, _path);

        Gizmos.color = Color.yellow;

        if (_path.status != NavMeshPathStatus.PathInvalid)
            foreach (Vector3 corner in _path.corners)
                Gizmos.DrawSphere(corner, 0.5f);
    }
}
