using System.Collections.Generic;
using System.Numerics;
using AppUi.Components;
using AppUi.Systems;

namespace AppUi.Components;

public class Scene
{
    public List<SceneObject> Objects { get; } = new ();
    
    public DrawingSystem DrawingSystem { get; set; }

    public void Initialize()
    {
        Objects.Add(new Wall(new Vector2(100, 100), new Vector2(100, 200), DrawingSystem.DrawWall));
        Objects.Add(new Wall(new Vector2(100, 100), new Vector2(200, 100), DrawingSystem.DrawWall));
        Objects.Add(new Wall(new Vector2(200, 200), new Vector2(200, 100), DrawingSystem.DrawWall));
        Objects.Add(new Wall(new Vector2(100, 200), new Vector2(200, 260), DrawingSystem.DrawWall));
        
        Objects.Add(new Ball(new Vector2(150, 150), new Vector2(0,10), 10, DrawingSystem.DrawBall));
    }
}