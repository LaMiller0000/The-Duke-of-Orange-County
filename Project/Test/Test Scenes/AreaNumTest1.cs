using Godot;
using System;
using System.Linq;

public partial class AreaNumTest1 : Area3D
{
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

	// Helper method to normalize angle to [0, 2π]
	private double NormalizeAngle(double angle)
	{
		return Math.Abs(angle % (2 * Math.PI));
	}

	// Helper method to check if angle is between min and max (handles wrapping)
	private bool IsAngleBetween(double angle, double min, double max)
	{
		angle = NormalizeAngle(angle);
		min = NormalizeAngle(min);
		max = NormalizeAngle(max);

		if (min <= max)
		{
			return angle >= min && angle <= max;
		}
		else
		{
			// Wrapping case (e.g., from 350° to 10°)
			return angle >= min || angle <= max;
		}
	}

	private void OnBodyEntered(Node3D body)
	{
	   var overlappingBodies = GetOverlappingBodies();
	   foreach (var nodies in overlappingBodies)
	   {
		  if (nodies.IsInGroup("TestGroup1") && (nodies != _currentTarget))
		  {
			Vector3 direction3D = nodies.GlobalPosition - this.GlobalPosition;
			float dx = direction3D.X;
			float dz = direction3D.Z;
			double absoluteAngleToTarget = Math.Atan2(dz, dx);
			Math.Abs(absoluteAngleToTarget % (2 * Math.PI));
			double adjustedMinAngle = NormalizeAngle(minAngle + _springArmRotation);
			double adjustedMaxAngle = NormalizeAngle(maxAngle + _springArmRotation);
			bool isInRange = IsAngleBetween(absoluteAngleToTarget, adjustedMinAngle, adjustedMaxAngle);
			if (isInRange)
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
				if (rayCast.IsColliding() && collider != null && collider.IsInGroup("TestGroup1"))
				{
					moveMath = new moveMath();
					int distance = moveMath.distanceCalc(nodies.GlobalPosition, GlobalPosition);
					if (distance < minLength)
					{
						minLength = distance;
						target = nodies;
						GD.Print($"Absolute angle: {absoluteAngleToTarget * 180 / Math.PI:F1}°");
						GD.Print($"Spring arm rotation: {_springArmRotation * 180 / Math.PI:F1}°");
					}
				}
				++count;
				rayCast.QueueFree();
			}
		  }
	   }
	   EmitSignal("TargetRecieved", target);
	   count = 0;
	}
}
