using Godot;
using System;

public partial class PAUSE : CanvasLayer
{
    [Signal]
    public delegate void PauseGameEventHandler();
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
            EmitSignal(SignalName.PauseGame);
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
        
    }
}
