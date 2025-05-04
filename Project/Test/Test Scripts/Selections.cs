using Godot;
using System;

public partial class Selections : Node
{
	[Export] protected PackedScene Selection;
	protected TestMagicSelect _selectScene;
	protected MousePosition _mousePosition;
	protected int _selectionNumber;
	protected Vector2 windowSize;
	protected int _onScreenNumber {get; set; }
	protected int _onScreenRadius { get; set; }
	public int getSelectionNumber() => _selectionNumber;
	public int getOnScreenNumber() => _onScreenNumber;
	public void setOnScreenNumber(int number) => _onScreenNumber = number;
	public int getOnScreenRadius() => _onScreenRadius;
	public void setOnScreenRadius(int radius) => _onScreenRadius = radius;
	
	public Selections()
	{
		_onScreenNumber = 3;
		_onScreenRadius = 15;
	}

	public Selections(int onScreenNumber, int onScreenRadius)
	{
		_onScreenNumber = onScreenNumber;
		_onScreenRadius = onScreenRadius;
	}
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetTree().Root.SizeChanged += OnWindowResized;
		_mousePosition = new MousePosition();
		AddChild(_mousePosition);
		AddSelector();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	public virtual void OnMySignalReceived(int signal)
	{
		GD.PrintErr("Received signal: " + signal);
		_selectionNumber = signal;
		_selectScene.QueueFree();
		_mousePosition.Center();
		Positioner();
	}
	
	private void OnWindowResized()
	{
		Positioner();
		GD.Print("Window size changed!");
	}
	public virtual void Positioner()
	{
		windowSize = GetWindow().Size;
		GD.Print("Window Size: " + windowSize);

		if (_selectScene != null)
		{
			GD.Print(_selectScene.GetCoor());
			_selectScene.SetCoor(windowSize - windowSize / _onScreenNumber);
			_selectScene.Radius = (int)(windowSize.X / _onScreenRadius);
			GD.Print("BEFORE selectionScreen positioner");
			_selectScene.Positioner();
		}
		PositionerAmendment();
		
	}

	public virtual void PositionerAmendment()
	{
		GD.Print("Admendement positioner");
	}
	public virtual void AddSelector()
	{
		GD.Print("Before instance");
		_selectScene = Selection.Instantiate<TestMagicSelect>();
		GD.Print("After instance, before coor");
		_selectScene.SetCoor(GetWindow().Size - GetWindow().Size / _onScreenNumber);
		GD.Print("After coor, before mouse center");
		_mousePosition.Center();
		GD.Print("After mouse center, before positioner");
		Positioner();
		GD.Print("After positioner, before adding scene");
		AddChild(_selectScene);
		GD.Print("After adding scene, before signal");
		
		if (_selectScene is Node signalEmitter && signalEmitter.HasSignal("Selected"))
		{
			GD.Print("Selected signal connected");
			signalEmitter.Connect("Selected", new Callable(this, "OnMySignalReceived"));
		}
		else
		{
			GD.PrintErr("Emitter instance does not have the signal 'MySignal'.");
		}
	}
}
