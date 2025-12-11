using System.Collections;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private readonly KeyCode ToggleKey = KeyCode.F;

    [SerializeField] private Item _itemPrefab;

    [SerializeField] private Transform _target;
    [SerializeField] private float _spawnRadiusForTarget;
    [SerializeField] private float _cooldownTime;

    private bool _isActive;
    private Coroutine _spawnCoroutine;

    private void Update()
    {
        if (Input.GetKeyDown(ToggleKey))
        {
            if (_isActive)
            {
                _isActive = false;

                if (_spawnCoroutine != null)
                    StopCoroutine(_spawnCoroutine);
            }
            else
            {
                _isActive = true;
                _spawnCoroutine = StartCoroutine(SpawnProcess(_cooldownTime));
            }
        }
    }

    private IEnumerator SpawnProcess(float cooldownTime)
    {
        while (true)
        {
            Vector3 spawnPoint = NavMeshUtils.GetRandomPointOnNavMesh(_target.position, _spawnRadiusForTarget);

            Instantiate(_itemPrefab, spawnPoint, Quaternion.identity, null);
            yield return new WaitForSeconds(cooldownTime);
        }
    }
}
