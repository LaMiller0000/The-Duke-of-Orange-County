using Godot;
using System;

public partial class TestDialogRoot : Node2D
{
	[Export] public string[] txt = { "I don't know why I even try anymore",
									 "There's this burning...",
									 "something, in me.",
									 "To be honest, how can this all happen?",
									 "We were all fine, all alright in our ways.",
									 "But no!",
									 "It changed, it always changes.",
									 "I was completely, and utterly fine in this state.",
									 "It changed, and everything's worse.",
									 "There was no reason for it to change,",
									 "but it did.",
									 "It really did." };
	private TestDialog _testDialog;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_testDialog = new TestDialog(0.1f, txt);
		AddChild(_testDialog);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
