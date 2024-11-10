using System.Numerics;
using AppUi.Components;
using Leopotam.EcsLite;

namespace AppUi.Components;

public delegate void DrawWall(WallComponent wall);

public class Wall : SceneObject
{
    public Vector2 Start { get; }
    public Vector2 End { get; }
    
    public event DrawWall OnDrawWall;

    public Wall(Vector2 start, Vector2 end)
    {
        Start = start;
        End = end;
    }
    
    public Wall(Vector2 start, Vector2 end, DrawWall draw)
    {
        Start = start;
        End = end;
        OnDrawWall += draw;
    }

    public override void New(EcsWorld world)
    {
        var pool = world.GetPool<WallComponent>();
        var entity = world.NewEntity();
        ref WallComponent cmp = ref pool.Add(entity);
        cmp.Start = Start;
        cmp.End = End;
        cmp.Id = Id;
        OnDrawWall(cmp);
    }
}

public struct WallComponent
{
    public int Id;
    public Vector2 Start;
    public Vector2 End;
    public double Length => Vector2.Distance(Start, End);
    
}