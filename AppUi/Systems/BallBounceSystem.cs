using System;
using System.Numerics;
using AppUi.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace AppUi.Systems;

public class BallBounceSystem : IEcsRunSystem
{
    private readonly EcsFilterInject<Inc<BallComponent>> _ballFilter;
    private readonly EcsPoolInject<BallComponent> _ballPool;
    
    private readonly EcsFilterInject<Inc<WallComponent>> _wallFilter;
    private readonly EcsPoolInject<WallComponent> _wallPool;
    public void Run(IEcsSystems systems)
    {
        foreach (var i in _ballFilter.Value)
        {
            ref var ball = ref _ballPool.Value.Get(i);
            foreach (var j in _wallFilter.Value)
            {
                ref var wall = ref _wallPool.Value.Get(j);

                if (IsCollision(ball, wall, out var normal))
                {
                    ball.Velocity = Vector2.Reflect(ball.Velocity, normal);
                    Console.WriteLine($"Ball collided with wall ID: {j}");
                }
            }
        }
    }
    
    
    private bool IsCollision(BallComponent ball, WallComponent wall, out Vector2 normal)
    {
        float distance = DistancePointToSegment(ball.Position, wall.Start, wall.End);
        
        if (distance <= ball.Radius)
        {
            Vector2 wallDirection = (wall.End - wall.Start);
            normal = new Vector2(-wallDirection.Y, wallDirection.X);
            normal = Vector2.Normalize(normal);
            return true;
        }

        normal = Vector2.Zero;
        return false;
    }

    private float DistancePointToSegment(Vector2 point, Vector2 a, Vector2 b)
    {

        Vector2 ab = b - a;
        Vector2 ap = point - a;

        float abSquared = ab.LengthSquared();
        float abDotAp = Vector2.Dot(ap, ab);
        float t = Math.Clamp(abDotAp / abSquared, 0, 1);

        Vector2 closestPoint = a + t * ab;
        return Vector2.Distance(point, closestPoint);
    }

  
}