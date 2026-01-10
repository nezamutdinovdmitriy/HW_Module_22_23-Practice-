using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Gameplay/LevelConfig", fileName = "LevelConfig")]
public class LevelConfig : ScriptableObject
{
    [field: SerializeField] public float DistanceTraveledToWin { get; private set; }
    [field: SerializeField] public float TimeToDefeat { get; private set; }
    [field: SerializeField] public AgentEnemyConfig EnemyConfig { get; private set; }
    [field: SerializeField] public int EnemiesCount { get; private set; }
    [field: SerializeField] public float EnemiesSpawnRange { get; private set; }
    [field: SerializeField] public Vector3 MainHeroStartPosition { get; private set; }

    [ContextMenu("UpdateMainHeroStartPosition")]
    public void UpdateMainHeroStartPosition()
    {
        GameObject point = GameObject.FindGameObjectWithTag("MainHeroStartPosition");
        MainHeroStartPosition = point.transform.position;
    }
}
