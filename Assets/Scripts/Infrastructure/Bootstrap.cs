using System.Collections;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private LayerMask _ground;
    [SerializeField] private GameObject _pointToMovePrefab;

    [SerializeField] private MedkitSpawner _medkitSpawner;

    [SerializeField] private LoadingScreen _loadingScreen;
    [SerializeField] private ConfirmPopup _confirmPopup;

    [SerializeField] private AudioController _audioController;

    private IPointToMoveInput _moveInput;
    private ISelectedPositionView _pointView;

    private DesktopInput _desktopInput;
    private ControllersUpdateService _controllersUpdateService;

    private GameplayCycle _gameplayCycle;

    private ControllersFactory _controllersFactory;
    private CharactersFactory _charactersFactory;

    private void Awake() => StartCoroutine(StartProcess());

    private IEnumerator StartProcess()
    {
        _loadingScreen.Show();
        _loadingScreen.ShowMessage("Loading...");

        MainHeroConfig heroConfig = Resources.Load<MainHeroConfig>("Configs/MainHeroConfig");
        
        LevelsListConfig levelsListConfig = Resources.Load<LevelsListConfig>("Configs/LevelsListConfig");

        _desktopInput = new DesktopInput();
        _audioController.Initialize();

        _controllersUpdateService = new ControllersUpdateService();
        
        _controllersFactory = new ControllersFactory();
        _charactersFactory = new CharactersFactory();

        MainHeroFactory mainHeroFactory = new MainHeroFactory(_controllersUpdateService, _controllersFactory, _charactersFactory);
        EnemiesFactory enemiesFactory = new EnemiesFactory(_controllersUpdateService, _controllersFactory, _charactersFactory);

        EnemiesSpawner enemiesSpawner = new EnemiesSpawner(enemiesFactory);

        LevelConfig levelConfig = levelsListConfig.GetRandomConfig();

        _gameplayCycle = new GameplayCycle(
            mainHeroFactory,
            heroConfig, 
            _moveInput, 
            _pointView, 
            _ground, 
            _pointToMovePrefab, 
            levelConfig, 
            _confirmPopup, 
            enemiesSpawner, 
            this);

        _medkitSpawner.Initialize(_desktopInput);

        yield return new WaitForSeconds(2f);

        _gameplayCycle.Prepare();

        _loadingScreen.Hide();

        yield return _gameplayCycle.Launch();
    }

    private void Update()
    {
        _controllersUpdateService?.Update(Time.deltaTime);
        _gameplayCycle?.Update(Time.deltaTime);
    }

    private void OnDestroy()
    {
        _gameplayCycle?.Dispose();
    }
}
