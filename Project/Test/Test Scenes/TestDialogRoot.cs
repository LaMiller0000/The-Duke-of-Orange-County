using Godot;
using System;

public partial class TestDialogRoot : Node2D
{
	[Export] public string[] txt = 
	{
		"Test Writing section one.",
		"Test Writing section two.", 
	};
	private TestDialog _testDialog;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_testDialog = new TestDialog(0.1f, txt, new Vector2(300, 300), 24);
		AddChild(_testDialog);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
