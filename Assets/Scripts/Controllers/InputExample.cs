using UnityEngine;
using UnityEngine.AI;

public class InputExample : MonoBehaviour
{
    [SerializeField] private Character _character;
    [SerializeField] private Character _enemy;
    [SerializeField] private AgentCharacter _agentEnemy;

    private Controller _characterController;
    private Controller _enemyController;
    private Controller _agentEnemyController;

    private void Awake()
    {
        _characterController = new CompositeController(
            new PlayerDirectionalMovableController(_character),
            new PlayerDirectionalRotatableController(_character));

        _characterController.Enable();

        NavMeshQueryFilter queryFilter = new();
        queryFilter.agentTypeID = 0;
        queryFilter.areaMask = NavMesh.AllAreas;

        _enemyController = new CompositeController(
            new DirectionalMovableAgroController(_enemy, _character.transform, queryFilter, 30, 2, 1),
            new AlongMovableVelocityRotatableController(_enemy, _enemy));

        _enemyController.Enable();

        _agentEnemyController = new AgentCharacterAgroController(_agentEnemy, _character.transform, 30, 2, 1);
        _agentEnemyController.Enable();
    }

    private void Start()
    {
        _enemy.gameObject.SetActive(false);
    }

    private void Update()
    {
        _characterController.Update(Time.deltaTime);
        _enemyController.Update(Time.deltaTime);
        _agentEnemyController.Update(Time.deltaTime);
    }
}
