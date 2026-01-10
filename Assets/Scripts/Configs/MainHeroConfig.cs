using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Gameplay/MainHeroConfig", fileName = "MainHeroConfig")]
public class MainHeroConfig : ScriptableObject
{
    [field: SerializeField] public AgentCharacter Prefab { get; private set; }
    [field: SerializeField] public float MaxHealth { get; private set; } = 100;
    [field: SerializeField] public float MoveSpeed { get; private set; } = 5;
    [field: SerializeField] public float RotationSpeed { get; private set; } = 900;
    [field: SerializeField] public float JumpSpeed { get; private set; } = 15;
    [field: SerializeField] public AnimationCurve JumpCurve { get; private set; }

}
