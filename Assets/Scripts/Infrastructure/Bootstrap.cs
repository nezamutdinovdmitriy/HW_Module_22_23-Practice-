using Cinemachine;
using System.Collections;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private float _radius;
    [SerializeField] private int _count;

    [SerializeField] private CinemachineVirtualCamera _followCamera;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private LayerMask _ground;
    [SerializeField] private GameObject _pointToMovePrefab;

    private IPointToMoveInput _moveInput;
    private ISelectedPositionView _pointView;

    [SerializeField] private MedkitSpawner _medkitSpawner;

    [SerializeField] private LoadingScreen _loadingScreen;
    [SerializeField] private ConfirmPopup _confirmPopup;

    [SerializeField] private AudioController _audioController;

    private DesktopInput _desktopInput;
    private ControllersUpdateService _controllersUpdateService;
    
    private ControllersFactory _controllersFactory;
    private CharactersFactory _charactersFactory;

    private void Awake()
    {
        StartCoroutine(StartProcess());
    }

    private IEnumerator StartProcess()
    {
        _loadingScreen.Show();
        _loadingScreen.ShowMessage("Loading...");

        MainHeroConfig heroConfig = Resources.Load<MainHeroConfig>("Configs/MainHeroConfig");
        AgentEnemyConfig enemyConfig = Resources.Load<AgentEnemyConfig>("Configs/AgentEnemyConfig");

        _desktopInput = new DesktopInput();
        _audioController.Initialize();

        _controllersUpdateService = new ControllersUpdateService();
        
        _controllersFactory = new ControllersFactory();
        _charactersFactory = new CharactersFactory();

        MainHeroFactory mainHeroFactory = new MainHeroFactory(_controllersUpdateService, _controllersFactory, _charactersFactory);
        EnemiesFactory enemiesFactory = new EnemiesFactory(_controllersUpdateService, _controllersFactory, _charactersFactory);

        EnemiesSpawner enemiesSpawner = new EnemiesSpawner(enemiesFactory);

        AgentCharacter mainHero = mainHeroFactory.CreateAgentMainHero(heroConfig, _spawnPoint.position, _followCamera, _moveInput, _pointView, _ground, _pointToMovePrefab);

        _medkitSpawner.Initialize(_desktopInput);

        yield return new WaitForSeconds(2f);

        _loadingScreen.Hide();

        _confirmPopup.Show();
        _confirmPopup.ShowMessage($"PRESS {KeyCode.R.ToString()} FOR BEGIN");

        yield return _confirmPopup.WaitConfirm(KeyCode.R);

        _confirmPopup.Hide();

        enemiesSpawner.Spawn(enemyConfig, mainHero.transform, _radius, _count);
    }

    private void Update()
    {
        _controllersUpdateService?.Update(Time.deltaTime);
    }
}
