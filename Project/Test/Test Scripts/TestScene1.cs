using Godot;
using System;

public partial class TestScene1 : Node
{
	[Export] public PackedScene Selection;
	
	[Export] public PackedScene MiniGame;
	
	
	private TestMagicSelect _selectScene;

	private MagicMiniGame _magicMiniGame;
	
	private MousePosition _mousePosition;
	
	private int _selectionNumber;
	
	private int _miniGameNumber;
	private int _onScreenNumber {get; set; }
	private int _onScreenRadius { get; set; }
	
	public TestScene1()
	{
		_onScreenNumber = 3;
		_onScreenRadius = 15;
	}

	public TestScene1(int onScreenNumber, int onScreenRadius)
	{
		_onScreenNumber = onScreenNumber;
		_onScreenRadius = onScreenRadius;
	}
	public override void _Ready()
	{
		GetTree().Root.SizeChanged += OnWindowResized;
		_mousePosition = new MousePosition();
		AddChild(_mousePosition);
		AddSelector();
	}
	public int getSelectionNumber() => _selectionNumber;
	public int getMiniGameNumber() => _miniGameNumber;
	public int getOnScreenNumber() => _onScreenNumber;
	public void setOnScreenNumber(int number) => _onScreenNumber = number;
	public int getOnScreenRadius() => _onScreenRadius;
	public void setOnScreenRadius(int radius) => _onScreenRadius = radius;
	
	
	private void OnWindowResized()
	{
		Positioner();
		GD.Print("Window size changed!");
	}
	private void OnMySignalReceived(int signal)
	{
		GD.PrintErr("Received signal: " + signal);
		_selectionNumber = signal;
		_selectScene.QueueFree();
		_magicMiniGame = MiniGame.Instantiate<MagicMiniGame>();
		AddChild(_magicMiniGame);
		_mousePosition.Center();
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
	private void AddSelector()
	{
		_selectScene = Selection.Instantiate<TestMagicSelect>();
		_selectScene.SetCoor(GetWindow().Size - GetWindow().Size / _onScreenNumber);
		_mousePosition.Center();
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
	private void Positioner()
	{
		Vector2 windowSize = GetWindow().Size;
		GD.Print("Window Size: " + windowSize);

		if (_selectScene != null)
		{
			GD.Print(_selectScene.GetCoor());
			_selectScene.SetCoor(windowSize - windowSize / _onScreenNumber);
			_selectScene.Radius = (int)(windowSize.X / _onScreenRadius);
			_selectScene.Positioner();
		}

		if (_magicMiniGame != null)
		{
			_magicMiniGame.SetCoor(windowSize - windowSize / _onScreenNumber);
			GD.Print("The magic minigame is " + _magicMiniGame.x + ", " + _magicMiniGame.y);
			_magicMiniGame.SetRadius((int)(windowSize.X / _onScreenRadius));
			_magicMiniGame.Positioner();
		}
	}
	public override void _Process(double delta)
	{
	}
}
