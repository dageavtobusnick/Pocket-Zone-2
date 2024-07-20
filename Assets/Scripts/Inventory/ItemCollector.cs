using UnityEngine;
using Zenject;

[RequireComponent(typeof(Collider2D))]
public class ItemCollector : MonoBehaviour
{
    [Inject]
    private InventoryManager _inventoryManager;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other != null && other.TryGetComponent<Collectable>(out var item))
        {
            item.Collect(_inventoryManager);
        }
    }
}
