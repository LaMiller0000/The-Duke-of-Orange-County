using Godot;
using System;

public partial class MagicUiSelection : Selections
{
	[Export] public PackedScene MiniGame;

	private MagicMiniGame _magicMiniGame;
	
	private int _miniGameNumber;
	
	public MagicUiSelection()
	{
		_onScreenNumber = 3;
		_onScreenRadius = 15;
	}

	public MagicUiSelection(int onScreenNumber, int onScreenRadius)
	{
		_onScreenNumber = onScreenNumber;
		_onScreenRadius = onScreenRadius;
	}
	public int getMiniGameNumber() => _miniGameNumber;
	
	public override void OnMySignalReceived(int signal)
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
	public virtual void SignalRecievedAmendment()
	{
		GD.PrintErr("Received signal");
	}
	private void OutOfPointsReceived()
	{
		_miniGameNumber = _magicMiniGame.GetPoints();
		GD.PrintErr("Received points: " + _miniGameNumber);
		_magicMiniGame.QueueFree();
		AddSelector();
	}
	public override void PositionerAmendment()
	{
		GD.PrintErr("Positioner amendment");
		if (_magicMiniGame != null)
		{
			_magicMiniGame.SetCoor(windowSize - windowSize / _onScreenNumber);
			GD.Print("The magic minigame is " + _magicMiniGame.x + ", " + _magicMiniGame.y);
			_magicMiniGame.SetRadius((int)(windowSize.X / _onScreenRadius));
			_magicMiniGame.Positioner();
		}
	}
}
