using Godot;
using System;

public partial class MagicMelleStateMachine : Node
{
	// Called when the node enters the scene tree for the first time.
	public enum State {MAGIC, MELLE, DEFAULT}
	
	[Export]
	public State currentState = State.MAGIC;
	
	private TestScene1 magicScene;
	private MelleUiSelection melleScene;
	public override void _Ready()
	{

	}

	public void Switch()
	{
		switch (currentState)
		{
			case State.MAGIC:
				magicScene ??= new TestScene1();
				break;
			case State.MELLE:
				melleScene ??= new MelleUiSelection();
				break;
			case State.DEFAULT:
				break;
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}
}
