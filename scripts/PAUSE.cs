using Godot;
using System;

public partial class Pause : CanvasLayer
{
    [Signal]
    public delegate void StartGameEventHandler();   
    public override void _Ready()
    {
		Hide();
    }

    public override void _Process(double delta)
    {
        if (Input.IsActionJustReleased("pause_game"))
		{
            Show();
		    GetTree().Paused = true;
		}
    }
    private void OnContinue()
    {   
        Hide();
        GetTree().Paused = false;
    }
    private void OnRestart()
    {
        GetTree().Paused = false;
        EmitSignal(SignalName.StartGame);
        Hide();
        
    }
    private void OnQuit()
    {
        GD.Print("Now Quiting");
        GetTree().Root.PropagateNotification((int)NotificationWMCloseRequest);
        
    }
}

