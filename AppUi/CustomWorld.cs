using System;
using System.Linq;
using System.Threading;
using AppUi.Components;
using AppUi.Systems;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace AppUi;

public class CustomWorld
{
    private readonly EcsSystems _systems;
    private readonly EcsWorld _world = new EcsWorld();
    private readonly DrawingSystem _drawingSystem;
    private readonly Scene _scene;

    public CustomWorld(DrawingSystem drawingSystem)
    {
        _systems = new EcsSystems(_world);
        _drawingSystem = drawingSystem;
        _scene = new Scene();
        _scene.DrawingSystem = _drawingSystem;
        _scene.Initialize();

        _systems
            .Add(new EcsScene(_world, _scene))
            .Add(new BallMovementSystem())
            .Add(new BallBounceSystem())
            .Inject()
            .Init();
    }
    
    public void Run(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            _systems.Run();
    
            var ballFilter = _systems.GetWorld().Filter<BallComponent>();
            var ballPool = _systems.GetWorld().GetPool<BallComponent>();
            foreach (var i in ballFilter.End())
            {
                ref var ball = ref ballPool.Get(i);
                Console.WriteLine($"Ball {ball.Id} position: {ball.Position}");
                _drawingSystem.MoveBall(ball);
                var id = ball.Id;
                var item = (Ball)_scene.Objects.First(x => x.Id == id);
                item.Position = ball.Position;
                
            }
            
            System.Threading.Thread.Sleep(32);
        }
    }
}