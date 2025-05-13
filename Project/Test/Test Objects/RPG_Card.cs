using Godot;
using System;

public partial class RPG_Card : Node
{
	// Called when the node enters the scene tree for the first time.
	public struct RPG
	{
		public int _health;
		public int _mana;
		public int _intelligence;
		public int _faith;
		public int _strength;
		public int _dexterity;
		public int _constitution;
		public int _luck;
	}
	
	private RPG _rpg;
	private RPG _out;

	public RPG_Card()
	{
		_rpg = new RPG();
		_out = new RPG();
		_rpg._health = 10;
		_rpg._mana = 10;
		_rpg._intelligence = 10;
		_rpg._faith = 10;
		_rpg._strength = 10;
		_rpg._dexterity = 10;
		_rpg._constitution = 10;
		_rpg._luck = 10;
	}

	public RPG_Card(
		int health,
		int mana,
		int intelligence,
		int faith,
		int strength,
		int dexterity,
		int constitution,
		int luck)
	{
		_out = new RPG();
		_rpg = new RPG();
		_rpg._health = health;
		_rpg._mana = mana;
		_rpg._intelligence = intelligence;
		_rpg._faith = faith;
		_rpg._strength = strength;
		_rpg._dexterity = dexterity;
		_rpg._constitution = constitution;
		_rpg._luck = luck;
	}
	
	public override void _Ready()
	{
	}

	public RPG GetRPGOut() => _out;

	public void SetRPGOut(RPG rpg) => _out = rpg;

	public RPG GetRPGCard() => _rpg;

	public void SetRPGCard(RPG rpg) => _rpg = rpg;

	public RPG CalcRPG()
	{
		moveMath calculator = new moveMath();
		_out._health = (int)calculator.acceleration(_rpg._health, -50f, 20f, 3f);
		_out._mana = (int)calculator.acceleration(_rpg._mana);
		_out._intelligence = (int)calculator.acceleration(_rpg._intelligence);
		_out._faith = (int)calculator.acceleration(_rpg._faith);
		_out._strength = (int)calculator.acceleration(_rpg._strength);
		_out._dexterity = (int)calculator.acceleration(_rpg._dexterity);
		_out._constitution = (int)calculator.acceleration(_rpg._constitution);
		_out._luck = (int)calculator.acceleration(_rpg._luck);
		calculator.QueueFree();
		return _out;
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
