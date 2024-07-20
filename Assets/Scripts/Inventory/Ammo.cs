using UnityEngine;

[CreateAssetMenu(fileName = "New Ammo", menuName = "Inventory/Ammo")]
public class Ammo : Item
{
    [SerializeField]
    private int _damage;
    [SerializeField]
    private int _speed;

    public int Damage { get => _damage; }
    public int Speed { get => _speed; }
}
