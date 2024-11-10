using AppUi.Components;

using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace AppUi.Systems;

public class BallMovementSystem : IEcsRunSystem
{
    private EcsFilterInject<Inc<BallComponent>> _ballFilter;
    private EcsPoolInject<BallComponent> _ballPool;
    
    
    public void Run(IEcsSystems systems)
    {
        foreach (var i in _ballFilter.Value)
        {
            ref var ball = ref _ballPool.Value.Get(i);
            ball.Position += ball.Velocity;
            
        }
    }
}