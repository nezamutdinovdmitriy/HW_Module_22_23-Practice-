using UnityEngine;

public class Medkit : Item
{
    [SerializeField] private float _restoreAmount;

    private IDamageable _damageable;

    public bool HasUsed { get; private set; }
    public Transform Target { get; private set; }
    
    public override void Use() => _damageable.Heal(_restoreAmount);

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IDamageable>(out _damageable))
        {
            Use();
            HasUsed = true;
            Target = other.transform;
        }
        else
            Debug.Log("Вам это не нужно!");
    }
}
