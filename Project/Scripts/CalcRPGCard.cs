using Godot;
using System;

public partial class CalcRPGCard : Node
{
	// Called when the node enters the scene tree for the first time.
	private moveMath _moveMath = new moveMath();
	


	public int calcHealth(int health) => (int)(_moveMath.acceleration(health, -20, 100) + _moveMath.acceleration(health, -30, 0));
	
	public int calcMana(int mana) => (int)(_moveMath.acceleration(mana, -20, 100) + _moveMath.acceleration(mana, -30, 0));
	
	public int calcStamina(int stamina) => (int)(_moveMath.acceleration(stamina, -20, 100) + _moveMath.acceleration(stamina, -30, 0));
	
	public int calcMeleeSkill(int meS) => (int)(_moveMath.acceleration(meS, -20, 100) + _moveMath.acceleration(meS, -30, 0));
	
	public int calcMagicSkill(int maS) => (int)(_moveMath.acceleration(maS, -20, 100) + _moveMath.acceleration(maS, -30, 0));

	public int calcAgility(int stamina, int health) => ((1/2) * stamina) + ((1/5) * health);

	public void calcXCard(XRPGCard card, BasicRPGCard card2)
	{
		card.health = calcHealth(card2.Health);
		card.mana = calcMana(card2.Mana);
		card.stamina = calcStamina(card2.Stamina);
		card.meleeSkill = calcMeleeSkill(card2.MeleeSkill);
		card.magicSkill = calcMagicSkill(card2.MagicSkill);
		card.agility = calcAgility(card2.Stamina, card2.Health);
	}
}
