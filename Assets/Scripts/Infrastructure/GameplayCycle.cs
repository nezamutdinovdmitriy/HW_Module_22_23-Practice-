using System;
using System.Collections;
using UnityEngine;

public class GameplayCycle : IDisposable
{
    private MainHeroFactory _mainHeroFactory;
    private MainHeroConfig _mainHeroConfig;
    private AgentCharacter _mainHero;

    private IPointToMoveInput _moveInput;
    private ISelectedPositionView _selectedPositionView;
    private LayerMask _ground;
    private GameObject _pointToMovePrefab;

    private LevelConfig _levelConfig;

    private ConfirmPopup _confirmPopup;

    private GameMode _gameMode;

    private EnemiesSpawner _enemiesSpawner;

    private MonoBehaviour _context;

    public GameplayCycle(
        MainHeroFactory mainHeroFactory, 
        MainHeroConfig mainHeroConfig, 
        IPointToMoveInput moveInput, 
        ISelectedPositionView selectedPositionView, 
        LayerMask ground, 
        GameObject pointToMovePrefab, 
        LevelConfig levelConfig,
        ConfirmPopup confirmPopup, 
        EnemiesSpawner enemiesSpawner, 
        MonoBehaviour context)
    {
        _mainHeroFactory = mainHeroFactory;
        _mainHeroConfig = mainHeroConfig;
        _moveInput = moveInput;
        _selectedPositionView = selectedPositionView;
        _ground = ground;
        _pointToMovePrefab = pointToMovePrefab;
        _levelConfig = levelConfig;
        _confirmPopup = confirmPopup;
        _enemiesSpawner = enemiesSpawner;
        _context = context;
    }

    public void Update(float deltaTime) => _gameMode?.Update(deltaTime);

    public void Prepare()
    {
        _mainHero = _mainHeroFactory.CreateAgentMainHero(_mainHeroConfig, _levelConfig.MainHeroStartPosition, _moveInput, _selectedPositionView, _ground, _pointToMovePrefab);
    }

    public IEnumerator Launch()
    {
        _confirmPopup.Show();
        _confirmPopup.ShowMessage($"PRESS {KeyCode.R.ToString()} FOR BEGIN");

        yield return _confirmPopup.WaitConfirm(KeyCode.R);

        _confirmPopup.Hide();

        _gameMode = new GameMode(_levelConfig, _mainHero, _enemiesSpawner);

        _gameMode.Win += OnGameModeWin;
        _gameMode.Defeat += OnGameModeDefeat;

        _gameMode.Start();
    }

    public void Dispose() => OnGameModeEnded();

    private void OnGameModeEnded()
    {
        if(_gameMode != null)
        {
            _gameMode.Win -= OnGameModeWin;
            _gameMode.Defeat -= OnGameModeDefeat;
        }
    }

    private void OnGameModeDefeat()
    {
        OnGameModeEnded();
        Debug.Log("Defeat");
        _context.StartCoroutine(Launch());
    }

    private void OnGameModeWin()
    {
        OnGameModeEnded();
        Debug.Log("Win");
        _context.StartCoroutine(Launch());
    }
}
