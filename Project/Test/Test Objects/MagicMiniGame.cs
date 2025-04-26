using Godot;
using System;

public partial class MagicMiniGame : Node2D
{
	[Signal]
	public delegate void OutOfPointsEventHandler();
	
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

	public int x, y, Points = 0;
	public float Radius;
	
	public MagicMiniGame()
	{
		this.x = 0;
		this.y = 0;
		this.Radius = 0;
	}

	public MagicMiniGame(int x, int y, float radius)
	{
		this.x = x;
		this.y = y;
		this.Radius = radius;
	}

	public void SetRadius(int radius)
	{
		this.Radius = radius;
	}

	~MagicMiniGame()
	{
		DotScene.Free();
		dot.Dispose();
		EmitterScene.Free();
	}

	public override void _Ready()
	{
		for (int i = 0; i < 100; i++)
		{
			var EmitterInstance = MakePoint(x, y, Radius);
			AddChild(EmitterInstance);
		}
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

	public void SetCoor(Vector2 coor)
	{
		this.x = (int)coor.X;
		this.y = (int)coor.Y;
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
		else
		{
			EmitSignal("OutOfPoints");
		}
	}

	private Vector2 GetRandomPoint(int X, int Y, float R)
	{
		Random random = new();
		double angle = random.NextDouble() * (2 * Math.PI);
	
		float offsetX = R * (float)Math.Cos(angle);
		float offsetY = R * (float)Math.Sin(angle);
	
		return new Vector2(X + offsetX, Y + offsetY); // Keep as float
	}

	private Node2D MakePoint(int X, int Y, float R)
	{
		point = GetRandomPoint(X, Y, R);
		var newDot = DotScene.Instantiate<Node2D>();
		newDot.Position = point;
		return (newDot);
	}

	public int GetPoints() => Points;
}
