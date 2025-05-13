using Godot;
using System;

public partial class TestRPG_CardScene : Node
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		RPG_Card card = new RPG_Card();
		RPG_Card.RPG cardRPG = new RPG_Card.RPG();
		cardRPG = card.CalcRPG();
		GD.Print("Health : " + cardRPG._health);
		GD.Print("Mana : " + cardRPG._mana);
		GD.Print("Intelligence : " + cardRPG._intelligence);
		GD.Print("Strength : " + cardRPG._strength);
		GD.Print("Dexterity : " + cardRPG._dexterity);
		GD.Print("Faith : " + cardRPG._faith);
		GD.Print("Constitution : " + cardRPG._constitution);
		GD.Print("Luck : " + cardRPG._luck);
		// Create the RichTextLabel and enable BBCode
		RichTextLabel cardText = new RichTextLabel();
		cardText.BbcodeEnabled = true;

		// Append formatted text for RPG stats
		cardText.Text += $"[b]Health:[/b] {cardRPG._health}\n";
		cardText.Text += $"[b]Mana:[/b] {cardRPG._mana}\n";
		cardText.Text += $"[b]Intelligence:[/b] {cardRPG._intelligence}\n";
		cardText.Text += $"[b]Strength:[/b] {cardRPG._strength}\n";
		cardText.Text += $"[b]Dexterity:[/b] {cardRPG._dexterity}\n";
		cardText.Text += $"[b]Faith:[/b] {cardRPG._faith}\n";
		cardText.Text += $"[b]Constitution:[/b] {cardRPG._constitution}\n";
		cardText.Text += $"[b]Luck:[/b] {cardRPG._luck}\n";

		// Create the Panel and set its size
		Panel panel = new Panel();
		panel.SetSize(new Vector2(300, 300)); // Ensure the correct method for size setting

		// Add the panel and text label to the scene
		AddChild(panel);
		panel.AddChild(cardText);
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
