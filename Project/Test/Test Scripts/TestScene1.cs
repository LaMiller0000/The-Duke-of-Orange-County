using Godot;
//using System;

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
		_selectScene = Selection.Instantiate<TestMagicSelect>();
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

	private void OnMySignalReceived(int signal)
	{
		GD.PrintErr("Received signal: " + signal);
		_selectionNumber = signal;
		//probably should emit a signal or hold the value that is received with signal to know what move to cast
		_selectScene.QueueFree();
		_magicMiniGame = MiniGame.Instantiate<MagicMiniGame>();
		AddChild(_magicMiniGame);
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
	}
	
	public int getSelectionNumber() => _selectionNumber;
	
	public int getMiniGameNumber() => _miniGameNumber;

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
