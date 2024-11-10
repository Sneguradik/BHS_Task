using System;
using System.Threading;
using System.Threading.Tasks;
using AppUi.Systems;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Interactivity;

namespace AppUi;

public partial class MainWindow : Window
{

    public int BallPositionX { get; set; } = 140;

    private float BallXStart { get; set; } = 0;
    private float BallYStart { get; set; } = 0;

    private Canvas? _canvas { get; set; } = null;

    private Task? GameTask { get; set; }
    
    private CancellationTokenSource GameCancellationTokenSource { get; set; } = new CancellationTokenSource();
    
    
    public MainWindow()
    {
        InitializeComponent();
    }


    public void StartButtonHandler(object sender, RoutedEventArgs e)
    {
        if (_canvas is null)
        {
            _canvas = this.FindControl<Canvas>("Canvas");
            if (_canvas is null) return;
        }

        if (GameTask is not null)
        {
            GameCancellationTokenSource.Cancel();
            GameCancellationTokenSource.Dispose();
            GameCancellationTokenSource = new CancellationTokenSource();
            Thread.Sleep(2500);
        }
        
        _canvas.Children.Clear();
        
        var token = GameCancellationTokenSource.Token;
        
        
        GameTask = new Task(() =>
        {
            var drawingSystem = new DrawingSystem(_canvas);
            var world = new CustomWorld(drawingSystem);
            world.Run(CancellationToken.None);
        }, token);
        GameTask.Start();
    }

    private float GetBallPosition(object? sender, TextChangingEventArgs e)
    {
        float pos = 0;
        if (sender is null) return pos;
        if (sender is not TextBox textBox) return pos;
        float.TryParse(textBox.Text, out pos);
        return pos;
    }
    
    
}