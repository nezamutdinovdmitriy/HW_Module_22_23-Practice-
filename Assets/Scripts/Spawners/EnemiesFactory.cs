using UnityEngine;

public class EnemiesFactory
{
    private ControllersUpdateService _controllersUpdateService;

    private ControllersFactory _controllersFactory;
    private CharactersFactory _charactersFactory;

    public EnemiesFactory(ControllersUpdateService controllersUpdateService, ControllersFactory controllersFactory, CharactersFactory charactersFactory)
    {
        _controllersUpdateService = controllersUpdateService;
        _controllersFactory = controllersFactory;
        _charactersFactory = charactersFactory;
    }

    public AgentCharacter CreateAgentEnemy(AgentEnemyConfig config, Vector3 spawnPosition, Transform target)
    {
        AgentCharacter instance = _charactersFactory.CreateAgentCharacter(
            config.Prefab,
            spawnPosition,
            config.MoveSpeed,
            config.RotationSpeed,
            config.JumpSpeed,
            config.JumpCurve,
            config.MaxHealth);

        Controller controller = _controllersFactory.CreateAgentCharacterAgroController(instance, target, config.AgroRange, config.MinDistanceToTarget, config.TimeForIdle);

        controller.Enable();

        _controllersUpdateService.Add(controller, () => instance.IsDestroyed);

        return instance;
    }
}
