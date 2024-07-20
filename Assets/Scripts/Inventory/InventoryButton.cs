using UnityEngine;
using Zenject;

public class InventoryButton : MonoBehaviour
{
    [Inject]
    private InventoryManager _inventoryManager;

    public void OnInventoryButtonClick()
    {
        var inventoryGameOblect = _inventoryManager.gameObject;
        inventoryGameOblect.SetActive(!inventoryGameOblect.activeInHierarchy);
    }
}
