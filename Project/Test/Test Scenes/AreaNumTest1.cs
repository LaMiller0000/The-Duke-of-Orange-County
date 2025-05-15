using Godot;
using System;
using System.Linq;

public partial class AreaNumTest1 : Area3D
{
	// Called when the node enters the scene tree for the first time.
	private int count = 0;
	private int minLength = 60;
	private RayCast3D rayCast;
	private moveMath moveMath;

	public override void _Ready()
	{
		// Connect the body_entered signal
		BodyEntered += OnBodyEntered;
	}

	private void OnBodyEntered(Node3D body)
	{
		GD.Print("Body entered: " + body.Name);
		
		var overlappingBodies = GetOverlappingBodies();
		GD.Print("Overlapping bodies count: " + overlappingBodies.Count);
		foreach (var nodies in overlappingBodies)
		{
			GD.Print("Checking body: " + nodies.Name);
			if (nodies.IsInGroup("TestGroup1"))
			{
				GD.Print("Found body in TestGroup1");
				
				// Create the raycast
				rayCast = new RayCast3D();
				AddChild(rayCast);
				
				// Setup raycast properties
				rayCast.GlobalPosition = GlobalPosition;
				// This is the critical fix - target position needs to be direction vector
				Vector3 direction = nodies.GlobalPosition - this.GlobalPosition;
				rayCast.TargetPosition = direction;
				rayCast.CollisionMask = uint.MaxValue;
				rayCast.CollideWithAreas = true;  
				rayCast.CollideWithBodies = true;
				rayCast.Enabled = true;
				
				// Process one frame to allow raycast to update
				rayCast.ForceRaycastUpdate();
				
				GD.Print("Raycast from: " + rayCast.GlobalPosition);
				GD.Print("Raycast to: " + (rayCast.GlobalPosition + rayCast.TargetPosition));
				GD.Print("Is colliding: " + rayCast.IsColliding());
				
				if (rayCast.IsColliding())
				{
					Node3D collider = rayCast.GetCollider() as Node3D;
					GD.Print("Raycast hit: " + (collider != null ? collider.Name : "unknown"));
				}
				
				// Create and use distance calculator
				moveMath = new moveMath();
				int distance = moveMath.distanceCalc(nodies.GlobalPosition, GlobalPosition);
				GD.Print("Distance: " + distance);
				
				if (distance < minLength)
				{
					minLength = distance;
					GD.Print("New minimum length: " + minLength);
				}
				
				++count;
				
				// Clean up
				moveMath.QueueFree();
				rayCast.QueueFree();
				
			}
		}
		
		GD.Print("Minimum length: " + minLength);
		GD.Print("Total count: " + count);
		count = 0;
	}
}
