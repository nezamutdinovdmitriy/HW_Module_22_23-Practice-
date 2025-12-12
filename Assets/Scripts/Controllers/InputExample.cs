using UnityEngine;

public class InputExample : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private AgentCharacter _agentCharacter;
    [SerializeField] private AgentCharacterView _agentCharacterView;
    [SerializeField] private LayerMask _ground;
    [SerializeField] private GameObject _pointToMovePrefab;
    [SerializeField] private MedkitSpawner _medkidSpawner;

    private Controller _agentCharacterController;
    private IPointToMoveInput _moveInput;
    private ISelectedPositionView _pointView;
    private DesktopInput _desktopInput;

    private void Awake()
    {
        _moveInput = new MouseToWorldPointInput(_camera, _ground);
        _pointView = new PointToMoveView(_pointToMovePrefab, 1f, _agentCharacterView);
        _desktopInput = new DesktopInput();

        _agentCharacterController = new CompositeController(
           new MovementBehaviorStateController(
               new AgentCharacterPointToMoveController(_agentCharacter, _moveInput, _pointView),
               new AgentСharacterWanderingMoveController(_agentCharacter, 2f, 15f)),
           new AlongMovableVelocityRotatableController(_agentCharacter, _agentCharacter, _agentCharacter));

        _agentCharacterController.Enable();

        _medkidSpawner.Initialize(_desktopInput);
    }

    private void Update()
    {
        _agentCharacterController.Update(Time.deltaTime);
    }
}
