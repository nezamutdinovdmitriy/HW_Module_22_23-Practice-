using UnityEngine;

public class AgentCharacterPointToMoveController : Controller
{
    private AgentCharacter _character;
    private IPointToMoveInput _moveInput;
    private ISelectedPosition _pointToMoveView;
    private LayerMask _ground;

    public AgentCharacterPointToMoveController(AgentCharacter character, IPointToMoveInput moveInput, ISelectedPosition pointView, LayerMask ground)
    {
        _character = character;
        _moveInput = moveInput;
        _pointToMoveView = pointView;
        _ground = ground;
    }

    protected override void UpdateLogic(float deltaTime)
    {
        if (_moveInput.TryGetPoint(out Vector3 hitPoint))
        {
            _pointToMoveView.SelectPosition(hitPoint);
            _character.SetDestination(hitPoint);
        }
    }
}
