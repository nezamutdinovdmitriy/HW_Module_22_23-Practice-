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

    public AgentCharacter Spawn()
    {
        AgentCharacter instance = Instantiate(_prefab, _spawnPoint.position, Quaternion.identity, null);

        instance.Initialize();

        _followCamera.Follow = instance.CameraTarget;

        _moveInput = new MouseToWorldPointInput(Camera.main, _ground);
        _pointView = new PointToMoveView(_pointToMovePrefab, 1f, this);

        _controller = new CompositeController(
           new MovementBehaviorStateController(
               new AgentCharacterPointToMoveController(instance, _moveInput, _pointView),
               new AgentСharacterWanderingMoveController(instance, 30f, 15f)),
           new AlongMovableVelocityRotatableController(instance, instance, instance));

        _controller.Enable();

        return instance;
    }

    private void Update()
    {
        _controller.Update(Time.deltaTime);
    }
}
