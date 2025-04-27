using Godot;
using System;

public partial class TestScene1 : Node
{
	[Export] public PackedScene Selection;
	
	[Export] public PackedScene MiniGame;
	
	private TestMagicSelect _selectScene;

	private MagicMiniGame _magicMiniGame;
	
	private int _selectionNumber;
	
	private int _miniGameNumber;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetTree().Root.SizeChanged += OnWindowResized;

		AddSelector();
	}

	private void AddSelector()
	{
		_selectScene = Selection.Instantiate<TestMagicSelect>();
		_selectScene.SetCoor(GetWindow().Size - GetWindow().Size / 3);
		Positioner();
		AddChild(_selectScene);
		if (_selectScene is Node signalEmitter && signalEmitter.HasSignal("Selected"))
		{
			signalEmitter.Connect("Selected", new Callable(this, "OnMySignalReceived"));
		}
		else
		{
			GD.PrintErr("Emitter instance does not have the signal 'MySignal'.");
		}
	}

	private void OnWindowResized()
	{
		Positioner();
		GD.Print("Window size changed!");
	}

	private void Positioner()
	{
		Vector2 windowSize = GetWindow().Size;
		GD.Print("Window Size: " + windowSize);

		if (_selectScene != null)
		{
			GD.Print(_selectScene.GetCoor());
			_selectScene.SetCoor(windowSize - windowSize / 3);
			_selectScene.Radius = (int)(windowSize.X / 15);
			_selectScene.Positioner();
		}

		if (_magicMiniGame != null)
		{
			_magicMiniGame.SetCoor(windowSize - windowSize / 3);
			GD.Print("The magic minigame is " + _magicMiniGame.x + ", " + _magicMiniGame.y);
			_magicMiniGame.SetRadius((int)(windowSize.X / 15));
			GD.Print("BEFORE POSITIONER");

			_magicMiniGame.Positioner();
		}
	}

	private void OnMySignalReceived(int signal)
	{
		GD.PrintErr("Received signal: " + signal);
		_selectionNumber = signal;
		//probably should emit a signal or hold the value that is received with signal to know what move to cast
		_selectScene.QueueFree();
		_magicMiniGame = MiniGame.Instantiate<MagicMiniGame>();
		AddChild(_magicMiniGame);
		Positioner();
		if (_magicMiniGame is Node signalEmitter && signalEmitter.HasSignal("OutOfPoints"))
		{
			signalEmitter.Connect("OutOfPoints", new Callable(this, "OutOfPointsReceived"));
		}
		else
		{
			GD.PrintErr("Emitter instance does not have the signal 'OutOfPoints'.");
		}
	}

	private void OutOfPointsReceived()
	{
		_miniGameNumber = _magicMiniGame.GetPoints();
		GD.PrintErr("Received points: " + _miniGameNumber);
		_magicMiniGame.QueueFree();
		AddSelector();
	}
	
	public int getSelectionNumber() => _selectionNumber;
	
	public int getMiniGameNumber() => _miniGameNumber;

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}
}
