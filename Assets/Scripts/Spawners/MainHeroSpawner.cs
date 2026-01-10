using Cinemachine;
using UnityEngine;

public class MainHeroSpawner : MonoBehaviour
{
    [SerializeField] private MainHeroConfig _config;
    [SerializeField] private CinemachineVirtualCamera _followCamera;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private LayerMask _ground;
    [SerializeField] private GameObject _pointToMovePrefab;

    private IPointToMoveInput _moveInput;
    private ISelectedPositionView _pointView;

    private MainHeroFactory _mainHeroFactory;

    public void Initialize(MainHeroFactory mainHeroFactory)
    {
        _mainHeroFactory = mainHeroFactory;
    }

    public AgentCharacter Spawn()
    {
        return _mainHeroFactory.CreateAgentMainHero(
            _config,
            _spawnPoint.position,
            _followCamera,
            _moveInput,
            _pointView,
            _ground,
            _pointToMovePrefab);
    }
}
