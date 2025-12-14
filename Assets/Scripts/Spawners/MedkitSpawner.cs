using System.Collections;
using UnityEngine;

public class MedkitSpawner : MonoBehaviour
{
    [SerializeField] private Medkit _medkitPrefab;

    [SerializeField] private Transform _target;
    [SerializeField] private float _spawnRadiusForTarget;
    [SerializeField] private float _cooldownTime;

    [SerializeField] private AudioController _audioController;

    private bool _isActive;
    private Coroutine _spawnCoroutine;
    private DesktopInput _input;

    public void Initialize(DesktopInput input)
    {
        _input = input;
    }

    private void Update()
    {
        if (_input.ItemSpawnerToggle)
        {
            if (_isActive)
            {
                _isActive = false;

                if (_spawnCoroutine != null)
                    StopCoroutine(_spawnCoroutine);

                Debug.Log("Спавнер аптечек выключен!");
            }
            else
            {
                _isActive = true;
                _spawnCoroutine = StartCoroutine(SpawnProcess(_cooldownTime));

                Debug.Log("Спавнер аптечек включен!");
            }
        }
    }

    private IEnumerator SpawnProcess(float cooldownTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(cooldownTime);

            Vector3 spawnPoint = NavMeshUtils.GetRandomPointOnNavMesh(_target.position, _spawnRadiusForTarget);
            spawnPoint.y = 1f;

            Medkit item = Instantiate(_medkitPrefab, spawnPoint, Quaternion.identity, null);

            MedkitView view = item.GetComponentInChildren<MedkitView>();

            view.Initialize(_audioController);
        }
    }
}
