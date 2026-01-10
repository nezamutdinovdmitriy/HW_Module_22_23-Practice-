using Cinemachine;
using UnityEngine;

public class MainHeroSpawner : MonoBehaviour
{
    [SerializeField] private AgentCharacter _prefab;
    [SerializeField] private CinemachineVirtualCamera _followCamera;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private LayerMask _ground;
    [SerializeField] private GameObject _pointToMovePrefab;

    private Controller _controller;

    private IPointToMoveInput _moveInput;
    private ISelectedPositionView _pointView;

    private ControllersUpdateService _controllersUpdateService;
    
    private ControllersFactory _controllersFactory;
    private CharactersFactory _charactersFactory;

    public void Initialize(ControllersUpdateService controllersUpdateService, ControllersFactory controllersFactory, CharactersFactory charactersFactory)
    {
        _controllersUpdateService = controllersUpdateService;
        
        _controllersFactory = controllersFactory;
        _charactersFactory = charactersFactory;
    }

    public AgentCharacter Spawn()
    {
        AgentCharacter instance = _charactersFactory.CreateAgentCharacter(
            _prefab, 
            _spawnPoint.position,
            9, 
            900, 
            5, 
            new AnimationCurve(new Keyframe(0,0), new Keyframe(1,0)),
            100);

        _followCamera.Follow = instance.CameraTarget;

        _moveInput = new MouseToWorldPointInput(Camera.main, _ground);
        _pointView = new PointToMoveView(_pointToMovePrefab, 1f, this);

        _controller = _controllersFactory.CreateMainHeroController(instance, 15, 50, _moveInput, _pointView);

        _controller.Enable();

        _controllersUpdateService.Add(_controller);

        return instance;
    }
}
