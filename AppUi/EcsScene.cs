using AppUi.Components;
using AppUi.Systems;
using Leopotam.EcsLite;

namespace AppUi;

public class EcsScene : IEcsSystem
{
    private readonly EcsWorld _world;

    public EcsScene(EcsWorld world, Scene scene)
    {
        _world = world;
        foreach (var obj in scene.Objects) obj.New(world);
    }
    
}