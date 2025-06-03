using System;
using Godot;

public partial class TestingScene1 : Node3D
{
	private CharacterBody3D characterBody;
	private int radius {get; set;}
	private int counter = 0;
	private AreaTargetSt s1;
	
	public override void _Ready()
	{
		radius = 10;
		characterBody = GetNode<CharacterBody3D>("Root");
		s1 = new AreaTargetSt(radius);
		s1.setPlayer(characterBody);
		//AddChild(s1);
	}
	
	public override void _Process(double delta)
	{
		if (counter == 0)
		{
			counter++;
			AddChild(s1);
			StaticBody3D up = GetNode<StaticBody3D>("Target2");
			StaticBody3D down = GetNode<StaticBody3D>("Target4");
			StaticBody3D right = GetNode<StaticBody3D>("Target5");
			StaticBody3D left = GetNode<StaticBody3D>("Target6");
			up.Position = characterBody.GlobalPosition + 5 * new Vector3((float)Math.Cos(Math.PI / 2), 0, (float)Math.Sin(Math.PI / 2));
			down.Position = characterBody.GlobalPosition + 5 * new Vector3((float)Math.Cos(3 * Math.PI / 2), 0, (float)Math.Sin(3 * Math.PI / 2));
			right.Position = characterBody.GlobalPosition + 5 * new Vector3((float)Math.Cos(2 * Math.PI), 0, (float)Math.Sin(2 * Math.PI));
			left.Position = characterBody.GlobalPosition + 5 * new Vector3((float)Math.Cos(Math.PI), 0, (float)Math.Cos(Math.PI));
		}
	}

}
