using UnityEngine;
using Zenject;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D _projectile;
    [SerializeField]
    private Ammo _ammo;
    [SerializeField]
    private Transform _start;
    [SerializeField]
    private int _shootsPerMinute;
    [SerializeField]
    private Transform _end;
    [SerializeField]
    private HP _hP;

    [Inject]
    private InventoryManager _inventory;

    private int _shootInterval;
    private float _lastShootTime;

    private void Awake()
    {
        _shootInterval = 60 / _shootsPerMinute;
        _lastShootTime = Time.time;
    }

    public void Shoot()
    {
        if(!_inventory.Inventory.ContainsItem(_ammo))
            return;
        if (_shootInterval > Time.time - _lastShootTime)
            return;
        var bullet = Instantiate(_projectile, _start);
        if(bullet.TryGetComponent<Damage>(out var damage))
        {
            damage.SetDamage(_ammo.Damage);
            damage.ExtractTeam(_hP);
            damage.AllowDamage();
        }
        var rb = bullet.GetComponent<Rigidbody2D>();
        var direction = (_end.position - _start.position).normalized;
        rb.velocity = direction * _ammo.Speed;
        _inventory.Inventory.RemoveItem(_ammo);
        _lastShootTime = Time.time;
    }

}