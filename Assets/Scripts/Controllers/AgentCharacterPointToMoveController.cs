using UnityEngine;

public class AgentCharacterPointToMoveController : Controller
{
    private readonly AgentCharacter _character;
    private readonly IPointToMoveInput _moveInput;
    private readonly ISelectedPosition _pointToMoveView;

    public AgentCharacterPointToMoveController(AgentCharacter character, IPointToMoveInput moveInput, ISelectedPosition pointView)
    {
        _character = character;
        _moveInput = moveInput;
        _pointToMoveView = pointView;
    }

    public bool IsActive { get; private set; }

    public bool IsMoving => _character.CurrentVelocity.magnitude >= 0.05f;

    protected override void UpdateLogic(float deltaTime)
    {
        if (_moveInput.TryGetPoint(out Vector3 hitPoint))
        {
            _pointToMoveView.SelectPosition(hitPoint);
            _character.SetDestination(hitPoint);

            IsActive = true;
        }

        if (IsActive && IsMoving == false)
            IsActive = false;
    }

    public override void Disable()
    {
        base.Disable();
        IsActive = false;
    }
}
