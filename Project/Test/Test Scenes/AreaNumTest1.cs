using Godot;
using System;
using System.Linq;

public partial class AreaNumTest1 : Area3D
{
	// Called when the node enters the scene tree for the first time.
	private int count = 0;
	public int radius { get; set; }
	private int minLength;
	private RayCast3D rayCast;
	private moveMath moveMath;
	public Node3D target;

	[Signal]
	public delegate void TargetRecievedEventHandler(Node3D t);

	public override void _Ready()
	{
		GD.Print("4");
		BodyEntered += OnBodyEntered;
	}

	public AreaNumTest1()
	{
		radius = 10;
		minLength = 20;
	}

	public AreaNumTest1(int r)
	{
		radius = r;
		minLength = 2 * r;
	}

	public void setRadius(int r)
	{
		radius = r;
	}

	public Node3D getTarget()
	{

		return target;
	}

	private void OnBodyEntered(Node3D body)
	{
		GD.Print("OnBodyEntered");
		//minLength = radius;
		var overlappingBodies = GetOverlappingBodies();
		foreach (var nodies in overlappingBodies)
		{
			if (nodies.IsInGroup("TestGroup1"))
			{
				float dx = nodies.GlobalPosition.X - body.GlobalPosition.X;
				float dy = nodies.GlobalPosition.Y - body.GlobalPosition.Y;
				double angle = Math.Atan2(dx, dy);
				if (angle < 0) { angle += 2.0 * Math.PI;}

				if (angle <= (5 * Math.PI) / 6 && angle >= Math.PI / 6)
				{
					rayCast = new RayCast3D();
					AddChild(rayCast);
					rayCast.GlobalPosition = GlobalPosition;
					Vector3 direction = nodies.GlobalPosition - this.GlobalPosition;
					rayCast.TargetPosition = direction;
					rayCast.CollisionMask = uint.MaxValue;
					rayCast.CollideWithAreas = true;
					rayCast.CollideWithBodies = true;
					rayCast.ExcludeParent = true;
					rayCast.Enabled = true;
					rayCast.ForceRaycastUpdate();
					Node3D collider = rayCast.GetCollider() as Node3D;
					if (rayCast.IsColliding() && collider.IsInGroup("TestGroup1"))
					{
						moveMath = new moveMath();
						int distance = moveMath.distanceCalc(nodies.GlobalPosition, GlobalPosition);
						if (distance < minLength)
						{
							minLength = distance;
							target = nodies;
							GD.Print("New minimum length: " + minLength);
						}
					
						//moveMath.QueueFree();
					}
					++count;
					rayCast.QueueFree();
				}
			}
			
			GD.Print(nodies.ToString());
		}

		GD.Print("OnBodyEntered before emmision");
		GD.Print(target);
		EmitSignal("TargetRecieved", target);
		count = 0;
	}
}
