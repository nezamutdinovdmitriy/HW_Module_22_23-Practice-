using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AgentJumper
{
    private readonly NavMeshAgent _agent;
    private readonly float _speedJump;

    private readonly MonoBehaviour _coroutineRunner;

    private Coroutine _jumpProcess;

    public AgentJumper(NavMeshAgent agent, float speedJump, MonoBehaviour coroutineRunner)
    {
        _agent = agent;
        _speedJump = speedJump;
        _coroutineRunner = coroutineRunner;
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
            _agent.transform.position = Vector3.Lerp(startLinkPosition, endLinkPosition, progress / duration);
            progress += Time.deltaTime;

            yield return null;
        }

        _agent.CompleteOffMeshLink();

        _jumpProcess = null;
    }
}
