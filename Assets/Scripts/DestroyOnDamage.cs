using UnityEngine;

[RequireComponent(typeof(Damage))]
public class DestroyOnDamage : MonoBehaviour
{
    private Damage _damage;

    private void Awake()
    {
        _damage = GetComponent<Damage>();
    }

    public void DestroyBullet()
    {
        Destroy(gameObject);
    }

    private void OnEnable()
    {
        _damage.DamageGiven += DestroyBullet;
    }

    private void OnDisable()
    {
        _damage.DamageGiven -= DestroyBullet;
    }
}
