using Godot;
using System;

public partial class Shield : Area2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play();
	}

}
