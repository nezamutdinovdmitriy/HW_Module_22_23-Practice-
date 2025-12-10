public class AlongMovableVelocityRotatableController : Controller
{
    private readonly IDirectionalMovable _movable;
    private readonly IDirectionalRotatable _rotatable;
    private readonly AgentCharacter _character;

    public AlongMovableVelocityRotatableController(IDirectionalMovable movable, IDirectionalRotatable rotatable, AgentCharacter character)
    {
        _movable = movable;
        _rotatable = rotatable;
        _character = character;
    }

    protected override void UpdateLogic(float deltaTime)
    {
        if (_character.InJumpProcess)
            return;

        _rotatable.SetRotationDirection(_movable.CurrentVelocity);
    }
}
