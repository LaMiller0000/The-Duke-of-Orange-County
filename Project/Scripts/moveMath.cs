
using Godot;
using System;

public partial class moveMath : Node
{
	private Vector3 prevPos, nextPos, currPos = Vector3.Zero;

	public void setNextPos(Vector3 nextPos) => this.nextPos = nextPos;
	public Vector3 getNextPos() => nextPos;
	public void setPrevPos(Vector3 prevPos) => this.prevPos = prevPos;
	public Vector3 getPrevPos() => prevPos;
	public void setCurrPos(Vector3 currPos) => this.currPos = currPos;
	public Vector3 getCurrPos() => currPos;

	// a math function which spits out an acceleration value // look mom no if statements :)
	public float acceleration(float distance, float xOffSet = 10, float yOffSet = 10, float intensity = 2) => (float)Math.Cbrt(distance - xOffSet) * intensity + yOffSet;

	// calculates the distance between two points
	public int distanceCalc(Vector3 point1, Vector3 point2) => (int)Mathf.Sqrt(Mathf.Pow((point1.X - point2.X), 2) + Mathf.Pow((point1.Y - point2.Y), 2) + Mathf.Pow((point1.Z - point2.Z), 2));

	// determins if the player is moving vertically more than horizontally & diagonal 
	public bool jumpCalc(Vector3 point1, Vector3 point2)
	{
		int x = (int)Mathf.Sqrt(Mathf.Pow((point1.X - point2.X), 2) + Mathf.Pow((point1.Z - point2.Z), 2)) / 2;
		int y = (int)Mathf.Sqrt(Mathf.Pow((point1.Y - point2.Y), 2));

		if (x < y)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	public int converter(Vector3 dot) => (int)Mathf.Sqrt((dot.X * dot.X) + (dot.Y * dot.Y) * (dot.Z * dot.Z)) / 3;
}
