using Godot;
using System;

public partial class BasicRPGCard : Node
{
	public int Health;
	public int Mana;
	public int Stamina;
	public int MeleeSkill;
	public int MagicSkill;
	
	public BasicRPGCard()
	{
		Health = 10;
		Mana = 10;
		Stamina = 10;
		MeleeSkill = 10;
		MagicSkill = 10;
	}
	
	public BasicRPGCard(int H, int M, int S, int MeS, int MaS)
	{
		Health = H;
		Mana = M;
		Stamina = S;
		MeleeSkill = MeS;
		MagicSkill = MaS;
	}
}
