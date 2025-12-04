using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private AgentCharacter _character;
    [SerializeField] private Image _filled;

    private void Update()
    {
        _filled.fillAmount = _character.CurrentHealth / _character.MaxHealth;
    }
}
