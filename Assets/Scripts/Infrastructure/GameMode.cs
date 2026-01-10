using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
public class GameMode
{
    public event Action Win; 
    public event Action Defeat;

    private LevelConfig _levelConfig;

    private float _currentDistanceTraveled;
    private float _currentTimeToDefeat;

    private AgentCharacter _mainHero;
    private Vector3 _previousMainHeroPosition;

    private EnemiesSpawner _enemiesSpawner;
    private List<AgentCharacter> _spawnedEnemies;

    private bool _isRunning;

    public GameMode(LevelConfig levelConfig, AgentCharacter mainHero, EnemiesSpawner enemiesSpawner)
    {
        _levelConfig = levelConfig;
        _mainHero = mainHero;
        _enemiesSpawner = enemiesSpawner;
    }

    public void Start()
    {
        _currentTimeToDefeat = _levelConfig.TimeToDefeat;
        _currentDistanceTraveled = 0;

        _previousMainHeroPosition = _mainHero.transform.position;

        _spawnedEnemies = _enemiesSpawner.Spawn(
            _levelConfig.EnemyConfig, 
            _mainHero.transform,
            _levelConfig.EnemiesSpawnRange,
            _levelConfig.EnemiesCount);

        _isRunning = true;
    }

    public void Update(float deltaTime)
    {
        if (_isRunning == false)
            return;

        ProcessCountingDefeatTime(deltaTime);

        if (DefeatConditionCompleted())
        {
            ProcessDefeat();
            return;
        }

        ProcessCountingCurrentDistanceTraveled();

        if (WinConditionCompleted())
        {
            ProcessWin();
            return;
        }
    }

    private void ProcessCountingDefeatTime(float deltaTime) => _currentTimeToDefeat -= deltaTime;

    private void ProcessCountingCurrentDistanceTraveled()
    {
        _currentDistanceTraveled += (_mainHero.transform.position - _previousMainHeroPosition).magnitude;
        _previousMainHeroPosition = _mainHero.transform.position;
    }

    private void ProcessDefeat()
    {
        ProcessEndGame();

        Defeat?.Invoke();
    }

    private void ProcessWin()
    {
        ProcessEndGame();

        Win?.Invoke();
    }

    private void ProcessEndGame()
    {
        _isRunning = false;

        foreach (AgentCharacter enemy in _spawnedEnemies)
            enemy.Destroy();

        _spawnedEnemies.Clear();
    }

    private bool DefeatConditionCompleted() => _currentTimeToDefeat <= 0;
    private bool WinConditionCompleted() => _currentDistanceTraveled >= _levelConfig.DistanceTraveledToWin;
}
