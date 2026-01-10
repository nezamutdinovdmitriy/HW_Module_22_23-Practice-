using System.Collections;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private MainHeroSpawner _mainHeroSpawner;
    [SerializeField] private EnemiesSpawner _enemiesSpawner;

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

        _desktopInput = new DesktopInput();
        _audioController.Initialize();

        _controllersUpdateService = new ControllersUpdateService();
        
        _controllersFactory = new ControllersFactory();
        _charactersFactory = new CharactersFactory();

        MainHeroFactory mainHeroFactory = new MainHeroFactory(_controllersUpdateService, _controllersFactory, _charactersFactory);
        EnemiesFactory enemiesFactory = new EnemiesFactory(_controllersUpdateService, _controllersFactory, _charactersFactory);

        _mainHeroSpawner.Initialize(mainHeroFactory);
        _enemiesSpawner.Initialize(enemiesFactory);

        AgentCharacter mainHero = _mainHeroSpawner.Spawn();

        _medkitSpawner.Initialize(_desktopInput);

        yield return new WaitForSeconds(2f);

        _loadingScreen.Hide();

        _confirmPopup.Show();
        _confirmPopup.ShowMessage($"PRESS {KeyCode.R.ToString()} FOR BEGIN");

        yield return _confirmPopup.WaitConfirm(KeyCode.R);

        _confirmPopup.Hide();

        _enemiesSpawner.Spawn(mainHero.transform);
    }

    private void Update()
    {
        _controllersUpdateService?.Update(Time.deltaTime);
    }
}
