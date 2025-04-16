using Godot;
using System;

public partial class TestMagicSelect : Node2D
{
	/**/
	[Export]
	public Area2D up;
	
	[Export]
	public Area2D down;
	
	[Export]
	public Area2D left;
	
	[Export]
	public Area2D right;
	
	[Export]
	public int Radius = 80;
	*/
	private Area2D up;
	private Area2D down;
	private Area2D left;
	private Area2D right;
	
	private int coor = 800;
	
	public override void _Ready()
	{
		up.Position = Angle(Math.PI / 2);
		down.Position = Angle((3 * Math.PI) / 2);
		left.Position = Angle(Math.PI);
		right.Position = Angle(2 * Math.PI);

	}
	
	private void AddChildren(Area Point)
	{
		//Point.add
		break;
	}
	
	public Vector2 Angle(double angle)
	{
		float x = coor + Radius * (float)Math.Cos(angle);
		float y = coor + Radius * (float)Math.Sin(angle);
		return new Vector2(x, y);
	}
}
