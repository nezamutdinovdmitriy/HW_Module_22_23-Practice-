using UnityEngine;

public class Medkit : Item
{
    [SerializeField] private float _restoreAmount;

    private IHealable _healable;

    public bool HasUsed { get; private set; }
    public Transform Target { get; private set; }

    public override void Use() => _healable.Heal(_restoreAmount);

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IHealable>(out _healable))
        {
            Use();
            HasUsed = true;
            Target = other.transform;
        }
        else
            Debug.Log("Вам это не нужно!");
    }
}
