using UnityEngine;

public class InputExample : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private AgentCharacter _agentCharacter;
    [SerializeField] private LayerMask _ground;
    [SerializeField] private GameObject _pointToMovePrefab;

    private Controller _agentCharacterController;
    private IPointToMoveInput _moveInput;
    private ISelectedPosition _pointView;

    private void Awake()
    {
        _moveInput = new MouseToWorldPointInput(_camera, _ground);
        _pointView = new PointToMoveView(_pointToMovePrefab, 1f);

        _agentCharacterController = new CompositeController(
            new AgentCharacterPointToMoveController(_agentCharacter, _moveInput, _pointView, _ground),
            new AlongMovableVelocityRotatableController(_agentCharacter, _agentCharacter));
        
        _agentCharacterController.Enable();
    }

    private void Update()
    {
        _agentCharacterController.Update(Time.deltaTime);
    }
}
