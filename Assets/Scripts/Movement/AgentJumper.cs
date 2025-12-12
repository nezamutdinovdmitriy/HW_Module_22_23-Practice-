using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AgentJumper
{
    private readonly NavMeshAgent _agent;
    private readonly float _speedJump;

    private readonly MonoBehaviour _coroutineRunner;
    private readonly AnimationCurve _yOffsetCurve;

    private Coroutine _jumpProcess;

    public AgentJumper(NavMeshAgent agent, float speedJump, MonoBehaviour coroutineRunner, AnimationCurve yOffsetCurve)
    {
        _agent = agent;
        _speedJump = speedJump;
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

        float duration = (endLinkPosition - startLinkPosition).magnitude / _speedJump;

        float progress = 0f;

        while (progress < duration)
        {
            float yOffset = _yOffsetCurve.Evaluate(progress / duration);

            _agent.transform.position = Vector3.Lerp(startLinkPosition, endLinkPosition, progress / duration) + Vector3.up * yOffset;
            progress += Time.deltaTime;

            yield return null;
        }

        _agent.CompleteOffMeshLink();

        _jumpProcess = null;
    }
}
