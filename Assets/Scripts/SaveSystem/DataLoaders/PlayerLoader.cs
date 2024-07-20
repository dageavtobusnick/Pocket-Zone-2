using UnityEngine;
using Zenject;

[RequireComponent (typeof(HP))]
public class PlayerLoader : MonoBehaviour, IDataLoader<PlayerData>
{
    [SerializeField]
    private ItemRegistry items;

    [Inject]
    private InventoryManager _inventory;

    private HP _hP;

    private void Awake()
    {
        _hP = GetComponent<HP>();
    }

    public void LoadData(PlayerData data)
    {
        _inventory.LoadInventory(data, items);
        _hP.LoadHealth(data);
        transform.position = new Vector3(data.X, data.Y, 0);
    }

    public PlayerData SaveData()
    {
        var position = transform.position;
        return new PlayerData(position.x, position.y, _hP.Health, _inventory.Inventory.GetData());
    }
}

