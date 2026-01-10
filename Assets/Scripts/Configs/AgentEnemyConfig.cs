using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Gameplay/AgentEnemyConfig", fileName = "AgentEnemyConfig")]
public class AgentEnemyConfig : ScriptableObject
{
    [field: SerializeField] public AgentCharacter Prefab { get; private set; }
    [field: SerializeField] public float MaxHealth { get; private set; } = 100;
    [field: SerializeField] public float MoveSpeed { get; private set; } = 4;
    [field: SerializeField] public float RotationSpeed { get; private set; } = 900;
    [field: SerializeField] public float JumpSpeed { get; private set; } = 15;
    [field: SerializeField] public AnimationCurve JumpCurve { get; private set; }

    [field: SerializeField] public float AgroRange { get; private set; } = 20;
    [field: SerializeField] public float MinDistanceToTarget { get; private set; } = 1;
    [field: SerializeField] public float IdleTimer { get; private set; } = 1;
    [field: SerializeField] public float TimeForIdle { get; private set; } = 1;
}
