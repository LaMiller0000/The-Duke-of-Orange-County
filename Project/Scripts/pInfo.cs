using Godot;
//using System;

public partial class pInfo : Node
{
	// can be used for loading the players stats, but going to use for bosses to get stats.
	public float playerHealth {  get; set; }
	public Vector3 playerPos { get; set; }
	public int playerStamina { get; set; }

	public pInfo()
	{

	}

	public pInfo(int playerHealth, Vector3 playerPos, int playerStamina)
	{
		this.playerHealth = playerHealth;
		this.playerPos = playerPos;
		this.playerStamina = playerStamina;
	}

   ~pInfo()
	{
		this.playerHealth = 0;
		this.playerPos = Vector3.Zero;
		this.playerStamina = 0;
	}
}
