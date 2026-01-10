using Cinemachine;
using UnityEngine;

public class MainHeroFactory
{
    private ControllersUpdateService _controllersUpdateService;

    private ControllersFactory _controllersFactory;
    private CharactersFactory _charactersFactory;

    public MainHeroFactory(ControllersUpdateService controllersUpdateService, ControllersFactory controllersFactory, CharactersFactory charactersFactory)
    {
        _controllersUpdateService = controllersUpdateService;
        _controllersFactory = controllersFactory;
        _charactersFactory = charactersFactory;
    }

    public AgentCharacter CreateAgentMainHero(MainHeroConfig config,
        Vector3 spawnPosition,
        IPointToMoveInput moveInput, 
        ISelectedPositionView pointView,
        LayerMask ground,
        GameObject pointToMovePrefab
        )
    {
        AgentCharacter instance = _charactersFactory.CreateAgentCharacter(
            config.Prefab,
            spawnPosition,
            config.MoveSpeed,
            config.RotationSpeed,
            config.JumpSpeed,
            config.JumpCurve,
            config.MaxHealth);

        CinemachineVirtualCamera followCameraPrefab = Resources.Load<CinemachineVirtualCamera>("Follow Camera");

        CinemachineVirtualCamera followCamera = Object.Instantiate(followCameraPrefab);

        followCamera.Follow = instance.CameraTarget;

        moveInput = new MouseToWorldPointInput(Camera.main, ground);
        pointView = new PointToMoveView(pointToMovePrefab, 1f, instance);

        Controller controller = _controllersFactory.CreateMainHeroController(instance, 15, 50, moveInput, pointView);

        controller.Enable();

        _controllersUpdateService.Add(controller);

        return instance;
    }
}
