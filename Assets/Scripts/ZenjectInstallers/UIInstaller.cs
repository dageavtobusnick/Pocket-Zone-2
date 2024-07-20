using UnityEngine;
using Zenject;

public class UIInstaller : MonoInstaller
{
    [SerializeField]
    private InventoryManager _inventoryManager;
    public override void InstallBindings()
    {
        Container.Bind<InventoryManager>().FromInstance(_inventoryManager).AsSingle();
    }
}