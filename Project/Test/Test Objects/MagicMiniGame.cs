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

	private int TimeCounter = 3;

	private Node2D emitterInstance;
	
	private Node2D dot;
	
	private double angle;

	private Vector2 point;
	
	private Timer spawnTimer;

	private ProgressBar progressBar;

	public int x, y, Points = 0;
	public float Radius;
	
	public MagicMiniGame()
	{
		this.x = 0;
		this.y = 0;
		this.Radius = 0;
		spawnTimer = new Timer();
		AddChild(spawnTimer);
		spawnTimer.WaitTime = 1;
		spawnTimer.Connect("timeout", new Callable(this, "OnSpawn"));
		spawnTimer.Start();
		
		progressBar = new ProgressBar();
		progressBar.MinValue = 0;
		progressBar.MaxValue = 3;
		
	}

	public MagicMiniGame(int x, int y, float radius)
	{
		this.x = x;
		this.y = y;
		this.Radius = radius;
		spawnTimer = new Timer();
		AddChild(spawnTimer);
		spawnTimer.WaitTime = 1;
		spawnTimer.Connect("timeout", new Callable(this, "OnSpawn"));
		
		progressBar = new ProgressBar();
		progressBar.MinValue = 0;
		progressBar.MaxValue = 3;
		progressBar.Value = TimeCounter;
		progressBar.Size = new Vector2(2 * Radius, (float)0.5 * Radius); 
		progressBar.ShowPercentage = false; // Hide the default text
		progressBar.Position = new Vector2(x - (float)(x * .1), y + (float)(y * 0.3));
		AddChild(progressBar);
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
		spawnTimer.Start();
		AddChild(progressBar);
		progressBar.Value = TimeCounter;
		progressBar.Size = new Vector2(2 * Radius, (float)0.5 * Radius); 
		progressBar.ShowPercentage = false; // Hide the default text
		progressBar.Position = new Vector2(x - (float)(x * .1), y + (float)(y * 0.3));
		emitterInstance = MakePoint(x, y, Radius);
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

	public void Positioner()
	{
		GD.Print("FIRST LINE OF POSITIONER");
		progressBar.Position = new Vector2(x - (float)(x * .1), y + (float)(y * 0.3));
		progressBar.Size = new Vector2(2 * Radius, (float)0.5 * Radius); 
		GD.Print("SECOND LINE OF POSITIONER");
		SetCoor(GetWindow().Size - GetWindow().Size / 3);
		SetRadius((int)(GetWindow().Size.X / 15));
		point = new Vector2(x + Radius * (float)Math.Cos(angle), y + Radius * (float)Math.Sin(angle));
		GD.Print("THIRD LINE OF POSITIONER");
		emitterInstance.Position = point;
		//this.Position = point;
		GD.Print("FOURTH LINE OF POSITIONER");
	}
	
	private void OnSpawn()
	{
		--TimeCounter;
		progressBar.Value = TimeCounter;
		if (TimeCounter == 0)
		{
			EmitSignal("OutOfPoints");
		}
		GD.Print(TimeCounter.ToString());
	}

	public void SetCoor(Vector2 coor)
	{
		x = (int)coor.X;
		y = (int)coor.Y;
	}

	private void OnMySignalReceived()
	{
		GD.Print("Signal received");
		--Counter;
		++TimeCounter;
		progressBar.Value = TimeCounter;
		if (Counter != 0)
		{
			++Points;
			emitterInstance = MakePoint(x, y, Radius);
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
		angle = random.NextDouble() * (2 * Math.PI);
	
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
	
	public override void _Process(double delta)
	{
		//GD.Print("TEST");
		//Positioner();
	}
}
