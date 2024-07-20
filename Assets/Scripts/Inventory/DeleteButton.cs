using UnityEngine;
using Zenject;

public class DeleteButton : MonoBehaviour
{
    [Inject]
    private InventoryManager _inventoryManager;

    public void OnDeleteButtonClicked()
    {
        _inventoryManager.DeleteSelectedItem();
    }
}
