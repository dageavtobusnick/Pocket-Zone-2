using UnityEngine;
using Zenject;

public class InputInstaller : MonoInstaller
{
    [SerializeField]
    private VirtualJoystick _joystick;
    public override void InstallBindings()
    {
        Container.Bind<VirtualJoystick>().FromInstance(_joystick).AsSingle();
    }
}