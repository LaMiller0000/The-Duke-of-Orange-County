using Godot;
using System;
using System.Numerics;

public partial class dMath : Node
{
	public bool bStab(Node3D Attacker, Node3D Target, int intensity = 5)
	{
		if (direction(Target, Attacker, intensity) && lineC(Target, Attacker, intensity))
		{
			return (true);
		}
		else { return false; }
	}

	private bool lineC(Node3D A, Node3D B, int i = 5)
	{
		// the tan function on the poles gives back either undefined or 0 which is useless with the formula
		if (A.RotationDegrees.Y == 0 || A.RotationDegrees.Y == 360 || A.RotationDegrees.Y == -360)
		{
			if (B.Transform.Origin.X < A.Transform.Origin.X && (B.Transform.Origin.Y <= A.Transform.Origin.Y + i || B.Transform.Origin.Y >= A.Transform.Origin.Y - i)) { return true; }
			else { return false; }
		}
		else if (A.RotationDegrees.Y == 180 || A.RotationDegrees.Y == -180)
		{
			if (B.Transform.Origin.X > A.Transform.Origin.X && (B.Transform.Origin.Y <= A.Transform.Origin.Y + i || B.Transform.Origin.Y >= A.Transform.Origin.Y - i)) { return true; }
			else { return false; }
		}
		else if (A.RotationDegrees.Y == 90 || A.RotationDegrees.Y == -270)
		{
			if (B.Transform.Origin.Y < A.Transform.Origin.Y && (B.Transform.Origin.X <= A.Transform.Origin.X + i || B.Transform.Origin.X >= A.Transform.Origin.X - i)) { return true; }
			else { return false; }
		}
		else if (A.RotationDegrees.Y == -90 || A.RotationDegrees.Y == 27 )
		{
			if (B.Transform.Origin.Y > A.Transform.Origin.Y && (B.Transform.Origin.X <= A.Transform.Origin.X + i || B.Transform.Origin.X >= A.Transform.Origin.X - i)) { return true; }
			else { return false; }
		}
		else 
		{

			double c = 5 * Math.Cos(A.RotationDegrees.Y);
			double s = 5 * Math.Sin(A.RotationDegrees.Y);
			double x2 = A.Transform.Origin.X + c;
			double y2 = A.Transform.Origin.Y + s;
			bool x = false;
			bool y = false;

			if (A.Transform.Origin.X < x2) { if (B.Transform.Origin.X <  A.Transform.Origin.X) { x = true; } } // Determines if B is behind A
			else if (B.Transform.Origin.X > A.Transform.Origin.X) { x = true; } 
			if(A.Transform.Origin.Y < y2) { if (B.Transform.Origin.Y < A.Transform.Origin.Y) { y = true; } }
			else if (B.Transform.Origin.Y > A.Transform.Origin.Y) { y = true; }

			if (x &&  y) { return true; }
			else { return false; }
		}
	}

	// determines if the two target are looking in the same direction w/ wiggle room
	private bool direction(Node3D  A, Node3D B, int i = 10)
	{
		var rotationA = A.GlobalRotationDegrees.Y;
		var rotationB = B.GlobalRotationDegrees.Y;
		if (rotationA + i >= rotationB || rotationA - i <= rotationB) { return true; }
		else { return false; }
	}
}
