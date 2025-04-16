using Godot;
using System;

public partial class MagicMiniGame : Node2D
{
	[Export]
	public PackedScene DotScene { get; set; }

	[Export]
	public float SpawnInterval { get; set; } = 0.1f;

	[Export]
	public int Counter { get; set; } = 3;

	[Export]
	public PackedScene EmitterScene { get; set; }

	private Node2D dot;

	private Vector2 point;

	[Export]
	public int x, y, Radius, Points;
	
	public MagicMiniGame()
	{
		this.x = 0;
		this.y = 0;
		this.Radius = 0;
	}

	public MagicMiniGame(int x, int y, int radius)
	{
		this.x = x;
		this.y = y;
		this.Radius = radius * 2;
	}

	~MagicMiniGame()
	{
		DotScene.Free();
		dot.Dispose();
		EmitterScene.Free();
	}

	public override void _Ready()
	{
		var emitterInstance = MakePoint(x, y, Radius);
		AddChild(emitterInstance);
		if (emitterInstance is Node signalEmitter && signalEmitter.HasSignal("MySignal"))
		{
			signalEmitter.Connect("MySignal", new Callable(this, "OnMySignalReceived"));
		}
		else
		{
			GD.PrintErr("Emitter instance does not have the signal 'MySignal'.");
		}
	}

	private void OnMySignalReceived()
	{
		GD.Print("Signal received");
		--Counter;
		if (Counter != 0)
		{
			++Points;
			var emitterInstance = MakePoint(x, y, Radius);
			AddChild(emitterInstance);
			if (emitterInstance is Node2D SignalEmitter && SignalEmitter.HasSignal("MySignal"))
			{
				SignalEmitter.Connect("MySignal", new Callable(this, nameof(OnMySignalReceived)));
			}
			else
			{
				GD.PrintErr("Emitter instance does not have the signal 'MySignal'.");
			}
		}
	}

	private Vector2 GetRandomPoint(int X, int Y, int R)
	{
		Random random = new();
		double angle = random.NextDouble() * 2 * Math.PI;
		int randomX = X + (int)(R * Math.Cos(angle));
		int randomY = Y + (int)(R * Math.Sin(angle));
		return new Vector2(randomX, randomY);
	}

	private Node2D MakePoint(int X, int Y, int R)
	{
		point = GetRandomPoint(X, Y, R);
		var newDot = DotScene.Instantiate<Node2D>();
		newDot.Position = point;
		return (newDot);
	}

	public int GetPoints() => Points;
}
