using AppUi.Systems;
using HarfBuzzSharp;
using Leopotam.EcsLite;
namespace AppUi.Components;


public abstract class SceneObject
{
    public int Id { get; }
    private static int _idCounter = 0;
    
    
    protected SceneObject()
    {
        Id = _idCounter++;
    }

    public abstract void New(EcsWorld world);
    
}