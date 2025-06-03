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
		while (angle < 0) angle += 2.0 * Math.PI;
		while (angle >= 2.0 * Math.PI) angle -= 2.0 * Math.PI;
		return angle;
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
			 // Calculate direction from this area to the target
			 Vector3 direction3D = nodies.GlobalPosition - this.GlobalPosition;
			 
			 // For 3D, you might want to project to XZ plane or XY plane depending on your game
			 // Assuming XZ plane (horizontal movement, Y is up)
			 float dx = direction3D.X;
			 float dz = direction3D.Z;
			 
			 // Get absolute angle to target
			 double absoluteAngleToTarget = Math.Atan2(dz, dx);
			 if (absoluteAngleToTarget < 0) 
			 { 
				absoluteAngleToTarget += 2.0 * Math.PI;
			 }
			 
			 // Calculate relative angle by subtracting spring arm rotation
			 double relativeAngle = absoluteAngleToTarget - _springArmRotation;
			 relativeAngle = NormalizeAngle(relativeAngle);
			 
			 // Adjust min and max angles relative to spring arm rotation
			 double adjustedMinAngle = NormalizeAngle(minAngle + _springArmRotation);
			 double adjustedMaxAngle = NormalizeAngle(maxAngle + _springArmRotation);
			 
			 // Check if the target is within the angular range
			 bool isInRange = IsAngleBetween(absoluteAngleToTarget, adjustedMinAngle, adjustedMaxAngle);
			 
			 if (isInRange)
			 {
				// Create and configure raycast
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
					  
					  // Debug output showing both absolute and relative angles
					  GD.Print($"Absolute angle: {absoluteAngleToTarget * 180 / Math.PI:F1}°");
					  GD.Print($"Relative angle: {relativeAngle * 180 / Math.PI:F1}°");
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
