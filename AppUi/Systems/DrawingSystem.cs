using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using AppUi.Components;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.LogicalTree;
using Avalonia.Media;
using Avalonia.Threading;

namespace AppUi.Systems;

public class DrawingSystem
{
    private readonly Canvas _canvas;
    

    public DrawingSystem(Canvas canvas)
    {
        _canvas = canvas;
    }
    
    public void DrawBall(BallComponent component)
    {
        
        Dispatcher.UIThread.Post(() =>
        {
            var el = new Ellipse();
            el.Fill = Brushes.Red;
            el.Width = component.Radius * 2;
            el.Height = component.Radius * 2;
            el.Name = component.Id.ToString();
            Canvas.SetLeft(el, component.Position.X-component.Radius);
            Canvas.SetTop(el, component.Position.Y-component.Radius);
            _canvas.Children.Add(el);
        });
    }

    public void DrawWall(WallComponent component)
    {
        
        
        Dispatcher.UIThread.Post(() =>
        {
            var el = new Line();
            el.Stroke = Brushes.Red;
            el.StrokeThickness = 2;
            el.StartPoint = new Point(component.Start.X, component.Start.Y);
            el.EndPoint = new Point(component.End.X, component.End.Y);
            el.Name = component.Id.ToString();
            _canvas.Children.Add(el);
        });
    }

    public void MoveBall(BallComponent component)
    {
        Dispatcher.UIThread.Post(() =>
        {
            var el = _canvas.Children.FirstOrDefault(x=>x.Name == component.Id.ToString());
            if (el is null) return;
            Canvas.SetLeft(el, component.Position.X - component.Radius);
            Canvas.SetTop(el, component.Position.Y - component.Radius);
        });
    }
    
    
}