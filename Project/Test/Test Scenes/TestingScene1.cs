using System;
using Godot;

public partial class TestingScene1 : Node3D
{
	private CharacterBody3D characterBody;
	private int radius {get; set;}
	
	public override void _Ready()
	{
		radius = 10;
		characterBody = GetNode<CharacterBody3D>("Root");
		
	}
	
	public override void _Input(InputEvent @event)
	{
		if (Input.IsActionJustReleased("ui_accept"))
		{
			characterBody = GetNode<CharacterBody3D>("Root");
			if ((int)characterBody.Get("STATED") == 1)
			{
				AreaTargetSt s1 = new AreaTargetSt(radius);
				s1.setPlayer(characterBody);
				AddChild(s1);
			}
			else if ((int)characterBody.Get("STATED") == 0)
			{
				
				characterBody.Set("STATED", 1);
			}
		}
	}
}
