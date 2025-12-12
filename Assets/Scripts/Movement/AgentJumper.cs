using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AgentJumper
{
    private readonly NavMeshAgent _agent;
    
    private readonly float _jumpSpeed;
    private readonly float _rotationSpeed;

    private readonly MonoBehaviour _coroutineRunner;
    private readonly AnimationCurve _yOffsetCurve;

    private Coroutine _jumpProcess;

    public AgentJumper(NavMeshAgent agent, float jumpSpeed, float rotationSpeed, MonoBehaviour coroutineRunner, AnimationCurve yOffsetCurve)
    {
        _agent = agent;
        _jumpSpeed = jumpSpeed;
        _rotationSpeed = rotationSpeed;
        _coroutineRunner = coroutineRunner;
        _yOffsetCurve = yOffsetCurve;
    }

    public bool InProcessJump => _jumpProcess != null;

    public void Jump(OffMeshLinkData offMeshLinkData)
    {
        if (InProcessJump)
            return;

        _jumpProcess = _coroutineRunner.StartCoroutine(JumpProcess(offMeshLinkData));
    }

    private IEnumerator JumpProcess(OffMeshLinkData offMeshLinkData)
    {
        Vector3 startLinkPosition = offMeshLinkData.startPos;
        Vector3 endLinkPosition = offMeshLinkData.endPos;
        Vector3 finalDestination = _agent.destination;

        float linkLenght = (endLinkPosition - startLinkPosition).magnitude;

        Vector3 directionalToDestination = finalDestination - startLinkPosition;
        Vector3 normalizedDirection = directionalToDestination.normalized;

        Vector3 targetJumpVector = normalizedDirection * linkLenght;
        Vector3 targetLerpPosition = startLinkPosition + targetJumpVector;

        float duration = linkLenght / _jumpSpeed;
        float progress = 0f;

        while (progress < duration)
        {
            float yOffset = _yOffsetCurve.Evaluate(progress / duration);

            Vector3 horizontalPosition = Vector3.Lerp(startLinkPosition, targetLerpPosition, progress / duration);

            _agent.transform.position = horizontalPosition + Vector3.up * yOffset;

            Vector3 lookDirection = finalDestination - _agent.transform.position;
            lookDirection.y = 0f;

            if(lookDirection.sqrMagnitude > 0)
            {
                Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
                _agent.transform.rotation = Quaternion.Lerp(_agent.transform.rotation, targetRotation, _rotationSpeed);
            }

            progress += Time.deltaTime;

            yield return null;
        }

        _agent.CompleteOffMeshLink();

        _jumpProcess = null;
    }
}
