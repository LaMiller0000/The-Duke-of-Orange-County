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
	private Node3D _currentTarget;
	private double _springArmRotation;
	private double minAngle { get; set; }
	private double maxAngle { get; set;}

	[Signal]
	public delegate void TargetRecievedEventHandler(Node3D t);

	public override void _Ready()
	{
		//GD.Print("4");
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

	public AreaNumTest1(int r, double minAngle, double maxAngle)
	{
		radius = r;
		this.minAngle = minAngle;
		this.maxAngle = maxAngle;
	}

	public void setSpringArmRotation(double angle)
	{
		_springArmRotation = angle;
	}

	public void setRadius(int r)
	{
		radius = r;
	}

	public Node3D getTarget()
	{

		return target;
	}

	public void setAngles(double minAngle, double maxAngle)
	{
		this.minAngle = minAngle;
		this.maxAngle = maxAngle;
	}
	public void setTarget(Node3D target)
	{
		this._currentTarget = target;
	}

	private void OnBodyEntered(Node3D body)
	{
		//GD.Print("OnBodyEntered");
		//minLength = radius;
		var overlappingBodies = GetOverlappingBodies();
		foreach (var nodies in overlappingBodies)
		{
			if (nodies.IsInGroup("TestGroup1") && (nodies != _currentTarget))
			{
				float dx = nodies.GlobalPosition.X - body.GlobalPosition.X;
				float dy = nodies.GlobalPosition.Y - body.GlobalPosition.Y;
				double angle = Math.Atan2(dx, dy);
				if (angle < 0) { angle += 2.0 * Math.PI;}
// minAngle (5 * Math.PI) / 6
// maxAngle Math.PI / 6
				if (angle <=  maxAngle && angle >= minAngle)
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
					//rayCast.AddException(GetParent().GetChild<CollisionShape3D>("CollisionShape3D"));
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
							GD.Print(angle * 180 / Math.PI);
							//GD.Print("New minimum length: " + minLength);
						}
					
						//moveMath.QueueFree();
					}
					++count;
					rayCast.QueueFree();
				}
			}
			
			//GD.Print(nodies.ToString());
		}

		//GD.Print("OnBodyEntered before emmision");
		//GD.Print(target);
		EmitSignal("TargetRecieved", target);
		count = 0;
	}
}
