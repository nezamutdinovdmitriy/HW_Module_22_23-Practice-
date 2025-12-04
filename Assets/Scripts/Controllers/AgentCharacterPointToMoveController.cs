using UnityEngine;
using UnityEngine.AI;

public class AgentCharacterPointToMoveController : Controller
{
    private AgentCharacter _character;
    private NavMeshPath _pathToTarget = new NavMeshPath();
    private IPointToMoveInput _moveInput;
    private ISelectedPosition _pointView;
    private LayerMask _ground;

    public AgentCharacterPointToMoveController(AgentCharacter character, IPointToMoveInput moveInput, ISelectedPosition pointView, LayerMask ground)
    {
        _character = character;
        _moveInput = moveInput;
        _pointView = pointView;
        _ground = ground;
    }

    protected override void UpdateLogic(float deltaTime)
    {
        if (_moveInput.TryGetPoint(out Vector3 hitPoint))
        {
            _pointView.SelectPosition(hitPoint);
            _character.SetDestination(hitPoint);
        }
    }
}
