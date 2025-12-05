public class MovementBehaviorStateController : Controller
{
    private readonly AgentCharacterPointToMoveController _pointToMoveController;
    private readonly AgentСharacterWanderingMoveController _wanderingMoveController;

    public MovementBehaviorStateController(AgentCharacterPointToMoveController pointToMoveController, AgentСharacterWanderingMoveController wanderingMoveController)
    {
        _pointToMoveController = pointToMoveController;
        _wanderingMoveController = wanderingMoveController;
    }

    public override void Enable()
    {
        base.Enable();
        _pointToMoveController.Enable();
    }

    protected override void UpdateLogic(float deltaTime)
    {
        _pointToMoveController.Enable();
        _pointToMoveController.Update(deltaTime);

        if (_pointToMoveController.IsActive)
        {
            _wanderingMoveController.Disable();
        }
        else
        {
            _wanderingMoveController.Enable();
            _wanderingMoveController.Update(deltaTime);
        }
    }

    public override void Disable()
    {
        base.Disable();
        _pointToMoveController.Disable();
        _wanderingMoveController.Disable();
    }
}
