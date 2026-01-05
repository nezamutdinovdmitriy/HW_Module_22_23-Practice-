using System.Collections;
using UnityEngine;

public class DeathView : MonoBehaviour, IInitializable
{
    private const string EdgeKey = "_Edge";

    private readonly int _isAliveKey = Animator.StringToHash("IsAlive");

    [SerializeField] private Animator _animator;
    [SerializeField] private SkinnedMeshRenderer _skinRender;
    [SerializeField] private float _dessolveTime = 2f;

    private IHealth _healthEntity;
    private bool _isInit;

    public void Initialize()
    {
        _healthEntity = GetComponentInParent<IHealth>();

        _isInit = true;
    }

    private void Update()
    {
        if (_isInit == false)
            return;

        _animator.SetBool(_isAliveKey, _healthEntity.IsAlive);
    }

    public void ShowDeathEffect() => StartCoroutine(DissolveProcess(_dessolveTime));

    private IEnumerator DissolveProcess(float dessolveTime)
    {
        float process = 0;

        while (process <= dessolveTime)
        {
            foreach (Material material in _skinRender.materials)
                material.SetFloat(EdgeKey, process / dessolveTime);

            process += Time.deltaTime;

            yield return null;
        }

        Destroy(transform.parent.gameObject);
    }
}
