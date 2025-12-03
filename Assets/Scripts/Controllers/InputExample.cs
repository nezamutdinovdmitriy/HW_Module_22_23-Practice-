using UnityEngine;
using UnityEngine.AI;

public class InputExample : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private AgentCharacter _agentCharacter;
    [SerializeField] private LayerMask _ground;

    private Controller _agentCharacterController;
    private MousePositionReader _mousePositionReader;
    private MovementClickHandler _movementClickHandler;

    private void Awake()
    {
        _mousePositionReader = new MousePositionReader(_camera);
        _movementClickHandler = new MovementClickHandler();

        _agentCharacterController = new CompositeController(
            new AgentCharacterMovableController(_agentCharacter, _mousePositionReader, _movementClickHandler, 0.05f, _ground),
            new AlongMovableVelocityRotatableController(_agentCharacter, _agentCharacter));
        
        _agentCharacterController.Enable();
    }

    private void Update()
    {
        _agentCharacterController.Update(Time.deltaTime);
    }
}
