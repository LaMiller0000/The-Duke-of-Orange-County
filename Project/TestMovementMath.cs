using Godot;
using System;

public class moveMath : Node
{
	private Vector3 originalVector, initialVector, newVector = Vector3.zero;

	public int converter(Vector3 dot) => (int)Mathf.Sqrt((dot.x * dot.x) + (dot.y * dot.y) * (dot.z * dot.z)) / 3;

	public Vector3 getInitPos() => initialVector;
	public Vector3 getOrigPos() => originalVector;
	public Vector3 getNewPos() => newVector;

	public void setOrigPos(Vector3 pos) => originalVector = pos;
	public void setInitPos(Vector3 pos) => initialVector = pos;
	public void setNewPos(Vector3 pos)
	{
		newVector = pos;
	}


	// calculates the distance between two points
	public int distanceCalc(Vector3 point1, Vector3 point2) => (int)Mathf.Sqrt(Mathf.Pow((point1.x - point2.x), 2) + Mathf.Pow((point1.y - point2.y), 2) + Mathf.Pow((point1.z - point2.z), 2));

	// determins if the player is moving vertically more than horizontally & diagonal 
	public bool jumpCalc(Vector3 point1, Vector3 point2)
	{
		int x = ((int)Math.Sqrt(Mathf.Pow((point1.x - point2.x), 2) + Mathf.Pow((point1.z - point2.z), 2)));
		int y = (int)Math.Sqrt(Mathf.Pow((point1.y - point2.y), 2));

		if (y > x)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	// makes sure the number that is called by using a math function isn't less than 0
	public int deter(int x)
	{
		if (0 > x)
		{
			return 0;
		}
		else
		{
			return x;
		}
	}

	// a math function that is used to add acceleration to the already existant acceleration
	// d is at what distance this parabola >= 0 // i is the intensity // distance is the distance the player has traveled
	public int addAcceleration(double i = 0, double d = 0, double distance = 0)
	{
		d += (Mathf.Sqrt(4 * (float)i));
		distance -= d;
		return deter((int)(-(Mathf.Pow((float)distance, 2)) + (float)i));
	}

	// a math function which spits out an acceleration value // look mom no if statements :)
	public float acceleration(float distance) => (float)Math.Cbrt(distance - 5f) + 20f;//(Mathf.Pow(distance, 1 / 3) + 10) * Mathf.Sign(distance);

	// a math function which spits out a value for jump height // distance for this function works how far the player has moved since calling the function.
	public (float, float ) jump(float time = 0, float height = 5, float gravity = -9.8f)
	{
		float timeOfApex = time / 2;
		gravity = (2 * height) / Mathf.Pow(timeOfApex, 2);
		return (((2 * height) / timeOfApex), gravity);
		//deter((int)(-(Mathf.Pow(distance, 2) + (height * distance))));
	}

}
