using UnityEngine;
using UnityEngine.AI;

public class AgentCharacterMovableController : Controller
{
    private AgentCharacter _character;
    private NavMeshPath _pathToTarget = new NavMeshPath();
    private MousePositionReader _mousePositionReader;
    private MovementClickHandler _movementClickHandler;
    private LayerMask _ground;

    private float _minDistanceToTarget;

    public AgentCharacterMovableController(AgentCharacter character, MousePositionReader mousePositionReader, MovementClickHandler movementClickHandler, float minDistanceToTarget, LayerMask ground)
    {
        _character = character;
        _mousePositionReader = mousePositionReader;
        _movementClickHandler = movementClickHandler;
        _minDistanceToTarget = minDistanceToTarget;
        _ground = ground;
    }

    protected override void UpdateLogic(float deltaTime)
    {
        if (_movementClickHandler.CheckForMovementClick())
        {
            Ray ray = _mousePositionReader.Ray;

            if(Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, _ground))
            {
                Debug.DrawRay(ray.origin, ray.direction * hitInfo.distance, Color.red, 5f);
                GameObject debugSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                debugSphere.transform.position = hitInfo.point;
                debugSphere.transform.localScale = Vector3.one * 0.5f;
                GameObject.Destroy(debugSphere, 1.0f);

                _character.SetDestination(hitInfo.point);
            }
        }
    }

    private bool IsTargetReached(float distanceToTarget) => distanceToTarget <= _minDistanceToTarget;
}
