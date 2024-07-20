using UnityEngine;
using Zenject;

public class MapInstaller : MonoInstaller
{
    [SerializeField]
    private Vector2 _minBounds = new(-5, -5);
    [SerializeField]
    private Vector2 _maxBounds = new(5, 5);
    public override void InstallBindings()
    {
        Container.Bind<MapBounds>().AsSingle().WithArguments(_minBounds, _maxBounds);
    }
}