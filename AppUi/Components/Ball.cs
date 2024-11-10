using System.Numerics;
using AppUi.Components;
using AppUi.Systems;
using Leopotam.EcsLite;

namespace AppUi.Components;

public delegate void DrawBall(BallComponent ball);

public class Ball : SceneObject
{
    public Vector2 Position { get; set; }
    public Vector2 Velocity { get; set; }
    public float Radius { get; }
    
    public event DrawBall OnDrawBall;
    
    public Ball(Vector2 position, Vector2 velocity, float radius)
    {
        Position = position;
        Velocity = velocity;
        Radius = radius;
    }
    
    public Ball(Vector2 position, Vector2 velocity, float radius, DrawBall draw)
    {
        Position = position;
        Velocity = velocity;
        Radius = radius;
        OnDrawBall += draw;
    }
    
    public override void New(EcsWorld world)
    {
        var pool = world.GetPool<BallComponent>();
        var entity = world.NewEntity();
        ref BallComponent cmp = ref pool.Add(entity);
        cmp.Position = Position;
        cmp.Velocity = Velocity;
        cmp.Radius = Radius;
        cmp.Id = Id;
        OnDrawBall(cmp);
    }
    
}

public struct BallComponent
{
    public int Id;
    public Vector2 Position;
    public Vector2 Velocity;
    public float Radius;
}