using UnityEngine;
using Zenject;

public class DropOnDeath: MonoBehaviour
{
    [SerializeField]
    private Item _drop;
    [SerializeField]
    private int _count;
    [SerializeField]
    private Collectable _collectable;


    private HP _hp;

    private void Awake()
    {
        _hp = GetComponent<HP>();
    }

    private void OnEnable()
    {
        _hp.Dead += OnDeath;
    }

    private void OnDisable()
    {
        _hp.Dead -= OnDeath;
    }

    private void OnDeath()
    {
        var cashedTransform = transform;
        var loot = Instantiate(_collectable, cashedTransform.position, cashedTransform.rotation);
        loot.Create(_drop, _count);
        Destroy(gameObject);
    }
}

