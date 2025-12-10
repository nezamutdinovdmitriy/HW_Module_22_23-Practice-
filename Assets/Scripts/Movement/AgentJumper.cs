using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AgentJumper
{
    private NavMeshAgent _agent;
    private float _speedJump;

    private MonoBehaviour _coroutineRunner;

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
        Vector3 startPosition = offMeshLinkData.startPos;
        Vector3 endPosition = offMeshLinkData.endPos;

        float duration = (endPosition - startPosition).magnitude / _speedJump;

        float progress = 0f;

        while (progress < duration)
        {
            _agent.transform.position = Vector3.Lerp(startPosition, endPosition, progress / duration);
            progress += Time.deltaTime;

            yield return null;
        }

        _agent.CompleteOffMeshLink();

        _jumpProcess = null;
    }
}
