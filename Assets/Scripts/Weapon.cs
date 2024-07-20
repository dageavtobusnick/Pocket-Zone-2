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
    private Transform _end;
    [SerializeField]
    private HP _hP;

    [Inject]
    private Inventory _inventory;

    public void Shoot()
    {
        if(!_inventory.ContainsItem(_ammo))
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
    }

}
