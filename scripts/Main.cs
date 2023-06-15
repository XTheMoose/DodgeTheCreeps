using Godot;
using System;

public partial class Main : Node
{
	[Export]
	public PackedScene MobScene { get; set;}

	private int _score;
	private float _mobTimer;
	private int _speed;

	public void GameOver()
	{
		GetNode<Timer>("MobTimer").Stop();
		GetNode<Timer>("ScoreTimer").Stop();
		GetNode<HUD>("HUD").ShowGameOver();
		GetNode<AudioStreamPlayer>("Music").Stop();
		GetNode<AudioStreamPlayer>("DeathSound").Play();
	}

	public void NewGame()
	{
		_score = 0;
		_mobTimer = 1;
		_speed = 0;

		GetTree().CallGroup("mobs", Node.MethodName.QueueFree);

		var player = GetNode<Player>("Player");
		var startPosition = GetNode<Marker2D>("StartPosition");
		player.Start(startPosition.Position);

		GetNode<Timer>("StartTimer").Start();
		GetNode<Timer>("MobTimer").WaitTime = _mobTimer;

		var hud = GetNode<HUD>("HUD");
		hud.UpdateScore(_score);
		hud.ShowMessage("Get Ready!");

		GetNode<AudioStreamPlayer>("Music").Play();

	}
	public void PauseGame()
	{
		GetNode<PAUSE>("PAUSE").Show();
		GetTree().Paused = true;
	}

	private void OnScoreTimerTimeout()
	{
		_score++;
		GetNode<HUD>("HUD").UpdateScore(_score);

		if (_mobTimer > 0.05 && _score % 5 == 0)
		{
			_mobTimer -= (float)0.05;
		}
		GetNode<Timer>("MobTimer").WaitTime = _mobTimer;

		_speed = _score * 4;
	}

	private void OnStartTimerTimeout()
	{
		GetNode<Timer>("MobTimer").Start();
		GetNode<Timer>("ScoreTimer").Start();
	}

	private void OnMobTimerTimeout()
	{
		// Create Instance of Mob Scene
		Mob mob = MobScene.Instantiate<Mob>();

		// Choose Random Location on Path2D
		var mobSpawnLocation = GetNode<PathFollow2D>("MobPath/MobSpawnLocation");
		mobSpawnLocation.ProgressRatio = GD.Randf();

		// Set mob's direction perpendicult to the path direction
		float direction = mobSpawnLocation.Rotation + Mathf.Pi / 2;

		//Set mob's position to random location
		mob.Position = mobSpawnLocation.Position;

		// Randomize direction
		
		direction += (float)GD.RandRange(-Mathf.Pi / 4, Mathf.Pi / 4);
		mob.Rotation = direction;

		// Choose random velocity
		var velocity = new Vector2((float)GD.RandRange(150.0 + _speed, 250.0 + _speed), 0);
		mob.LinearVelocity = velocity.Rotated(direction);

		// Spawn the mob in main scene
		AddChild(mob);
	}
}
