using UnityEngine;

public class AgentCharacterPointToMoveController : Controller
{
    private readonly AgentCharacter _character;
    private readonly IPointToMoveInput _moveInput;
    private readonly ISelectedPosition _pointToMoveView;
    private LayerMask _ground;

    public AgentCharacterPointToMoveController(AgentCharacter character, IPointToMoveInput moveInput, ISelectedPosition pointView, LayerMask ground)
    {
        _character = character;
        _moveInput = moveInput;
        _pointToMoveView = pointView;
        _ground = ground;
    }

    public bool IsActive { get; private set; }

    protected override void UpdateLogic(float deltaTime)
    {
        if (_moveInput.TryGetPoint(out Vector3 hitPoint))
        {
            _pointToMoveView.SelectPosition(hitPoint);
            _character.SetDestination(hitPoint);

            IsActive = true;
        }

        if (IsActive && _character.CurrentVelocity.magnitude <= 0.05f)
            IsActive = false;
    }

    public override void Disable()
    {
        base.Disable();
        IsActive = false;
    }
}
