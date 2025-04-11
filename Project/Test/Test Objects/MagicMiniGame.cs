using Godot;
using System;

public partial class MagicMiniGame : Node2D
{
	[Export]
	public PackedScene DotScene { get; set; }

	[Export]
	public float SpawnInterval { get; set; } = 0.1f;

	[Export]
	public Rect2 SpawnArea { get; set; }

	private Timer timer;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		timer = GetNode<Timer>("Timer");
		timer.WaitTime = SpawnInterval;
		timer.Start();

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}
	
	private Vector2 GetRandomPoint(int x, int y, int radius)
	{
		Random random       = new();
		double angle        = random.NextDouble() * 2 * Math.PI;                    // Random angle between 0 and 2*pi
		double randomRadius = Math.Sqrt(random.NextDouble()) * (radius / 2); // Random radius between 0 and maxRadius

		// Calculate the x and y coordinates using polar to Cartesian conversion
		int randomX = x + (int)(randomRadius * Math.Cos(angle));
		int randomY = y + (int)(randomRadius * Math.Sin(angle));

		return new Vector2(randomX, randomY);
	}
	
	private void _on_timer_timeout()
	{
		var point = GetRandomPoint(800, 800, 450);
		var dot = DotScene.Instantiate<Node2D>();
		var RandomPosition = new Vector2(
			(float)point[0],
			(float)point[1]
		);
		dot.Position = RandomPosition;
		AddChild(dot );
		// Replace with function body.
	}
}



